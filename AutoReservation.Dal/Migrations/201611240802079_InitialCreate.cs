namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auto",
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
                "dbo.Reservation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Von = c.DateTime(nullable: false),
                        Bis = c.DateTime(nullable: false),
                        AutoId = c.Int(nullable: false),
                        KundeId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auto", t => t.AutoId, cascadeDelete: true)
                .ForeignKey("dbo.Kunde", t => t.KundeId, cascadeDelete: true)
                .Index(t => t.AutoId)
                .Index(t => t.KundeId);
            
            CreateTable(
                "dbo.Kunde",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vorname = c.String(nullable: false, maxLength: 20),
                        Nachname = c.String(nullable: false, maxLength: 20),
                        Geburtsdatum = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "KundeId", "dbo.Kunde");
            DropForeignKey("dbo.Reservation", "AutoId", "dbo.Auto");
            DropIndex("dbo.Reservation", new[] { "KundeId" });
            DropIndex("dbo.Reservation", new[] { "AutoId" });
            DropTable("dbo.Kunde");
            DropTable("dbo.Reservation");
            DropTable("dbo.Auto");
        }
    }
}
