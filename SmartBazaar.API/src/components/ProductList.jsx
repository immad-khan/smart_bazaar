import React from 'react';
import ProductCard from './ProductCard';
import { Package } from 'lucide-react';

export default function ProductList({ products, loading }) {
  if (loading) {
    return (
      <div className="flex flex-col items-center justify-center py-20">
        <div className="animate-spin rounded-full h-16 w-16 border-b-4 border-cyan-500"></div>
        <p className="mt-4 text-white text-lg">Searching markets...</p>
      </div>
    );
  }

  if (products.length === 0) {
    return (
      <div className="flex flex-col items-center justify-center py-20 text-white">
        <Package size={64} className="text-slate-500 mb-4" />
        <p className="text-xl font-semibold">No products found</p>
        <p className="text-slate-400 mt-2">Try searching for something else</p>
      </div>
    );
  }

  return (
    <div className="w-full max-w-6xl mx-auto px-4">
      <div className="mb-6 text-white">
        <h2 className="text-2xl font-bold">
          Found {products.length} {products.length === 1 ? 'product' : 'products'}
        </h2>
      </div>
      
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {products.map((product, index) => (
          <ProductCard key={index} product={product} />
        ))}
      </div>
    </div>
  );
}
