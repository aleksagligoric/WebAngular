namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using WebApp.Models;

    public partial class slikaMigracija : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.Slikas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ImageUrl = c.String(nullable: false),
                    Korisnik = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Slikas", new[] { "Id" });
            DropTable("dbo.Slikas");
        }
    }
}
