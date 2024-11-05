using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TicketResell_DAO;

namespace TIcketResell_Repository
{
    public interface ISocialMediaRepository
    {
        Task<List<SocialMedium>> GetSocialMedias();
        Task<SocialMedium?> GetSocialMediaById(Guid id);
        Task<List<SocialMedium>> GetSocialMediaByUserId(Guid? userId);
        Task<bool> CreateSocialMedia(SocialMedium socialMedia);
        Task<bool> UpdateOrCreateSocialMediaLink(Guid userId, string type, string link);
    }
    public class SocialMediaRepository : ISocialMediaRepository
    {
        public async Task<bool> CreateSocialMedia(SocialMedium socialMedia) => await SocialMediaDAO.GetInstance.CreateSocialMedia(socialMedia);

        public async Task<SocialMedium?> GetSocialMediaById(Guid id) => await SocialMediaDAO.GetInstance.GetSocialMediaById(id);

        public async Task<List<SocialMedium>> GetSocialMediaByUserId(Guid? userId) => await SocialMediaDAO.GetInstance.GetSocialMediaByUserId(userId);

        public async Task<List<SocialMedium>> GetSocialMedias() => await SocialMediaDAO.GetInstance.GetSocialMedias();

        public async Task<bool> UpdateOrCreateSocialMediaLink(Guid userId, string type, string link) => await SocialMediaDAO.GetInstance.UpdateOrCreateSocialMediaLink(userId, type, link);
    }
}