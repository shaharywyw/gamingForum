using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingForum.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public Discussion Discussion { get; set; }
        public ApplicationUser Creator { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
