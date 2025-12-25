import React from 'react';
import { ExternalLink } from 'lucide-react';

export default function ProductList({ products, loading }) {
  if (loading) {
    return (
      <div className="flex justify-center items-center py-20">
        <div className="relative">
          <div className="w-16 h-16 border-4 border-cyan-300/30 border-t-cyan-300 rounded-full animate-spin"></div>
          <div className="absolute inset-0 w-16 h-16 border-4 border-cyan-400/20 border-b-cyan-400 rounded-full animate-spin" style={{animationDelay: '150ms'}}></div>
        </div>
      </div>
    );
  }

  if (products.length === 0) {
    return (
      <div className="text-center py-20">
        <div className="inline-block px-8 py-6 bg-black/40 backdrop-blur-xl rounded-2xl border border-cyan-300/20 shadow-[0_8px_32px_0_rgba(0,0,0,0.4)]">
          <p className="text-cyan-100 text-lg font-light">No products found.  Try another search!</p>
        </div>
      </div>
    );
  }

  return (
    <div className="max-w-7xl mx-auto px-4 py-8">
      <div className="mb-8 flex items-center justify-between">
        <h2 className="text-2xl font-bold text-cyan-100 drop-shadow-[0_0_20px_rgba(34,211,238,0.4)]">
          Showing {products.length} Products
        </h2>
        <div className="text-cyan-300/70 text-sm font-light">
          {products.length} {products.length === 1 ?  'result' : 'results'}
        </div>
      </div>
      
      {/* Dark grid layout like reference image */}
      <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4">
        {products.map((product, index) => (
          <div
            key={index}
            className="group relative"
          >
            {/* Dark glass card */}
            <div className="relative bg-black/60 backdrop-blur-lg rounded-lg border border-cyan-300/20 shadow-[0_4px_20px_0_rgba(0,0,0,0.5)] hover:shadow-[0_8px_30px_0_rgba(34,211,238,0.3)] hover:border-cyan-300/40 transition-all duration-300 overflow-hidden hover:scale-[1.02]">
              
              {/* Product Image */}
              <div className="relative aspect-square overflow-hidden bg-gradient-to-br from-gray-900 to-black p-4">
                {product.image ? (
                  <img
                    src={product.image}
                    alt={product.name}
                    className="w-full h-full object-contain group-hover:scale-110 transition-transform duration-500"
                  />
                ) : (
                  <div className="w-full h-full flex items-center justify-center text-cyan-300/30">
                    <svg className="w-16 h-16" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1} d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />
                    </svg>
                  </div>
                )}
                
                {/* Hover overlay */}
                <div className="absolute inset-0 bg-gradient-to-t from-black/60 via-transparent to-transparent opacity-0 group-hover: opacity-100 transition-opacity duration-300"></div>
              </div>
              
              {/* Product Info */}
              <div className="p-4 border-t border-cyan-300/10">
                <h3 className="text-cyan-50 font-medium text-sm mb-2 line-clamp-2 min-h-[40px] group-hover:text-cyan-200 transition-colors duration-300">
                  {product.name}
                </h3>
                
                {product.price && (
                  <p className="text-xl font-bold text-cyan-300 mb-2 drop-shadow-[0_0_8px_rgba(34,211,238,0.4)]">
                    {product.price}
                  </p>
                )}
                
                {product.source && (
                  <p className="text-cyan-200/50 text-xs mb-3 font-light uppercase tracking-wide">
                    {product.source}
                  </p>
                )}
                
                {product.url && (
                  <a
                    href={product.url}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="flex items-center justify-center gap-2 w-full px-3 py-2 bg-cyan-500/20 hover:bg-cyan-500/30 border border-cyan-400/30 hover:border-cyan-400/50 text-cyan-100 rounded-md transition-all duration-300 text-xs font-light shadow-[0_0_10px_rgba(34,211,238,0.15)] hover:shadow-[0_0_20px_rgba(34,211,238,0.3)]"
                  >
                    View
                    <ExternalLink size={14} />
                  </a>
                )}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}