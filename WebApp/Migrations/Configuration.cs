namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using WebApp.Models;
    using WebApp.Persistence;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        private void PopulateUserRoles(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Kontrolor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Kontrolor" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser" };

                manager.Create(role);
            }
        }

        private void PopulateUsers(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin1@yahoo.com"))
            {
                var user = new ApplicationUser()
                {
                    Id = "admin1",
                    UserName = "admin1@gmail.com",
                    Email = "admin1@gmail.com",
                    PasswordHash = ApplicationUser.HashPassword("admin"),
                    ConfirmPassword = ApplicationUser.HashPassword("admin"),
                    Tip = "Admin",
                    Name = "Ervin",
                    Surname = "Isljami",
                    Datum = DateTime.Now.ToString(),
                    EmailConfirmed = true,
                    Odobren = true,
                    Password = "admin"
                };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName == "admin2@yahoo.com"))
            {
                var user = new ApplicationUser()
                {
                    Id = "admin2",
                    UserName = "admin2@gmail.com",
                    Email = "admin2@gmail.com",
                    PasswordHash = ApplicationUser.HashPassword("admin"),
                    ConfirmPassword = ApplicationUser.HashPassword("admin"),
                    Tip = "Admin",
                    Name = "Aleksa",
                    Surname = "Gligoric",
                    Datum = DateTime.Now.ToString(),
                    EmailConfirmed = true,
                    Odobren = true,
                    Password = "admin"
                };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");

            }
            if (!context.Users.Any(u => u.UserName == "controller@gmail.com"))
            {  
                var user = new ApplicationUser()
                {
                    Id = "kontrolor1",
                    UserName = "kontrolor@gmail.com",
                    Email = "kontrolor@gmail.com",
                    PasswordHash = ApplicationUser.HashPassword("kontrolor"),
                    ConfirmPassword = ApplicationUser.HashPassword("kontrolor"),
                    Tip = "Kontrolor",
                    Name = "Aleksa",
                    Surname = "Gligoric",
                    Datum = DateTime.Now.ToString(),
                    EmailConfirmed = true,
                    Odobren = true,
                    Password = "admin"
                };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Kontrolor");
            }

        }
        
        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                //System.Diagnostics.Debugger.Launch();
            }

            Cenovnik cenovnik = new Cenovnik();
            cenovnik.CeneKarti = new List<CenaKarte>();
            PopulateUserRoles(context);
            PopulateUsers(context);
            

            CenaKarte cenaKarte = new CenaKarte();
            CenaKarte cenaKarteVre = new CenaKarte();
            CenaKarte cenaKarteVre1 = new CenaKarte();
            CenaKarte cenaKarteVre2 = new CenaKarte();
            CenaKarte cenaKarte2 = new CenaKarte();
            CenaKarte cenaKarte3 = new CenaKarte();
            CenaKarte cenaKarte4 = new CenaKarte();
            CenaKarte cenaKarte2p = new CenaKarte();
            CenaKarte cenaKarte3p = new CenaKarte();
            CenaKarte cenaKarte4p = new CenaKarte();
            CenaKarte cenaKarte5 = new CenaKarte();
            CenaKarte cenaKarte6 = new CenaKarte();
            CenaKarte cenaKarte7 = new CenaKarte();

            cenaKarte.Karte = new List<Karta>();
            cenaKarte2.Karte = new List<Karta>();
            cenaKarte3.Karte = new List<Karta>();
            cenaKarte4.Karte = new List<Karta>();
            cenaKarte.Cena = 90;
            cenaKarte2.Cena = 4500;
            cenaKarte3.Cena = 900;
            cenaKarte4.Cena = 100;
            cenaKarte2p.Cena = 400;
            cenaKarte3p.Cena = 800;
            cenaKarte4p.Cena = 80;
            cenaKarte5.Cena = 5000;
            cenaKarte6.Cena = 1000;
            cenaKarteVre2.Cena = 50;
            cenaKarteVre1.Cena = 45;
            cenaKarteVre.Cena = 40;
            cenovnik.VaziDo = DateTime.UtcNow;
            cenovnik.VaziOd = DateTime.UtcNow.AddDays(15);
            cenovnik.Aktuelan = true;

            cenaKarte.TipKarte = "Dnevna";
            cenaKarte.TipKupca = "Student";
            cenaKarte2.TipKarte = "Godisnja";
            cenaKarte2.TipKupca = "Student";
            cenaKarte3.TipKarte = "Mesecna";
            cenaKarte3.TipKupca = "Student";
            cenaKarte4.TipKarte = "Dnevna";
            cenaKarte4.TipKupca = "Obican";
            cenaKarte5.TipKarte = "Godisnja";
            cenaKarte5.TipKupca = "Obican";
            cenaKarte6.TipKarte = "Mesecna";
            cenaKarte6.TipKupca = "Obican";
            cenaKarteVre.TipKarte = "Vremenska";
            cenaKarteVre.TipKupca = "Obican";
            cenaKarteVre1.TipKarte = "Vremenska";
            cenaKarteVre1.TipKupca = "Student";
            cenaKarteVre2.TipKarte = "Vremenska";
            cenaKarteVre2.TipKupca = "Penzioner";
            cenaKarte2p.TipKarte = "Godisnja";
            cenaKarte2p.TipKupca = "Penzioner";
            cenaKarte3p.TipKarte = "Mesecna";
            cenaKarte3p.TipKupca = "Penzioner";
            cenaKarte4p.TipKarte = "Dnevna";
            cenaKarte4p.TipKupca = "Penzioner";
            cenovnik.CeneKarti.Add(cenaKarte);
            cenovnik.CeneKarti.Add(cenaKarte2);
            cenovnik.CeneKarti.Add(cenaKarte3);
            cenovnik.CeneKarti.Add(cenaKarte4);
            cenovnik.CeneKarti.Add(cenaKarte2p);
            cenovnik.CeneKarti.Add(cenaKarte3p);
            cenovnik.CeneKarti.Add(cenaKarte4p);
            cenovnik.CeneKarti.Add(cenaKarte5);
            cenovnik.CeneKarti.Add(cenaKarte6);
            cenovnik.CeneKarti.Add(cenaKarteVre);
            cenovnik.CeneKarti.Add(cenaKarteVre1);
            cenovnik.CeneKarti.Add(cenaKarteVre2);
            
            Karta kartaDnevna = new Karta();
            kartaDnevna.Tip = "Dnevna";
            kartaDnevna.IdKarte = 1;
            kartaDnevna.CenaKarte = cenaKarte;
            cenaKarte.Karte.Add(kartaDnevna);

            try
            {

                using (StreamReader r = new StreamReader("sve.json"))
                {
                    string json = "", linijaPodela = "";
                    string[] linije;
                    //Stanica s = new Stanica();
                    //Linija l = new Linija();

                    while ((json = r.ReadLine()) != null)
                    {
                        string[] linijaNiz;
                        Stanica s = new Stanica();
                        //json = r.ReadLine();
                        linijaPodela = json.Split('|')[0];
                        linijaNiz = linijaPodela.Split(',', '[', ']');
                        s.Adresa = json.Split('|')[3];
                        string brojX = json.Split('|')[1];
                        string brojY = json.Split('|')[2];
                        s.X = double.Parse(brojX);
                        s.Y = double.Parse(brojY);
                        s.Naziv = s.Adresa = json.Split('|')[3];
                        s.Linije = new List<Linija>();
                        bool stanicaPostoji = false;
                        List<Linija> stanLinije = new List<Linija>();

                        foreach (var lin in linijaNiz)
                        {
                            if (lin != "" && lin != "    \"")
                            {
                                Linija l = new Linija() { RedniBroj = lin };
                                List<Linija> sveLinije = context.Linije.ToList();
                                bool linijaPostoji = false;

                                foreach (var linija in sveLinije)
                                {
                                    if (linija.RedniBroj == lin)
                                    {
                                        l = null;
                                        l = linija;
                                        linijaPostoji = true;
                                        break;
                                    }
                                }

                                if (linijaPostoji)
                                {
                                    List<Stanica> sveStanice = context.Stanice.ToList();
                                    foreach (var stanica in sveStanice)
                                    {
                                        if (stanica.Adresa == s.Adresa && stanica.X == s.X && stanica.Y == s.Y)
                                        {
                                            s = null;
                                            s = stanica;
                                            stanicaPostoji = true;
                                            break;
                                        }
                                    }

                                    if (stanicaPostoji)
                                    {
                                        l.Stanice.Add(s);
                                        s.Linije.Add(l);
                                        context.Set<Stanica>().Attach(s);
                                        context.Entry(s).State = EntityState.Modified;
                                        context.Set<Linija>().Attach(l);
                                        context.Entry(l).State = EntityState.Modified;
                                        //continue;
                                    }
                                    else
                                    {
                                        l.Stanice.Add(s);
                                        s.Linije.Add(l);
                                        context.Set<Stanica>().Attach(s);
                                        context.Entry(s).State = EntityState.Modified;
                                        context.Set<Linija>().Attach(l);
                                        context.Entry(l).State = EntityState.Modified;
                                    }

                                }
                                else
                                {
                                    List<Stanica> sveStanice = context.Stanice.ToList();
                                    foreach (var stanica in sveStanice)
                                    {
                                        if (stanica.Adresa == s.Adresa)
                                        {
                                            s = null;
                                            s = stanica;
                                            stanicaPostoji = true;
                                            break;
                                        }
                                    }

                                    if (stanicaPostoji)
                                    {
                                        s.Linije.Add(l);
                                        context.Set<Stanica>().Attach(s);
                                        context.Entry(s).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        s.Linije = new List<Linija>();
                                        s.Linije.Add(l);
                                        context.Set<Stanica>().Attach(s);
                                        context.Entry(s).State = EntityState.Modified;
                                    }
                                    //s.Linije.Add(l);
                                    l.Stanice = new List<Stanica>();
                                    l.Stanice.Add(s);
                                    context.Linije.Add(l);
                                }
                            }
                        }

                        bool saveFailed;
                        do
                        {
                            saveFailed = false;

                            try
                            {
                                context.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException ex)
                            {
                                saveFailed = true;

                                // Update the values of the entity that failed to save from the store
                                ex.Entries.Single().Reload();
                            }

                        } while (saveFailed);
                    }
                }

                var lajne = context.Linije;
                foreach (var lajna in lajne)
                {
                    RedVoznje rv1 = new RedVoznje();
                    rv1.DanUNedelji = "RADNI";
                    rv1.Polasci = "0430 \n0500 18 36 54\n0612 30 48\n0706 21 35 47";
                    RedVoznje rv2 = new RedVoznje();
                    rv2.DanUNedelji = "SUBOTA";
                    rv2.Polasci = "0430 \n0500 18 36 54\n0612 30 48\n0706 21 35 47";
                    RedVoznje rv3 = new RedVoznje();
                    rv3.DanUNedelji = "NEDELJA";
                    rv3.Polasci = "0430 \n0500 18 36 54\n0612 30 48\n0706 21 35 47";
                    if (lajna.RedoviVoznje == null)
                    {
                        lajna.RedoviVoznje = new List<RedVoznje>();
                    }
                    lajna.RedoviVoznje.Add(rv3);
                    lajna.RedoviVoznje.Add(rv1);
                    lajna.RedoviVoznje.Add(rv2);

                    context.Set<Linija>().Attach(lajna);
                    context.Entry(lajna).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Error while update: " + e.Message);
            }
            

            context.Karte.Add(kartaDnevna);
            context.CeneKarti.Add(cenaKarte);
            context.CeneKarti.Add(cenaKarte2);
            context.CeneKarti.Add(cenaKarte3);
            context.CeneKarti.Add(cenaKarte4);
            context.CeneKarti.Add(cenaKarte2p);
            context.CeneKarti.Add(cenaKarte3p);
            context.CeneKarti.Add(cenaKarte4p);
            context.CeneKarti.Add(cenaKarte5);
            context.CeneKarti.Add(cenaKarte6);
            context.CeneKarti.Add(cenaKarteVre);
            context.CeneKarti.Add(cenaKarteVre1);
            context.CeneKarti.Add(cenaKarteVre2);
            context.Cenovnici.Add(cenovnik);

            ApplicationUser admin = new ApplicationUser();
            admin.UserName = "admin@admin.com";
            admin.Password = "admin";
            admin.Tip = "admin";
            admin.Datum = DateTime.Now.ToString();
            admin.Name = "Eki";
            admin.Surname = "Lima";
            admin.ConfirmPassword = "admin";
            admin.Odobren = true;


            ApplicationUser user1 = new ApplicationUser();
            user1.UserName = "admin@admin.com";
            user1.Password = "admin";
            user1.Tip = "admin";
            user1.Datum = DateTime.Now.ToString();
            user1.Name = "Eki";
            user1.Surname = "Lima";
            user1.ConfirmPassword = "admin";
            user1.Odobren = true;


            ApplicationUser user2 = new ApplicationUser();
            user2.UserName = "admin@admin.com";
            user2.Password = "admin";
            user2.Tip = "admin";
            user2.Datum = DateTime.Now.ToString();
            user2.Name = "Eki";
            user2.Surname = "Lima";
            user2.ConfirmPassword = "admin";
            user2.Odobren = true;
            //context.Users.Add(admin);
            //context.Users.Add(user1);
            //context.Users.Add(user2);
            

            try
            {
                context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}