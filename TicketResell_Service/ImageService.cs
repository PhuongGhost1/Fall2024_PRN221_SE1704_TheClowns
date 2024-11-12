using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<bool> AddImage(Image image) => await _imageRepository.AddImage(image);

        public async Task<Image?> GetImageById(int id) => await _imageRepository.GetImageById(id);

        public async Task<Image?> GetImageByTicketId(Guid ticketId) => await _imageRepository.GetImageByTicketId(ticketId);
        public async Task<bool> UpdateImage(Image image) => await _imageRepository.UpdateImage(image);
    }
}