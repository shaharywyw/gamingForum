using GamingForum.BusinessManagers.Interfaces;
using GamingForum.Data.Models;
using GamingForum.Models.HomeViewModels;
using GamingForum.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

namespace GamingForum.BusinessManagers
{
    public class HomeBusinessManager : IHomeBusinessManager
    {
        private readonly IUserService userService;
        private readonly IDiscussionService discussionService;

        public HomeBusinessManager(IUserService userService, IDiscussionService discussionService)
        {
            this.userService = userService;
            this.discussionService = discussionService;
        }

        public ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString, string gameName, int? page)
        {
            if (authorId is null)
                return new BadRequestResult();

            var applicationUser = userService.Get(authorId);

            if (applicationUser is null)
                return new NotFoundResult();

            int pageSize = 100;
            int pageNumber = page ?? 1;

            var discussions = discussionService.GetDiscussions(searchString ?? string.Empty, gameName ?? string.Empty)
                .Where(discussion => discussion.Creator == applicationUser);

            var test=new AuthorViewModel
            {
                Author = applicationUser,
                Discussions = new StaticPagedList<Discussion>(discussions.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, discussions.Count()),
                SearchString = searchString,
                GameName = gameName,
                PageNumber = pageNumber
            };
            return test;
        }
    }
}
