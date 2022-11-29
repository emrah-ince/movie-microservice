using System.Collections.Generic;
using System.Linq;

namespace TMDB.Services.UserComment.Dtos
{
    public class CustonCommentDto
    {
        public string MovieId { get; set; }
        //public List<CommentDto> Comments { get; set; }


        public string UserId { get; set; }
        //public string CustomCommentId { get; set; }
        public string Comment { get; set; }
        public int Point { get; set; }

        //public double AvgPoint
        //{
        //    get => Comments.Average(x => x.Point);
        //}
    }
}