namespace TMDB.Services.Movie.Dtos
{
    public class MovieDetailDto
    {
        public string Id { get; set; }

        public string PosterPath { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public string Title { get; set; }
        public string OriginalLanguage { get; set; }
        public string BackdropPath { get; set; }

        //public double popularity { get; set; }
        public int VoteCount { get; set; }

        public bool Video { get; set; }
        public double VoteAverage { get; set; }

        public string MovieId { get; set; }

        public MovieDto Movie { get; set; }
    }
}