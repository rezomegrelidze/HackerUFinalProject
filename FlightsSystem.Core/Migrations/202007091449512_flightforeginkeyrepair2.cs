namespace FlightsSystem.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flightforeginkeyrepair2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Flights", "OriginCountryCode", c => c.Long());
            AlterColumn("dbo.Flights", "DestinationCountryCode", c => c.Long());
            CreateIndex("dbo.Flights", "OriginCountryCode");
            CreateIndex("dbo.Flights", "DestinationCountryCode");
            AddForeignKey("dbo.Flights", "DestinationCountryCode", "dbo.Countries", "Id");
            AddForeignKey("dbo.Flights", "OriginCountryCode", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "OriginCountryCode", "dbo.Countries");
            DropForeignKey("dbo.Flights", "DestinationCountryCode", "dbo.Countries");
            DropIndex("dbo.Flights", new[] { "DestinationCountryCode" });
            DropIndex("dbo.Flights", new[] { "OriginCountryCode" });
            AlterColumn("dbo.Flights", "DestinationCountryCode", c => c.Long(nullable: false));
            AlterColumn("dbo.Flights", "OriginCountryCode", c => c.Long(nullable: false));
        }
    }
}
