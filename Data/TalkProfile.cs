// using AutoMapper;
// using WebApiPSCourse.Data.Entities;
// using WebApiPSCourse.Models;

// namespace WebApiPSCourse.Data
// {
//     public class TalkProfile : Profile
//     {
//         public TalkProfile()
//         {
//             this.CreateMap<Talk, TalkModel>()
//                 .ReverseMap();

//             // this.CreateMap<TalkModel, Talk>();


//             // this.CreateMap<Talk, TalkModel>().ReverseMap()
//             //     .ForMember(a => a.Title, b => b.MapFrom(c => c.Title));

//             this.CreateMap<Speaker, SpeakerModel>();
//             this.CreateMap<SpeakerModel, Speaker>();
//         }
//     }
// }