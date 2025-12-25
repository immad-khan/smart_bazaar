import React, { useState } from 'react';
import LandingPage from './pages/LandingPage';
import SellerHub from './pages/SellerHub';

export default function App() {
  const [currentView, setCurrentView] = useState('landing'); // 'landing' or 'seller'

  const navigateToSeller = () => {
    setCurrentView('seller');
  };

  const navigateToLanding = () => {
    setCurrentView('landing');
  };

  return (
    <>
      {currentView === 'landing' ? (
        <LandingPage navigateToSeller={navigateToSeller} />
      ) : (
        <SellerHub navigateToLanding={navigateToLanding} />
      )}
    </>
  );
}