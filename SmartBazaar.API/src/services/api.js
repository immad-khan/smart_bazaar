const API_BASE_URL = 'http://localhost:5000/api';

export const searchProducts = async (query) => {
  try {
    const response = await fetch(`${API_BASE_URL}/scraper/search?q=${encodeURIComponent(query)}`);
    if (!response.ok) {
      throw new Error('Search failed');
    }
    return await response.json();
  } catch (error) {
    console.error('API Error:', error);
    return [];
  }
};

export const scrapeProduct = async (url) => {
  try {
    const response = await fetch(`${API_BASE_URL}/scraper/test-naheed?url=${encodeURIComponent(url)}`);
    if (!response.ok) {
      throw new Error('Scrape failed');
    }
    return await response.json();
  } catch (error) {
    console.error('API Error:', error);
    return null;
  }
};

export const checkHealth = async () => {
  try {
    const response = await fetch(`${API_BASE_URL}/health`);
    if (!response.ok) {
      throw new Error('Health check failed');
    }
    return await response.json();
  } catch (error) {
    console.error('API Error:', error);
    return null;
  }
};
