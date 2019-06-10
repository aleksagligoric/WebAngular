using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Models.BindingModels;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
//	[Authorize]
	[RoutePrefix("api/RedVoznje")]
	public class RedVoznjeController : ApiController
	{
		private IUnitOfWork db;

		private const string LocalLoginProvider = "Local";
		private ApplicationUserManager _userManager;

		public RedVoznjeController() { }

		public RedVoznjeController(IUnitOfWork db)
		{
			this.db = db;
		}

		

		// GET: api/RedVoznje/IspisReda/{timetableTypeId}/{dayTypeId}/{lineId}
		//[ResponseType(typeof(string))]
		[Route("IspisReda/{timetableTypeId}/{dayTypeId}/{lineId}")]
		[HttpGet]
		public IHttpActionResult GetTimetableTimes(int timetableTypeId,int dayTypeId,int lineId) //vraca vremena polaska autobusa iz reda voznji
		{
			Timetable t = new Timetable();
			t = db.RepositoryTimetables.Find(x => x.TimetableTypeId == timetableTypeId && x.DayTypeId == dayTypeId && x.LineId == lineId).FirstOrDefault();

			return Ok(t.Times);
		}

        [Route("IspisCena/{ticketTypeId}/{userTypeId}")]
        [HttpGet]
        public IHttpActionResult GetPrices(int ticketTypeId, int userTypeId) //vraca vremena polaska autobusa iz reda voznji
        {
            Pricelist t = new Pricelist();
            t = db.RepositoryPricelists.Find(x => x.TicketTypeId == ticketTypeId && x.UserTypeId == userTypeId ).FirstOrDefault();

            return Ok(t.Cena);
        } 

        // GET: api/RedVoznje/RedVoznjiInfo
        [ResponseType(typeof(RedVoznjeInfoBindingModel))]
		[Route("RedVoznjiInfo")]
		public IHttpActionResult GetScheduleInfo()
		{
			List<TimetableType> timetableTypes = db.RepositoryTimetableTypes.GetAll().ToList();
			List<Line> lines = db.RepositoryLines.GetAll().ToList();
			List<DayType> dayTypes = db.RepositoryDayTypes.GetAll().ToList();
			RedVoznjeInfoBindingModel s = new RedVoznjeInfoBindingModel() { TimetableTypes = timetableTypes, Lines = lines, DayTypes = dayTypes };

			return Ok(s);
		}

        // GET: api/RedVoznje/RedVoznjiInfo
        [ResponseType(typeof(CenovnikInfo))]
        [Route("cenovnikInfo")]
        public IHttpActionResult GetCenaInfo()
        {
            List<TicketType> TicketcTypes = db.RepositoryTicketTypes.GetAll().ToList();
            List<UserType> UserTypes = db.RepositoryUserTypes.GetAll().ToList();
            CenovnikInfo s = new CenovnikInfo{ userTypes=UserTypes, ticketTypes= TicketcTypes,};

            return Ok(s);
        }


        protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TimetableExist(int id)
		{
			return db.RepositoryTimetables.GetAll().Count(e => e.Id == id) > 0;
		}

	}
}