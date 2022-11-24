using GamingForum.Data.Models;
using PagedList.Core;

namespace GamingForum.Models.HomeViewModels
{
    public class IndexViewModel
    {
        public IPagedList<Discussion> Discussions { get; set; } 
        public string? SearchString { get; set; }
        public string? GameName { get; set; }
        public int PageNumber { get; set; }
    }
}
