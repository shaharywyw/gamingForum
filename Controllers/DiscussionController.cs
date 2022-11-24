using Microsoft.AspNetCore.Mvc;
using GamingForum.Models.DiscussionViewModels;
using GamingForum.BusinessManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GamingForum.Controllers
{
    [Authorize]
    public class DiscussionController : Controller
    {
        private readonly IDiscussionBusinessManager discussionBusinessManager;
        private readonly IAdminBusinessManager adminBusinessManager;
        public DiscussionController(IDiscussionBusinessManager discussionBusinessManager, IAdminBusinessManager adminBusinessManager)
        {
            this.discussionBusinessManager = discussionBusinessManager;
            this.adminBusinessManager = adminBusinessManager;   
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id)
        {
            var actionResult = await discussionBusinessManager.GetDiscussionViewModel(id, User);
            if (actionResult.Result is null)
                return View(actionResult.Value);
            return actionResult.Result;
        }

        public IActionResult Create()
        {
			return View(new CreateViewModel());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var actionResult = await discussionBusinessManager.GetEditViewModel(id, User);
            if (actionResult.Result is null)
                return View(actionResult.Value);
            return actionResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createViewModel)
        {
            if(createViewModel.Discussion.Content == null)
                return RedirectToAction("Create");
            await discussionBusinessManager.CreateDiscussion(createViewModel, User);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            await discussionBusinessManager.DeleteDiscussion(id, User);
            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var actionResult = await discussionBusinessManager.UpdateDiscussion(editViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Edit", new { editViewModel.Discussion.Id });

            return actionResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Comment(DiscussionViewModel discussionViewModel)
        {
            var actionResult = await discussionBusinessManager.CreateComment(discussionViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Index", new { discussionViewModel.Discussion.Id });

            return actionResult.Result;
        }
    }
}
