using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Models.BindingModels;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
   
    [RoutePrefix("api/Tickets")]
    public class TicketsController : ApiController
    {
        private IUnitOfWork db;

        public TicketsController() { }
        public TicketsController(IUnitOfWork db)
        {
            this.db = db;
        }

        [Route("PromeniCenu")]
        public IHttpActionResult PromeniCenu(Pricelist model) //vraca vremena polaska autobusa iz reda voznji
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var list = db.RepositoryPricelists.Find(x => x.TicketTypeId == model.TicketTypeId && x.UserTypeId == model.UserTypeId);

            foreach(var item in list)
            {
                item.Price = model.Price;
                db.RepositoryPricelists.Update(item);
                
                break;
            }
            db.Complete();



            return Ok();
        }

        [Route("IspisCena/{ticketType}/{userType}")]
        [HttpGet]
        public IHttpActionResult IspisCena(int ticketType, int userType) //vraca vremena polaska autobusa iz reda voznji
        {
             var cene = db.RepositoryPricelists.Find(x => x.TicketTypeId == ticketType && x.UserTypeId == userType);
            foreach(var cena in cene)
            {
                return Ok(cena.Price);
            }

            return NotFound();
        }

        [Route("CenovnikInfo")]
        [HttpGet]
        public IHttpActionResult CenovnikInfo() //vraca vremena polaska autobusa iz reda voznji
        {
            List<TicketType> TicketcTypes = db.RepositoryTicketTypes.GetAll().ToList();
            List<UserType> UserTypes = db.RepositoryUserTypes.GetAll().ToList();
            CenovnikInfo s = new CenovnikInfo { userTypes = UserTypes, ticketTypes = TicketcTypes, };

            return Ok(s);
        }


    }
}
