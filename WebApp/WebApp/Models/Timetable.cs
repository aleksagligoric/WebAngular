using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Timetable
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "TimetableTypeId")]
        public int TimetableTypeId { get; set; }

        [Required]
        [Display(Name = "DayTypeId")]
        public int DayTypeId { get; set; }

        [Required]
        [Display(Name = "LineId")]
        public int LineId { get; set; }

        [Required]
        [Display(Name = "Times")]
        public string Times { get; set; }




        public DayType DayType { get; set; }

        public TimetableType TimetableType { get; set; }
		public Line Line { get; set; }

    }
}