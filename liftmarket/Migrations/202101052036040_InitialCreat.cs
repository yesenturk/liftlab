namespace liftmarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bayis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SehirId = c.Int(nullable: false),
                        IlceId = c.Int(nullable: false),
                        BayiKod = c.String(),
                        Sifre = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ilces", t => t.IlceId, cascadeDelete: true)
                .ForeignKey("dbo.Sehirs", t => t.SehirId, cascadeDelete: true)
                .Index(t => t.SehirId)
                .Index(t => t.IlceId);
            
            CreateTable(
                "dbo.Ilces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SehirId = c.Int(nullable: false),
                        IlceAdi = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sehirs", t => t.SehirId, cascadeDelete: false)
                .Index(t => t.SehirId);
            
            CreateTable(
                "dbo.Sehirs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SehirAdi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hammaddes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HammaddeAdi = c.String(),
                        StokSayisi = c.Int(nullable: false),
                        TedarikSuresi = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Recete_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recetes", t => t.Recete_Id)
                .Index(t => t.Recete_Id);
            
            CreateTable(
                "dbo.OdemeYontemleris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OdemeSekli = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recetes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Uruns", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.Uruns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunAdi = c.String(),
                        BayiFiyati = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MusteriFiyati = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UretimSure = c.Int(nullable: false),
                        BakimSure = c.Int(nullable: false),
                        KarDurumu = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServisBakims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Aciklama = c.String(),
                        UrunId = c.Int(nullable: false),
                        BakimTarihi = c.DateTime(nullable: false),
                        BayiId = c.Int(nullable: false),
                        BakimMasrafi = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bayis", t => t.BayiId, cascadeDelete: true)
                .ForeignKey("dbo.Uruns", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId)
                .Index(t => t.BayiId);
            
            CreateTable(
                "dbo.Siparislers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BayiId = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        SiparisTarihi = c.DateTime(nullable: false),
                        OdemeYontemleriId = c.Int(),
                        ToplamTutar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OnaylandiMi = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bayis", t => t.BayiId, cascadeDelete: true)
                .ForeignKey("dbo.OdemeYontemleris", t => t.OdemeYontemleriId)
                .ForeignKey("dbo.Uruns", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.BayiId)
                .Index(t => t.UrunId)
                .Index(t => t.OdemeYontemleriId);
            
            CreateTable(
                "dbo.Yoneticis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciAdi = c.String(),
                        Sifre = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Siparislers", "UrunId", "dbo.Uruns");
            DropForeignKey("dbo.Siparislers", "OdemeYontemleriId", "dbo.OdemeYontemleris");
            DropForeignKey("dbo.Siparislers", "BayiId", "dbo.Bayis");
            DropForeignKey("dbo.ServisBakims", "UrunId", "dbo.Uruns");
            DropForeignKey("dbo.ServisBakims", "BayiId", "dbo.Bayis");
            DropForeignKey("dbo.Recetes", "UrunId", "dbo.Uruns");
            DropForeignKey("dbo.Hammaddes", "Recete_Id", "dbo.Recetes");
            DropForeignKey("dbo.Bayis", "SehirId", "dbo.Sehirs");
            DropForeignKey("dbo.Bayis", "IlceId", "dbo.Ilces");
            DropForeignKey("dbo.Ilces", "SehirId", "dbo.Sehirs");
            DropIndex("dbo.Siparislers", new[] { "OdemeYontemleriId" });
            DropIndex("dbo.Siparislers", new[] { "UrunId" });
            DropIndex("dbo.Siparislers", new[] { "BayiId" });
            DropIndex("dbo.ServisBakims", new[] { "BayiId" });
            DropIndex("dbo.ServisBakims", new[] { "UrunId" });
            DropIndex("dbo.Recetes", new[] { "UrunId" });
            DropIndex("dbo.Hammaddes", new[] { "Recete_Id" });
            DropIndex("dbo.Ilces", new[] { "SehirId" });
            DropIndex("dbo.Bayis", new[] { "IlceId" });
            DropIndex("dbo.Bayis", new[] { "SehirId" });
            DropTable("dbo.Yoneticis");
            DropTable("dbo.Siparislers");
            DropTable("dbo.ServisBakims");
            DropTable("dbo.Uruns");
            DropTable("dbo.Recetes");
            DropTable("dbo.OdemeYontemleris");
            DropTable("dbo.Hammaddes");
            DropTable("dbo.Sehirs");
            DropTable("dbo.Ilces");
            DropTable("dbo.Bayis");
        }
    }
}
