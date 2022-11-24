using GamingForum.BusinessManagers.Interfaces;
using GamingForum.Data.Models;
using GamingForum.Models.AdminViewModels;
using GamingForum.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GamingForum.BusinessManagers
{
    public class AdminBusinessManager : IAdminBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDiscussionService discussionService;
        public AdminBusinessManager(UserManager<ApplicationUser> userManager, IDiscussionService discussionService)
        {
            this.userManager=userManager;  
            this.discussionService=discussionService;   
        }
        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel
            {
                Discussions=discussionService.GetDiscussions(applicationUser)
            };
        }
    }
}
