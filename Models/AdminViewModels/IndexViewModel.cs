using GamingForum.Data.Models;

namespace GamingForum.Models.AdminViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Discussion> Discussions { get; set; }
    }
}
