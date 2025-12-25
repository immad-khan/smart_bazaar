import React from 'react';
import { ShoppingCart, ExternalLink } from 'lucide-react';

export default function ProductCard({ product }) {
  return (
    <div className="bg-white rounded-xl shadow-lg overflow-hidden hover:shadow-2xl transition-all transform hover:-translate-y-1">
      <div className="p-6">
        <div className="flex items-start justify-between mb-4">
          <h3 className="text-lg font-bold text-slate-800 flex-1 line-clamp-2">
            {product.name || 'Unnamed Product'}
          </h3>
          <span className="ml-2 px-3 py-1 bg-cyan-100 text-cyan-700 rounded-full text-xs font-semibold">
            {product.source}
          </span>
        </div>
        
        <div className="flex items-center justify-between">
          <div>
            <p className="text-sm text-slate-500 mb-1">Price</p>
            <p className="text-2xl font-bold text-cyan-600">
              {product.price || 'N/A'}
            </p>
          </div>
          
          {product.link && (
            <a 
              href={product.link} 
              target="_blank" 
              rel="noopener noreferrer"
              className="flex items-center gap-2 bg-cyan-600 hover:bg-cyan-700 text-white px-4 py-2 rounded-lg transition-colors"
            >
              <ShoppingCart size={18} />
              <span className="text-sm font-semibold">View</span>
              <ExternalLink size={14} />
            </a>
          )}
        </div>
      </div>
    </div>
  );
}
