namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marke = c.String(nullable: false, maxLength: 20),
                        Tagestarif = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Basistarif = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kundes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vorname = c.String(nullable: false, maxLength: 20),
                        Nachname = c.String(nullable: false, maxLength: 20),
                        Geburtsdatum = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Von = c.DateTime(nullable: false),
                        Bis = c.DateTime(nullable: false),
                        AutoId = c.Int(nullable: false),
                        KundeId = c.Int(nullable: false),
                        ReservationsNr = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autoes", t => t.AutoId, cascadeDelete: true)
                .ForeignKey("dbo.Kundes", t => t.KundeId, cascadeDelete: true)
                .Index(t => t.AutoId)
                .Index(t => t.KundeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "KundeId", "dbo.Kundes");
            DropForeignKey("dbo.Reservations", "AutoId", "dbo.Autoes");
            DropIndex("dbo.Reservations", new[] { "KundeId" });
            DropIndex("dbo.Reservations", new[] { "AutoId" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Kundes");
            DropTable("dbo.Autoes");
        }
    }
}
