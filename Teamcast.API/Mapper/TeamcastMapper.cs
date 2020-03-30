using AutoMapper;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.DTOs;
using Teamcast.DTOs.Team.IN;
using Teamcast.DTOs.Team.OUT;
using Teamcast.DTOs.TeamMember.OUT;
using Teamcast.Models;

namespace Teamcast.Mapper
{
    public class TeamcastMapper : Profile
    {
        public TeamcastMapper()
        {
            //User mappings
            CreateMap<User, Login>().AfterMap((src, dest) => dest.Username = dest.Username.ToLower());
            CreateMap<User, UserUpdate>().ReverseMap().AfterMap((src, dest) => dest.Username = dest.Username.ToLower());
            CreateMap<User, UserRegister>().ReverseMap().AfterMap((src, dest) => dest.Username = dest.Username.ToLower());

            //Event mappings
            CreateMap<Event, EventCreate>().ReverseMap().AfterMap((src, dest) => 
                dest.Location = new Point(src.Longitude, src.Latitude)
                {
                    SRID = 4326 //spatial reference system used by Google maps (WGS84)
                } 
            );
            CreateMap<Event, EventUpdate>().ReverseMap().AfterMap((src, dest) => dest.LastUpdated = DateTime.Now);

            //Event nested mapping
            CreateMap<User, UserDto>()
                .BeforeMap((src, dest) => dest.Age = CalculateAge(src.DateOfBirth));
            CreateMap<EventMember, EventMemberDto>()
                .ForMember(dest => dest.User, opts => opts.Condition(src => {
                    if (src.Teams == null)
                        return true;
                    else
                        return false;
                }))
                .ForMember(dest => dest.Teams, opts => opts.MapFrom(src => src.Teams));
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.EventMember, opts => opts.MapFrom(src => src.EventMember))
                .ForMember(dest => dest.EventOwner, opts => opts.MapFrom(src => src.User));

            //Team mappings
            CreateMap<Team, TeamCreate>().ReverseMap();
            CreateMap<Team, TeamUpdate>().ReverseMap().AfterMap((src, dest) => dest.LastUpdated = DateTime.Now);

            //Team nested mapping
            CreateMap<User, UserDto>()
                .BeforeMap((src, dest) => dest.Age = CalculateAge(src.DateOfBirth));
            CreateMap<TeamMember, TeamMemberDto>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.User));
            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.TeamMembers, opts => opts.MapFrom(src => src.TeamMembers))
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.User));
        }

        public int CalculateAge(DateTime dob)
        {
            var age = DateTime.Now.Year - dob.Year;
            if (dob.Date > DateTime.Now.AddYears(-age)) age--;

            return age;
        }
    }
}
