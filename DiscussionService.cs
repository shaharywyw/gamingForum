using GamingForum.Data;
using GamingForum.Data.Models;
using GamingForum.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamingForum.Service
{
    public class DiscussionService : IDiscussionService
    {
        private readonly ApplicationDbContext _context;

        public DiscussionService(ApplicationDbContext _context)
        {
            this._context = _context;   
        }

        public Discussion GetDiscussion(int discussionId)
        {
            return _context.Discussions
                .Include(discussion=>discussion.Creator)
                .Include(discussion => discussion.Comments)
                    .ThenInclude(comment=>comment.Creator)

                .FirstOrDefault(discussion => discussion.Id == discussionId);
        }

        public async Task<Discussion> Add(Discussion discussion)
        {
            _context.Add(discussion);
            await _context.SaveChangesAsync();
            return discussion;
        }

        public async Task<Discussion> Delete(Discussion discussion)
        {
            _context.Remove(discussion);
            await _context.SaveChangesAsync();
            return discussion;
        }
        public async Task<Comment> Add(Comment comment)
        {
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public IEnumerable<Discussion> GetDiscussions(string searchString,string gameName)
        {
            return _context.Discussions
                .OrderByDescending(discussion => discussion.CreatedOn)
                .Include(discussion => discussion.Creator)
                .Include(discussion => discussion.Comments)
                .Where(Discussion => (Discussion.Title.Contains(searchString) || Discussion.Content.Contains(searchString)) && Discussion.GameName.Contains(gameName));
        }

        public IEnumerable<Discussion> GetDiscussions(ApplicationUser applicationUser)
        {
            return _context.Discussions
                 .Include(discussion => discussion.Creator)
                 .Include(discussion => discussion.Comments)
                 .Where(discussion=>discussion.Creator == applicationUser);
        }

        public Comment GetComment(int commentId)
        {
            return _context.Comments
                .Include(comment => comment.Creator)
                .Include(comment => comment.Discussion)
                .FirstOrDefault(comment=>comment.Id == commentId);
        }
        public async Task<Discussion> Update(Discussion discussion)
        {
            _context.Update(discussion);
            await _context.SaveChangesAsync();
            return discussion;
        }
    }
}
