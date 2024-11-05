using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketResell_BusinessObject;
using TIcketResell_Repository;

namespace TicketResell_Service
{
    public interface ISocialMediaService
    {
        Task<List<SocialMedium>> GetSocialMedias();
        Task<SocialMedium?> GetSocialMediaById(Guid id);
        Task<List<SocialMedium>> GetSocialMediaByUserId(Guid? userId);
        Task<bool> CreateSocialMedia(SocialMedium socialMedia);
        Task<bool> UpdateSocialMediaLinksAsync(Guid userId, string facebookLink, string twitterLink, string instagramLink);
    }
    public class SocialMediaService : ISocialMediaService
    {
        private readonly ISocialMediaRepository _socialMediaRepository;
        public SocialMediaService(ISocialMediaRepository socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        public async Task<bool> CreateSocialMedia(SocialMedium socialMedia) => await _socialMediaRepository.CreateSocialMedia(socialMedia);

        public async Task<SocialMedium?> GetSocialMediaById(Guid id) => await _socialMediaRepository.GetSocialMediaById(id);

        public async Task<List<SocialMedium>> GetSocialMediaByUserId(Guid? userId) => await _socialMediaRepository.GetSocialMediaByUserId(userId);

        public async Task<List<SocialMedium>> GetSocialMedias() => await _socialMediaRepository.GetSocialMedias();

        public async Task<bool> UpdateSocialMediaLinksAsync(Guid userId, string facebookLink, string twitterLink, string instagramLink)
        {
            bool facebookUpdated = await _socialMediaRepository.UpdateOrCreateSocialMediaLink(userId, "Facebook", facebookLink);
            bool twitterUpdated = await _socialMediaRepository.UpdateOrCreateSocialMediaLink(userId, "Twitter", twitterLink);
            bool instagramUpdated = await _socialMediaRepository.UpdateOrCreateSocialMediaLink(userId, "Instagram", instagramLink);

            return facebookUpdated || twitterUpdated || instagramUpdated;
        }
    }
}