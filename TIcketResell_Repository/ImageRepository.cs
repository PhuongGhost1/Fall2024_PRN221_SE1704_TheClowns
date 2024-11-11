using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

namespace TIcketResell_Repository
{
    public interface IImageRepository
    {
        Task<Image> GetImageById(int id);
        Task<bool> AddImage(Image image);
        Task<bool> UpdateImage(Image image);   
        Task<Image?> GetImageByTicketId(Guid ticketId);
    }
    public class ImageRepository : IImageRepository
    {
        public async Task<bool> AddImage(Image image) => await ImageDAO.GetInstance.AddImage(image);

        public async Task<Image> GetImageById(int id) => await ImageDAO.GetInstance.GetImageById(id);

        public async Task<Image?> GetImageByTicketId(Guid ticketId) => await ImageDAO.GetInstance.GetImageByTicketId(ticketId);

        public async Task<bool> UpdateImage(Image image) => await ImageDAO.GetInstance.UpdateImage(image);
    }
}