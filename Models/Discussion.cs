using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingForum.Data.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string GameName { get; set; }
        public DateTime CreatedOn { get; set; }
        public ApplicationUser Creator { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
        
    }
}
