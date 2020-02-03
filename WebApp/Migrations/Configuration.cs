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
                System.Diagnostics.Debugger.Launch();
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

            Slika slika = new Slika();
            slika.ImageUrl = "C:/Users/ervin/OneDrive/Desktop/WebAngular/WebApp/SlikeKorisnika/Penzioni-Cek.png";
            slika.Korisnik = user2.Id;

            try
            {
                context.Slike.Add(slika);
            }
            catch(Exception e)
            {
                Trace.TraceInformation(e.Message);
            }
            
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
