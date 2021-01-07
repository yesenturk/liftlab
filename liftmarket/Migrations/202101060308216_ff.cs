namespace liftmarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServisBakims", "BakimTarihi", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ServisBakims", "sonrakiBakimTarihi", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServisBakims", "sonrakiBakimTarihi", c => c.DateTime());
            AlterColumn("dbo.ServisBakims", "BakimTarihi", c => c.DateTime());
        }
    }
}
