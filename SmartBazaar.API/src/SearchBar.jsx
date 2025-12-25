import React, { useState } from 'react';
import { Search } from 'lucide-react';

export default function SearchBar({ onSearch }) {
  const [query, setQuery] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    if (query.trim()) {
      onSearch(query);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="w-full">
      <div className="relative group">
        {/* Crystal ice container - fully transparent, NO WHITE */}
        <div className="relative backdrop-blur-xl rounded-2xl border border-cyan-300/25 shadow-[0_-4px_24px_0_rgba(34,211,238,0.1)] hover:shadow-[0_-4px_32px_0_rgba(34,211,238,0.2)] hover:border-cyan-300/40 transition-all duration-300">
          {/* Inner glow effect */}
          <div className="absolute inset-0 rounded-2xl bg-gradient-to-r from-cyan-400/0 via-cyan-400/3 to-cyan-400/0 opacity-0 group-hover:opacity-100 transition-opacity duration-300"></div>
          
          <div className="relative flex items-center">
            <Search className="absolute left-6 text-cyan-300/60 group-hover:text-cyan-200/80 transition-colors duration-300" size={22} />
            
            <input
              type="text"
              value={query}
              onChange={(e) => setQuery(e.target.value)}
              placeholder="Search Naheed, Daraz, PandaMart...  (e.g.  soap, rice, milk)"
              className="w-full bg-transparent text-cyan-50 placeholder-cyan-200/30 px-16 py-4 rounded-2xl focus:outline-none focus:border-cyan-300/50 text-base font-light tracking-wide"
              style={{ backgroundColor: 'transparent' }}
            />
            
            <button
              type="submit"
              className="absolute right-3 px-6 py-2.5 bg-cyan-500/25 hover:bg-cyan-500/35 border border-cyan-400/25 hover:border-cyan-400/50 text-cyan-50 rounded-xl transition-all duration-300 font-light text-sm shadow-[0_0_12px_rgba(34,211,238,0.15)] hover:shadow-[0_0_20px_rgba(34,211,238,0.3)] backdrop-blur-sm"
            >
              Search
            </button>
          </div>
        </div>
        
        {/* Top reflection */}
        <div className="absolute inset-x-0 -top-px h-px bg-gradient-to-r from-transparent via-cyan-300/20 to-transparent"></div>
      </div>
    </form>
  );
}