using GamingForum.Models.AdminViewModels;
using System.Security.Claims;

namespace GamingForum.BusinessManagers.Interfaces
{
    public interface IAdminBusinessManager
    {
        Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal);
    }
}
