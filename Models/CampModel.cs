using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiPSCourse.Models
{
    public class CampModel
    {
        [Required]
        [StringLength(100)] // Max string length of 100
        public string Name { get; set; }

        [Required]
        public string Moniker { get; set; } // What you should see in the URI

        public DateTime EventDate { get; set; } = DateTime.MinValue;

        [Range(1, 100)]
        public int Length { get; set; } = 1;

        /*
            Relating Camp model to Location
            Camp model with location info
            With AutoMapper, Adding Location to the property names maps
            these properties to the Location entity
        */

        // public string LocationVenueName { get; set; }
        public string Venue { get; set; }

        public string LocationAddress1 { get; set; }

        public string LocationAddress2 { get; set; }

        public string LocationAddress3 { get; set; }

        public string LocationCityTown { get; set; }

        public string LocationStateProvince { get; set; }

        public string LocationPostalCode { get; set; }

        public string LocationCountry { get; set; }


        public ICollection<TalkModel> Talks { get; set; }

    }
}