﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IUnitOfWork Db { get; set; }
        public ValuesController(IUnitOfWork db)
        {
            Db = db;
        
        }
        public ValuesController()
        {
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [AllowAnonymous]
 
        [Route("GetZahtevi")]
        public List<string> GetValues()
        {
            IQueryable<ApplicationUser> acounti;
            acounti = db.Users.AsQueryable();
            List<string> usernameovi = new List<string>();
            foreach(ApplicationUser a in acounti)
            {
                if(!a.Odobren)
                usernameovi.Add(a.UserName);
            }
            return usernameovi;
        }
        // POST api/values
        [AllowAnonymous]
        [Route("Odobri")]
        [HttpPost]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetValues([FromBody] Dictionary<string, string> map)
        {
            IQueryable<ApplicationUser> acounti;
            acounti = db.Users.AsQueryable();
            string mejl = map["mail"];
            ApplicationUser app = new ApplicationUser();
            foreach (ApplicationUser a in acounti)
            {
                if(a.UserName == mejl)
                {
                    app = a;
                }
            }
            app.Odobren = true;
            db.Entry(app).State = EntityState.Modified;

            db.SaveChanges();
           
            string email = mejl.Replace('-', '.');
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("ervinisljami@gmail.com");
                mail.To.Add(mejl);
                mail.Subject = "Odobrenje profila.";
                mail.Body = "Postovani.\n\nVas nalog je upravo odobren. Sada mozete kupovati karte. Vas GSP.";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("ervinisljami@gmail.com", "McQrlak.3137");
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (Exception e)
                    {
                        Trace.TraceInformation(e.Message);
                    }
                }
            }

            return Ok("Odobrili ste mu registraciju!");
        }

        [AllowAnonymous]

        [Route("Verifikovan")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetVerifikovan()
        {
            IQueryable<ApplicationUser> acounti;
            acounti = db.Users.AsQueryable();
            ApplicationUser app = new ApplicationUser();
            var id = User.Identity.GetUserId();

            foreach (ApplicationUser a in acounti)
            {
                if (a.Id == id)
                {
                    app = a;
                    break;
                }
            }

            if (app.Odobren)
                return Ok("Verifikovan");
            else
                return Ok("Nije Verifikovan");
            
            //return Ok("Odobrili ste mu kupovinu!");
        }
    }
}
