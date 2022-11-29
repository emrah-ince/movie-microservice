using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMDB.Services.UserComment.Dtos
{
    public class CommentDto
    {
        public string UserId { get; set; }
        public string CustomCommentId { get; set; }
        public string Comment { get; set; }
        public int Point { get; set; }
    }
}
