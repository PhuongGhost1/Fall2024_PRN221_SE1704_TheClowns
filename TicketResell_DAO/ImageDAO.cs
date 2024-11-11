using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketResell_BusinessObject;

namespace TicketResell_DAO
{
    public class ImageDAO
    {
        private readonly TicketResellContext _context;
        public static ImageDAO _instance;
        public ImageDAO(TicketResellContext context)
        {
            _context = context;
        }

        public static ImageDAO GetInstance
        {
            get => _instance = _instance ?? new ImageDAO(new TicketResellContext());
        }

        public async Task<Image> GetImageById(int id)
        {
            return await _context.Images.FindAsync(id);
        }

        public async Task<bool> AddImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateImage(Image image)
        {
            _context.Images.Update(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Image?> GetImageByTicketId(Guid ticketId)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.TicketId == ticketId);
        }
    }
}