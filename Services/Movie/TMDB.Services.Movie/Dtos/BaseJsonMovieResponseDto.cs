using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMDB.Services.Movie.Dtos
{
    public class BaseJsonMovieResponseDto<T>
    {
        public int id { get; set; }
        public int page { get; set; }
        public List<T> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
