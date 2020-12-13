using AutoMapper;
using WebApiPSCourse.Data.Entities;
using WebApiPSCourse.Models;

namespace WebApiPSCourse.Data
{
    public class TalkProfile : Profile
    {
        public TalkProfile()
        {
            this.CreateMap<Talk, TalkModel>();
        }
    }
}