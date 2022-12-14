namespace TMDB.Services.Movie.Settings
{
    public interface IDatabaseSettings
    {
        public string MovieCollectionName { get; set; }
        public string MovieDetailCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}