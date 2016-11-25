namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InheritanceMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auto", "AutoKlasse", c => c.Int(nullable: false));
            DropColumn("dbo.Auto", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auto", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Auto", "AutoKlasse");
        }
    }
}
