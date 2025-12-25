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
    <form onSubmit={handleSubmit} className="w-full max-w-2xl">
      <div className="bg-white/10 backdrop-blur-md p-2 rounded-2xl border border-white/20 shadow-2xl">
        <div className="flex items-center bg-white rounded-xl px-4 py-3">
          <Search className="text-slate-400 mr-3" />
          <input 
            type="text" 
            placeholder="Search Naheed, Daraz, PandaMart... (e.g. soap, rice, milk)"
            className="w-full outline-none text-slate-800 text-lg"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
          />
          <button 
            type="submit"
            className="bg-cyan-600 hover:bg-cyan-500 text-white px-6 py-2 rounded-lg font-bold transition-all"
          >
            Search
          </button>
        </div>
      </div>
    </form>
  );
}
