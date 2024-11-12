using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketResell_BusinessObject;

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
}
