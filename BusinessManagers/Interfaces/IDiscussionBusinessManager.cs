using GamingForum.Data.Models;
using GamingForum.Models.DiscussionViewModels;
using GamingForum.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamingForum.BusinessManagers.Interfaces
{
    public interface IDiscussionBusinessManager
    {
        Task<Discussion> CreateDiscussion(CreateViewModel createDiscussionViewModel, ClaimsPrincipal claimsPrinciple);
        Task<ActionResult<Comment>> CreateComment(DiscussionViewModel discussionViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> UpdateDiscussion(EditViewModel editViewDiscussion, ClaimsPrincipal claimsPrincipal);
        IndexViewModel GetIndexViewModel(string searchString, string gameName, int? page);
        Task<ActionResult<DiscussionViewModel>> GetDiscussionViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> DeleteDiscussion(int? id, ClaimsPrincipal claimsPrinciple);
    }
}
