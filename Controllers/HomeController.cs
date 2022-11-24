using GamingForum.BusinessManagers.Interfaces;
using GamingForum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GamingForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDiscussionBusinessManager discussionBusinessManager;
        private readonly IHomeBusinessManager homeBusinessManager;
        public HomeController(IDiscussionBusinessManager discussionBusinessManager, IHomeBusinessManager homeBusinessManager)
        {
            this.discussionBusinessManager = discussionBusinessManager;
            this.homeBusinessManager = homeBusinessManager;
        }

        public IActionResult Index(string searchString, string gameName, int? page)
        {
            return View(discussionBusinessManager.GetIndexViewModel(searchString, gameName, page));
        }

        public IActionResult Author(string authorId, string searchString,string gameName, int? page)
        {
            var actionResult = homeBusinessManager.GetAuthorViewModel(authorId, searchString, gameName, page);
            if (actionResult is null)
                return View(actionResult.Value);
            if (actionResult.Result is null)
                return View(actionResult.Value);
            return actionResult.Result;
        }
    }
}