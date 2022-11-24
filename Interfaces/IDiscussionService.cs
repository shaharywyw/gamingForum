
using GamingForum.Data.Models;

namespace GamingForum.Service.Interfaces
{
    public interface IDiscussionService
    {
        Task<Discussion> Add(Discussion discussion);
        Task<Comment> Add(Comment comment);
        Task<Discussion> Delete(Discussion discussion);
        IEnumerable<Discussion> GetDiscussions(ApplicationUser applicationUser);
        Discussion GetDiscussion(int id);
        Task<Discussion> Update(Discussion discussion);
        IEnumerable<Discussion> GetDiscussions(string searchString, string gameName);
        Comment GetComment(int commentId);
    }
}
