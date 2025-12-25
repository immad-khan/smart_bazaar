-- Smart Bazaar Database Schema Setup Script
-- Run this in your Supabase SQL Editor

-- Create Sellers table
CREATE TABLE IF NOT EXISTS Sellers (
    SellerID SERIAL PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(20),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Stores table
CREATE TABLE IF NOT EXISTS Stores (
    StoreID SERIAL PRIMARY KEY,
    SellerID INTEGER NOT NULL REFERENCES Sellers(SellerID) ON DELETE CASCADE,
    StoreName VARCHAR(100) NOT NULL,
    Address VARCHAR(255) NOT NULL,
    ContactNumber VARCHAR(20) NOT NULL,
    Description TEXT,
    Latitude DOUBLE PRECISION,
    Longitude DOUBLE PRECISION,
    GoogleMapsUrl VARCHAR(500),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Products table
CREATE TABLE IF NOT EXISTS Products (
    ProductID SERIAL PRIMARY KEY,
    StoreID INTEGER NOT NULL REFERENCES Stores(StoreID) ON DELETE CASCADE,
    ProductName VARCHAR(100) NOT NULL,
    Description TEXT,
    Price DECIMAL(10, 2) NOT NULL CHECK (Price > 0),
    StockQuantity INTEGER NOT NULL CHECK (StockQuantity >= 0),
    Category VARCHAR(50) NOT NULL,
    ImageUrl VARCHAR(500),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create indexes for better query performance
CREATE INDEX IF NOT EXISTS idx_sellers_email ON Sellers(Email);
CREATE INDEX IF NOT EXISTS idx_stores_seller ON Stores(SellerID);
CREATE INDEX IF NOT EXISTS idx_products_store ON Products(StoreID);
CREATE INDEX IF NOT EXISTS idx_products_category ON Products(Category);

-- Display success message
SELECT 'Database tables created successfully!' AS status;
