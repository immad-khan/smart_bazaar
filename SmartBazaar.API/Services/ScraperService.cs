using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SmartBazaar.API.Models;

namespace SmartBazaar.API.Services;

public class ScraperService
{
    private readonly HttpClient _httpClient;
    private readonly List<string> _userAgents = new()
    {
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36",
        "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36"
    };

    public ScraperService()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    // 1. Search Logic for Naheed (Fast HttpClient)
    public async Task<List<ScrapedProduct>> SearchNaheed(string keyword)
    {
        try
        {
            string searchUrl = $"https://www.naheed.pk/catalogsearch/result/?q={Uri.EscapeDataString(keyword)}";
            var request = new HttpRequestMessage(HttpMethod.Get, searchUrl);
            request.Headers.Add("User-Agent", GetRandomUserAgent());

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return new List<ScrapedProduct>();

            var html = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var nodes = doc.DocumentNode.SelectNodes("//li[contains(@class, 'product-item')]");
            var results = new List<ScrapedProduct>();

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var name = node.SelectSingleNode(".//a[@class='product-item-link']")?.InnerText.Trim();
                    var price = node.SelectSingleNode(".//span[@data-price-type='finalPrice']//span[@class='price']")?.InnerText.Trim();
                    var link = node.SelectSingleNode(".//a[@class='product-item-link']")?.GetAttributeValue("href", "");
                    var image = node.SelectSingleNode(".//img[@class='product-image-photo']")?.GetAttributeValue("src", "");

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(price))
                    {
                        results.Add(new ScrapedProduct {
                            Name = System.Net.WebUtility.HtmlDecode(name),
                            Price = price,
                            Source = "Naheed",
                            Link = link ?? "",
                            Image = image ?? ""
                        });
                    }
                }
            }
            return results;
        }
        catch { return new List<ScrapedProduct>(); }
    }

    // 2. Search Logic for Daraz (Selenium for JavaScript)
    public async Task<List<ScrapedProduct>> SearchDaraz(string keyword)
    {
        var results = new List<ScrapedProduct>();
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--silent");
        options.AddArgument("--log-level=3"); // Hides annoying logs
        options.AddArgument($"user-agent={GetRandomUserAgent()}");

        var service = ChromeDriverService.CreateDefaultService();
        service.HideCommandPromptWindow = true; // Keeps terminal clean

        using (var driver = new ChromeDriver(service, options))
        {
            try
            {
                string url = $"https://www.daraz.pk/catalog/?q={Uri.EscapeDataString(keyword)}";
                driver.Navigate().GoToUrl(url);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                // Wait for the product items to actually load into the DOM
                wait.Until(d => d.FindElements(By.CssSelector("div[data-qa-locator='product-item']")).Count > 0);

                var productElements = driver.FindElements(By.CssSelector("div[data-qa-locator='product-item']"));

                foreach (var item in productElements.Take(10))
                {
                    try {
                        var name = item.FindElement(By.CssSelector("div.title--wFj93 > a")).Text;
                        var price = item.FindElement(By.CssSelector("span.currency--GVKjl")).Text;
                        var link = item.FindElement(By.CssSelector("div.title--wFj93 > a")).GetAttribute("href");
                        var image = item.FindElement(By.CssSelector("img.mainPic--ehOdr")).GetAttribute("src");

                        results.Add(new ScrapedProduct {
                            Name = name,
                            Price = "Rs. " + price,
                            Image = image ?? "",
                            Link = link ?? url,
                            Source = "Daraz"
                        });
                    } catch { continue; }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Daraz Selenium Error: {ex.Message}"); }
        }
        return results;
    }

    // 3. Combine Everything
    public async Task<List<ScrapedProduct>> SearchAllStores(string query)
    {
        var naheedTask = SearchNaheed(query);
        var darazTask = SearchDaraz(query);

        await Task.WhenAll(naheedTask, darazTask);

        var allResults = new List<ScrapedProduct>();
        allResults.AddRange(await naheedTask);
        allResults.AddRange(await darazTask);

        return allResults;
    }

    private string GetRandomUserAgent() => _userAgents[new Random().Next(_userAgents.Count)];
}