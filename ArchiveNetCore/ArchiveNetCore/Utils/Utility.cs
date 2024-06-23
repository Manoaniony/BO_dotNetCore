using System;
using ArchiveNetCore.Data;
using ArchiveNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ArchiveNetCore.Utils
{
    public class Utility
    {
        private readonly ArchiveNetCoreContext _context;

        public Utility(ArchiveNetCoreContext context)
        {
            _context = context;
        }

        public async Task<List<Articles>> CheckArticlePresenceAsync(int refEntrepot, int niveau, int rayon, int range)
        {
            var liste = await _context.Articles
                .Where(a => a.idEntrepot == refEntrepot && a.niv == niveau && a.rayon == rayon && a.range == range)
                .ToListAsync();
            return liste.Any() ? liste : new List<Articles>();
        }
    }
}

