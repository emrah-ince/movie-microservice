using AutoMapper;
using TMDB.Services.Movie.Dtos;

namespace TMDB.Services.Movie.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Model.Movie, MovieDto>().ReverseMap();
            CreateMap<Model.MovieDetail, MovieDetailDto>().ReverseMap();
            CreateMap<Model.Movie, MovieCreateDto>().ReverseMap();
            CreateMap<Model.MovieDetail, MovieDetailCreateDto>().ReverseMap();

            CreateMap<JsonMovieResponseDto, Model.Movie>()
                .ForMember(destination => destination.OriginalTitle, operation => operation.MapFrom(source => source.original_title));

            CreateMap<JsonMovieResponseDto, Model.MovieDetail>()
               .ForMember(destination => destination.PosterPath, operation => operation.MapFrom(source => source.poster_path))
               .ForMember(destination => destination.ReleaseDate, operation => operation.MapFrom(source => source.release_date))
               .ForMember(destination => destination.OriginalLanguage, operation => operation.MapFrom(source => source.original_language))
               .ForMember(destination => destination.BackdropPath, operation => operation.MapFrom(source => source.backdrop_path))
               .ForMember(destination => destination.VoteCount, operation => operation.MapFrom(source => source.vote_count))
               .ForMember(destination => destination.VoteAverage, operation => operation.MapFrom(source => source.vote_average));
        }
    }
}