using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.BindingModels
{
    public class CenovnikInfo
    {
        public List<UserType> userTypes { get; set; }
        public List<TicketType> ticketTypes { get; set; }
    }
}