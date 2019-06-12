using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;
using WebApp.Providers;
using WebApp.Results;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Station")]
    public class StationController : ApiController
    {
      

        private IUnitOfWork db;
        // GET: Station
        public StationController()
        {

        }

        public StationController(IUnitOfWork db)
        {
            this.db = db;
        }



        [AllowAnonymous]
        [Route("AddStation")]
        public async Task<IHttpActionResult> AddStation(Station model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var station = new Station() { Address = model.Address, Lines = model.Lines, Name = model.Name, X = model.X, Y = model.Y };

            try
            {
                db.RepositoryStations.Add(station);
                db.Complete();
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}