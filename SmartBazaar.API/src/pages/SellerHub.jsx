import React, { useState, useEffect } from 'react';
import { Navigation } from './SellerComponents';
import { LoginPage, SignupPage } from './AuthPages';
import { StoreRegistrationPage } from './StorePages';
import { AddProductPage, EditProductPage, ProductsPage } from './ProductPages';

// Session Timeout Component
const SessionTimeout = ({ setCurrentPage, setUser }) => {
  const INACTIVITY_TIMEOUT = 15 * 60 * 1000; // 15 minutes
  const CHECK_INTERVAL = 5 * 60 * 1000; // Check every 5 minutes

  useEffect(() => {
    let lastActivityTime = new Date().getTime();
    
    const resetTimer = () => {
      lastActivityTime = new Date().getTime();
    };

    const checkInactivity = () => {
      const currentTime = new Date().getTime();
      if (currentTime - lastActivityTime > INACTIVITY_TIMEOUT) {
        localStorage.removeItem('user');
        setUser(null);
        setCurrentPage('login');
        alert('Session timed out due to inactivity. Please log in again.');
      }
    };

    document.addEventListener('click', resetTimer);
    document.addEventListener('mousemove', resetTimer);
    document.addEventListener('keydown', resetTimer);

    const inactivityTimer = setInterval(checkInactivity, CHECK_INTERVAL);

    return () => {
      clearInterval(inactivityTimer);
      document.removeEventListener('click', resetTimer);
      document.removeEventListener('mousemove', resetTimer);
      document.removeEventListener('keydown', resetTimer);
    };
  }, [setCurrentPage, setUser]);

  return null;
};

// Main SellerHub Component
export default function SellerHub({ navigateToLanding }) {
  const [currentPage, setCurrentPage] = useState('login');
  const [user, setUser] = useState(null);
  const [editingProduct, setEditingProduct] = useState(null);

  // Check for stored user on mount
  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
      setCurrentPage('products');
    }
  }, []);

  // Helper to set page with optional product data
  const handleSetCurrentPage = (page, productData) => {
    setCurrentPage(page);
    if (productData) {
      setEditingProduct(productData);
    }
  };

  const renderPage = () => {
    switch(currentPage) {
      case 'login':
        return <LoginPage setCurrentPage={handleSetCurrentPage} setUser={setUser} />;
      case 'signup':
        return <SignupPage setCurrentPage={handleSetCurrentPage} setUser={setUser} />;
      case 'store':
        return <StoreRegistrationPage setCurrentPage={handleSetCurrentPage} user={user} />;
      case 'addProduct':
        return <AddProductPage setCurrentPage={handleSetCurrentPage} user={user} />;
      case 'editProduct':
        return <EditProductPage setCurrentPage={handleSetCurrentPage} user={user} product={editingProduct} />;
      case 'products':
        return <ProductsPage setCurrentPage={handleSetCurrentPage} user={user} />;
      default:
        return <LoginPage setCurrentPage={handleSetCurrentPage} setUser={setUser} />;
    }
  };

  return (
    <div className="bg-gradient min-h-screen">
      <Navigation currentPage={currentPage} setCurrentPage={handleSetCurrentPage} user={user} />
      <SessionTimeout setCurrentPage={handleSetCurrentPage} setUser={setUser} />
      {renderPage()}
    </div>
  );
}
