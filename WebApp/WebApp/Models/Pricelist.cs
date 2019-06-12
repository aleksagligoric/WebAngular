using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Pricelist
    {
        public int Id { get; set; }
      
        public int? TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }


        public int? UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public int Price { get; set; }


    }
}