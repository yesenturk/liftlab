namespace liftmarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Siparislers", "SiparisNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Siparislers", "SiparisNo");
        }
    }
}
