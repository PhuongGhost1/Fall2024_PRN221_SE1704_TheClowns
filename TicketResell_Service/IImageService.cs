using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

namespace TicketResell_Service
{
    public interface IImageService
    {
        Task<Image?> GetImageById(int id);
        Task<bool> AddImage(Image image);
        Task<bool> UpdateImage(Image image);
        Task<Image?> GetImageByTicketId(Guid ticketId);
    }
}
