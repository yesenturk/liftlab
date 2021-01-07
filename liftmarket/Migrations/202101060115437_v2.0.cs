namespace liftmarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uruns", "BakimFiyati", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Siparislers", "Adet", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Siparislers", "Adet");
            DropColumn("dbo.Uruns", "BakimFiyati");
        }
    }
}
