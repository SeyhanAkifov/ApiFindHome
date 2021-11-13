using ApiFindHome.Dto;
using ApiFindHome.Model;
using AutoMapper;
using System;

namespace ApiFindHome.MappingConfiguration
{
    public class ApplicationProfile : Profile
    {

        public ApplicationProfile()
        {
            this.CreateMap<Property, HomePagePropertyDto>()
               .ForMember(x => x.CityName, y => y.MapFrom(s => s.Address.City.Name))
               .ForMember(x => x.StreetName, y => y.MapFrom(s => s.Address.StreetName))
               .ForMember(x => x.StreetNumber, y => y.MapFrom(s => s.Address.StreetNumber))
               .ForMember(x => x.PostCode, y => y.MapFrom(s => s.Address.PostCode))
               .ForMember(x => x.PropertyType, y => y.MapFrom(s => s.Type.Name))
               .ForMember(x => x.YearsAgo, y => y.MapFrom(s => (DateTime.UtcNow  - s.AddedOn)));

            //this.CreateMap<NewsOutputDto, NewsViewModel>();

            //this.CreateMap<ComingSoonOutputDto, ComingSoonViewModel>();

            //this.CreateMap<DetailsOutputDto, MovieDetailsApiModel>();

            //this.CreateMap<DetailsOutputDto, MovieDetailsViewModel>();

            //this.CreateMap<MovieOutputDto, EditMovieInputModel>();

            //this.CreateMap<MovieOutputDto, MovieViewModel>();

            //this.CreateMap<MovieDetailOutputDto, MovieDetailsViewModel>();

            //this.CreateMap<CinemaProjectionOutputDto, CinemaProjectionViewModel>()
            //    .ForMember(x => x.CinemaHall, y => y.MapFrom(s => s.CinemaHall.Name));

        }
    }
}
