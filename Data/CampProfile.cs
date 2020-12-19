using AutoMapper;
using WebApiPSCourse.Data.Entities;
using WebApiPSCourse.Models;

namespace WebApiPSCourse.Data
{
    public class CampProfile : Profile
    {
        // Constructor
        public CampProfile()
        {
            // Get Camp
            // Map from Camp class to Camp model
            this.CreateMap<Camp, CampModel>()
                .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName)) // Map properties from the camp
                                                                                     .ReverseMap();  // .ForAllOtherMembers(x => x.Ignore());

            // Get Talk
            this.CreateMap<Talk, TalkModel>().ReverseMap()
                .ForMember(a => a.Camp, b => b.Ignore()) // From Talkmodel to talk, ignore camp
                .ForMember(a => a.Speaker, b => b.Ignore()); // From Talkmodel to talk, ignore speaker
                                                             // this.CreateMap<TalkModel, Talk>();
                                                             // .IncludeMembers(t => t.Speaker);

            // Get Speaker
            this.CreateMap<Speaker, SpeakerModel>().ReverseMap();
            // this.CreateMap<SpeakerModel, Speaker>();


            // // Create Map for POST camp
            // this.CreateMap<CampModel, Camp>();

            // // CreateMap for Update [PUT]
            // this.CreateMap<CampModel, Camp>()
            //     .ForPath(a => a.Location.VenueName, b => b.MapFrom(c => c.Venue))
            //     .ForPath(a => a.Location.Address1, b => b.MapFrom(c => c.LocationAddress1))
            //     .ForPath(a => a.Location.Address2, b => b.MapFrom(c => c.LocationAddress2))
            //     .ForPath(a => a.Location.Address3, b => b.MapFrom(c => c.LocationAddress3))
            //     .ForPath(a => a.Location.CityTown, b => b.MapFrom(c => c.LocationCityTown))
            //     .ForPath(a => a.Location.StateProvince, b => b.MapFrom(c => c.LocationStateProvince))
            //     .ForPath(a => a.Location.PostalCode, b => b.MapFrom(c => c.LocationPostalCode))
            //     .ForPath(a => a.Location.Country, b => b.MapFrom(c => c.LocationCountry));
        }
    }
}