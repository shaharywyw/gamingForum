using GamingForum.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GamingForum.BusinessManagers.Interfaces
{
    public interface IHomeBusinessManager
    {
        ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString,string gameName, int? page);
    }
}
