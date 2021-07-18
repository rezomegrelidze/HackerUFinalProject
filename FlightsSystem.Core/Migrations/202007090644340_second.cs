namespace FlightsSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AirlineCompanies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AirlineName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        CountryCode = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryCode, cascadeDelete: true)
                .Index(t => t.CountryCode);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        CreditCardNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AirlineCompanyId = c.Long(nullable: false),
                        OriginCountryCode = c.Long(nullable: false),
                        DestinationCountryCode = c.Long(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        LandingTime = c.DateTime(nullable: false),
                        RemainingTickets = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AirlineCompanies", t => t.AirlineCompanyId, cascadeDelete: true)
                .Index(t => t.AirlineCompanyId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FlightId = c.Long(nullable: false),
                        CustomerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Flights", t => t.FlightId, cascadeDelete: true)
                .Index(t => t.FlightId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.Tickets", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Flights", "AirlineCompanyId", "dbo.AirlineCompanies");
            DropForeignKey("dbo.AirlineCompanies", "CountryCode", "dbo.Countries");
            DropIndex("dbo.Tickets", new[] { "CustomerId" });
            DropIndex("dbo.Tickets", new[] { "FlightId" });
            DropIndex("dbo.Flights", new[] { "AirlineCompanyId" });
            DropIndex("dbo.AirlineCompanies", new[] { "CountryCode" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Flights");
            DropTable("dbo.Customers");
            DropTable("dbo.Countries");
            DropTable("dbo.AirlineCompanies");
        }
    }
}
