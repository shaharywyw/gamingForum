using GamingForum.Authorization;
using GamingForum.BusinessManagers.Interfaces;
using GamingForum.Data.Models;
using GamingForum.Models.DiscussionViewModels;
using GamingForum.Models.HomeViewModels;
using GamingForum.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System.Security.Claims;


namespace GamingForum.BusinessManagers
{
    public class DiscussionBusinessManager : IDiscussionBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDiscussionService discussionService;
        private readonly IAuthorizationService authorizationService;
        public DiscussionBusinessManager(UserManager<ApplicationUser> userManager, IDiscussionService discussionService, IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.discussionService = discussionService;
            this.authorizationService = authorizationService;
        }

        public async Task<ActionResult<DiscussionViewModel>> GetDiscussionViewModel(int? id,ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var discussionId = id.Value;
            var discussion = discussionService.GetDiscussion(discussionId);

            if (discussion is null)
                return new NotFoundResult();

            return new DiscussionViewModel
            {
                Discussion = discussion
            };
        }
        public IndexViewModel GetIndexViewModel(string searchString,string gameName,int? page)
        {
            int pageSize = 8;
            int pageNumber = page ?? 1;
            var discussions = discussionService.GetDiscussions(searchString ?? string.Empty, gameName ?? string.Empty);

            return new IndexViewModel
            {
                Discussions = new StaticPagedList<Discussion>(discussions.Skip((pageNumber -1)* pageSize).Take(pageSize), pageNumber, pageSize, discussions.Count()),
                SearchString = searchString,
                GameName= gameName,
                PageNumber = pageNumber
            };
        }
        public async Task<Discussion> CreateDiscussion(CreateViewModel createViewDiscussion, ClaimsPrincipal claimsPrinciple)
        {
            Discussion discussion = createViewDiscussion.Discussion;

            discussion.Creator= await userManager.GetUserAsync(claimsPrinciple);
            discussion.CreatedOn = DateTime.Now;
            discussion = await discussionService.Add(discussion);

            return discussion;
        }

        public async Task<ActionResult<EditViewModel>> DeleteDiscussion(int ? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var discussionId = id.Value;
            var discussion = discussionService.GetDiscussion(discussionId);

            if (discussion is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, discussion, Operations.Update);

            if (!authorizationResult.Succeeded)
                return DetermineActionResult(claimsPrincipal);

            await discussionService.Delete(discussion);
            return new EditViewModel();
        }
        public async Task<ActionResult<Comment>> CreateComment(DiscussionViewModel discussionViewModel, ClaimsPrincipal claimsPrincipal)
        {
            if (discussionViewModel.Discussion is null || discussionViewModel.Discussion.Id == 0)
                return new BadRequestResult();
            
            var discussion = discussionService.GetDiscussion(discussionViewModel.Discussion.Id);

            if (discussion == null)
                return new NotFoundResult();

            var comment = discussionViewModel.Comment;

            comment.Creator = await userManager.GetUserAsync(claimsPrincipal);
            comment.Discussion = discussion;
            comment.CreatedOn = DateTime.Now;

            return await discussionService.Add(comment);
        }
        public async Task<ActionResult<EditViewModel>> UpdateDiscussion(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal)
        {
        
            var discussion = discussionService.GetDiscussion(editViewModel.Discussion.Id);

            if (discussion is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, discussion, Operations.Update);

            if (!authorizationResult.Succeeded)
                return DetermineActionResult(claimsPrincipal);

            discussion.Title = editViewModel.Discussion.Title;
            discussion.Content = editViewModel.Discussion.Content;
            discussion.CreatedOn = DateTime.Now;

            return new EditViewModel
            {
                Discussion = await discussionService.Update(discussion)
            };
        }

        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var discussionId = id.Value;
            var discussion = discussionService.GetDiscussion(discussionId);

            if (discussion is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, discussion, Operations.Update);

            if (!authorizationResult.Succeeded)
                return DetermineActionResult(claimsPrincipal);

            return new EditViewModel
            {
                Discussion = discussion
            };
        }

        private ActionResult<EditViewModel> DetermineActionResult(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }



    }
}
