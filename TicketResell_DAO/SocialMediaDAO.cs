using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class SocialMediaDAO
    {
        private readonly TicketResellContext _context;
        public static SocialMediaDAO _instance;

        public SocialMediaDAO(TicketResellContext context)
        {
            _context = context;
        }

        public static SocialMediaDAO GetInstance
        {
            get => _instance = _instance ?? new SocialMediaDAO(new TicketResellContext());
        }

        public async Task<List<SocialMedium>> GetSocialMedias()
        {
            return await _context.SocialMedia.ToListAsync();
        }

        public async Task<SocialMedium?> GetSocialMediaById(Guid id)
        {
            return await _context.SocialMedia.FindAsync(id);
        }

        public async Task<List<SocialMedium>> GetSocialMediaByUserId(Guid? userId)
        {
            return await _context.SocialMedia.Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<bool> CreateSocialMedia(SocialMedium socialMedia)
        {
            _context.SocialMedia.Add(socialMedia);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrCreateSocialMediaLink(Guid userId, string type, string link)
        {
            var socialMedia = await _context.SocialMedia.FirstOrDefaultAsync(s => s.UserId == userId && s.Type == type);
            if (socialMedia != null)
            {
                socialMedia.Link = link;
                _context.SocialMedia.Update(socialMedia);
            }
            else
            {
                socialMedia = new SocialMedium
                {
                    UserId = userId,
                    Type = type,
                    Link = link,
                    Status = "Active" 
                };
                await _context.SocialMedia.AddAsync(socialMedia);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}