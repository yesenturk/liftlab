namespace liftmarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServisBakims", "SiparisNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServisBakims", "SiparisNo");
        }
    }
}
