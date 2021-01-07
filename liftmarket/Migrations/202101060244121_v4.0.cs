namespace liftmarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v40 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServisBakims", "sonrakiBakimTarihi", c => c.DateTime());
            AlterColumn("dbo.ServisBakims", "BakimTarihi", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServisBakims", "BakimTarihi", c => c.DateTime(nullable: false));
            DropColumn("dbo.ServisBakims", "sonrakiBakimTarihi");
        }
    }
}
