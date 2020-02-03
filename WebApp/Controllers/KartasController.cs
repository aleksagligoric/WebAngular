using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Kartas")]
    public class KartasController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private string userName;

        public IUnitOfWork Db { get; set; }

        public KartasController(IUnitOfWork db)
        {
            Db = db;
        }


        // GET: api/Kartas
        public IQueryable<Karta> GetKarte()
        {
            return db.Karte;
        }
        [AllowAnonymous]
        [ResponseType(typeof(string))]
        [Route("GetProveri/{IdKorisnika}")]
        public IHttpActionResult GetProveri(string IdKorisnika)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            ApplicationUser u = userManager.FindByName(IdKorisnika);

            Karta karta = new Karta();
            List<Karta> karte = Db.Karta.GetAll().ToList();
            //var user = UserManager.FindByName(IdKorisnika);

            if(u == null)
            {
                return Ok("Kornisnik ne postoji u bazi");
            }

            string odgovor = "";
            foreach(Karta k1 in karte)
            {
                if(k1.ApplicationUserId == u.Id)//user.Id)
                {
                    karta = k1;
                    break;
                }
            }
            if (karta == null)
            {
                return NotFound();
            }
            else
            {
                if (karta.Tip == "Dnevna") //&& (DateTime.UtcNow < karta.VaziDo.AddDays(1)))
                {

                    var datumKarte = karta.VaziDo;
                    var pocetakSledecegDana = new DateTime(datumKarte.Year, datumKarte.Month, datumKarte.AddDays(1).Day);

                    if(pocetakSledecegDana > DateTime.UtcNow)
                    {
                        odgovor = "Vazi vam karta";
                    }
                    else
                    {
                        odgovor = "Ovom korisniku ne vazi karta!";
                    }
                }
                else if (karta.Tip == "Mesecna") //&& (DateTime.UtcNow < karta.VaziDo.AddMonths(1)))
                {
                    var datumKarte = karta.VaziDo;
                    var startOfMonth = new DateTime(datumKarte.Year, datumKarte.Month, 1);
                    var DaysInMonth = DateTime.DaysInMonth(datumKarte.Year, datumKarte.Month);
                    var lastDay = new DateTime(datumKarte.Year, datumKarte.Month, DaysInMonth);

                    if (lastDay > DateTime.UtcNow)
                    {
                        odgovor = "Vazi vam karta";
                    }
                    else
                    {
                        odgovor = "Ovom korisniku ne vazi karta!";
                    }
                }
                else if (karta.Tip == "Godisnja") //&& (DateTime.UtcNow < karta.VaziDo.AddYears(1)))
                {
                    var now = karta.VaziDo;
                    var startOfYear = new DateTime(now.Year, 1, 1);
                    var nextYear = new DateTime(startOfYear.AddYears(1).Year, 1, 1);

                    if (nextYear > DateTime.UtcNow)
                    {
                        odgovor = "Vazi vam karta";
                    }
                    else
                    {
                        odgovor = "Ovom korisniku ne vazi karta!";
                    }
                }
                else if (karta.Tip == "Vremenska") //&& (DateTime.UtcNow < karta.VaziDo.AddHours(1)))
                {

                    var vremeKarte = karta.VaziDo.Hour;
                    var nextHour = vremeKarte + 1;

                    if (nextHour > DateTime.UtcNow.Hour)
                    {
                        odgovor = "Vazi vam karta";
                    }
                    else
                    {
                        odgovor = "Ovom korisniku ne vazi karta!";
                    }
                }
                else
                {
                    odgovor = "Ovom korisniku ne vazi karta!";
                }

            }
            return Ok(odgovor);
        }

        // GET: api/Kartas/5
        [AllowAnonymous]
        [ResponseType(typeof(string))]
        [Route("GetKarta/{tipKarte}/{tipKupca}")]
        public IHttpActionResult GetKartaCena(string tipKarte,string tipKupca)
        {
            List<CenaKarte> karte = Db.CenaKarte.GetAll().ToList();
            List<Cenovnik> cenovnici = Db.Cenovnik.GetAll().ToList();

            Cenovnik cen = Db.Cenovnik.GetAll().Where(t => t.VaziDo > DateTime.UtcNow && t.VaziOd < DateTime.UtcNow).FirstOrDefault();

            string odg = "Cena zeljene karte je : ";
            foreach(CenaKarte k in karte)
            {
                if(k.TipKarte == tipKarte && tipKupca == k.TipKupca && cen.IdCenovnik == k.CenovnikId)
                {
                    odg += k.Cena.ToString();
                }
            }
            odg += " rsd.";
            if (karte == null)
            {
                return NotFound();
            }

            return Ok(odg);
        }
        [AllowAnonymous]
        [ResponseType(typeof(string))]
        [Route("GetKartaPromenaCene/{tipKarte}/{tipKupca}/{cena}")]
        public IHttpActionResult GetKartaCena(string tipKarte, string tipKupca, int cena)
        {
            //POTREBNO JE PRAVITI NOVI CENOVNIK KADA SE PROMENI CENA KARTE
             List<CenaKarte> karte = Db.CenaKarte.GetAll().ToList();
            List<Cenovnik> cenovnici = Db.Cenovnik.GetAll().ToList();
            Cenovnik cen = Db.Cenovnik.GetAll().Where(t => t.VaziDo > DateTime.UtcNow && t.VaziOd < DateTime.UtcNow).FirstOrDefault();

            string odg = "Cena zeljene karte je bila : ";
            foreach (CenaKarte k in karte)
            {
                if (k.TipKarte == tipKarte && tipKupca == k.TipKupca && cen.IdCenovnik == k.CenovnikId)

                {
                    odg += k.Cena.ToString();
                    k.Cena = cena;
                    Db.CenaKarte.Update(k);
                   
                    Db.Complete();
                    break;
                }
            }
            odg += " rsd.";

            if (karte == null)
            {
                return NotFound();
            }
            odg += "Sada je promenjena na : " + cena.ToString() + " rsd.";
        
            return Ok(odg);
        }
        [AllowAnonymous]
        [ResponseType(typeof(Profil))]
        [Route("DobaviUsera")]
        public IHttpActionResult GetUsera()
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var id = User.Identity.GetUserId();
            ApplicationUser u = userManager.FindById(id);
            if (u == null)
            {
                return Ok();
            }
            Profil p = new Profil();
            p.Name = u.Name;
            p.Password = u.Password;
            p.Surname = u.Surname;
            p.Tip = u.Tip;
            p.Datum = u.Datum;
            p.ConfirmPassword = u.ConfirmPassword;
            p.Email = u.Email;
            p.UserName = u.UserName;
            return Ok(p);
        }
        [AllowAnonymous]
        [Route("PromeniProfil")]
        public IHttpActionResult PostKorisnika(RegisterBindingModel model)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var id = User.Identity.GetUserId();
            ApplicationUser u = userManager.FindById(id);
            if (u == null)
            {
                return Ok();
            }
            u.Email = model.Email;
            u.Datum = model.Date;
            u.ConfirmPassword = model.ConfirmPassword;
            u.Password = model.Password;
            u.Name = model.Name;
            u.UserName = model.Email;
            u.Surname = model.Surname;
            u.Tip = model.Tip;
      
            db.Entry(u).State = EntityState.Modified;

            db.SaveChanges();
            return Ok();
        }
        
        [AllowAnonymous]
        [ResponseType(typeof(string))]
        [HttpPost]
        [Route("GetKartaKupi2")]
        public IHttpActionResult GetKarta([FromBody] Dictionary<string, object> map)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            string tipKarte = map["tipKarte"].ToString();
            string mejl = map["mejl"].ToString();

            Karta novaKarta = new Karta();
            string tipKorisnika;
            var id = User.Identity.GetUserId();
            ApplicationUser u = userManager.FindById(id);
          
            if (u == null)
            {
                tipKorisnika = "Obican";
            }
            else
            {
                tipKorisnika = u.Tip;
            }
            float cena;
            string povratna = "";
            List<Cenovnik> cenovnici = Db.Cenovnik.GetAll().ToList();
            Cenovnik cen = Db.Cenovnik.GetAll().Where(t => t.VaziDo > DateTime.UtcNow && t.VaziOd < DateTime.UtcNow && t.Aktuelan == true).FirstOrDefault();



            CenaKarte ck = Db.CenaKarte.GetAll().FirstOrDefault(t => t.TipKarte == tipKarte && t.TipKupca.Equals("Obican"));

           // novaKarta.CenaKarte = ck;
            novaKarta.CenaKarteId = ck.IdCenaKarte;

            novaKarta.Tip = tipKarte;
       
     
            //novaKarta.ApplicationUserId = User.Identity.GetUserId();
            novaKarta.VaziDo = DateTime.UtcNow;
            if (u != null && u.Odobren == true)
            {
                novaKarta.ApplicationUserId = id;
                // novaKarta.ApplicationUser = u;
                //novaKarta.ApplicationUser = userManager.FindById(id);
                // u.Karte.Add(novaKarta);
                cena = ck.Cena;
                povratna = "Uspesno ste kupili " + tipKarte + "-u" + " kartu, po ceni od |" + cena.ToString() + "| rsd, hvala vam, vas gsp!";


                novaKarta.Cekirana = true;
                db.Dispose();

                Db.Karta.Add(novaKarta);

                Db.Complete();
            }
            else if (u != null && u.Odobren == false)
            {
       
                povratna = "Kontrolor vas nije prihvatio";


           
            }
            else if (u == null)
            {
                string email = mejl.Replace('-', '.');
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("ervinisljami@gmail.com");
                    mail.To.Add(mejl);
                    mail.Subject = "Kupili ste kartu.";
                    mail.Body = "Cestitamo kupili ste kartu. Nice.";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("ervinisljami@gmail.com", "McQrlak.3137");
                        smtp.EnableSsl = true;
                        try
                        {
                            smtp.Send(mail);
                        }
                        catch(Exception e)
                        {
                            Trace.TraceInformation(e.Message);
                        }
                    }
                }

                try
                {
                    //client.Send(mail);
                    cena = ck.Cena;
                    povratna = "Uspesno ste kupili " + tipKarte + "-u" + " kartu, po ceni od |" + cena.ToString() + "| rsd, hvala vam, vas gsp!";


                    novaKarta.Cekirana = true;
                    db.Dispose();

                    Db.Karta.Add(novaKarta);

                    Db.Complete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return InternalServerError(e);
                }
            }
      
            if (ck == null)
            {
                return NotFound();
            }
       
            return Ok(povratna);
        }

        // PUT: api/Kartas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKarta(int id, Karta karta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != karta.IdKarte)
            {
                return BadRequest();
            }

            db.Entry(karta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KartaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kartas
        [ResponseType(typeof(Karta))]
        public IHttpActionResult PostKarta(Karta karta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Karte.Add(karta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = karta.IdKarte }, karta);
        }

        // DELETE: api/Kartas/5
        [ResponseType(typeof(Karta))]
        public IHttpActionResult DeleteKarta(int id)
        {
            Karta karta = db.Karte.Find(id);
            if (karta == null)
            {
                return NotFound();
            }

            db.Karte.Remove(karta);
            db.SaveChanges();

            return Ok(karta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KartaExists(int id)
        {
            return db.Karte.Count(e => e.IdKarte == id) > 0;
        }
    }
}