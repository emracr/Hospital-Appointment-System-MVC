namespace RandevuSistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hekim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Isim = c.String(maxLength: 50),
                        Brans = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Poliklinik", t => t.Brans, cascadeDelete: true)
                .Index(t => t.Brans);
            
            CreateTable(
                "dbo.Poliklinik",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PoliklinikAdi = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Randevu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HastaTC = c.String(maxLength: 128),
                        RandevuTarihi = c.DateTime(nullable: false),
                        RandevuSaati = c.Int(nullable: false),
                        RandevuAlmaTarihi = c.DateTime(nullable: false),
                        Poliklinik = c.Int(nullable: false),
                        Hekim = c.String(),
                        Il = c.Int(nullable: false),
                        Ilce = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Il", t => t.Il, cascadeDelete: true)
                .ForeignKey("dbo.Poliklinik", t => t.Poliklinik, cascadeDelete: true)
                .ForeignKey("dbo.RandevuSaat", t => t.RandevuSaati, cascadeDelete: true)
                .ForeignKey("dbo.Vatandas", t => t.HastaTC)
                .Index(t => t.HastaTC)
                .Index(t => t.RandevuSaati)
                .Index(t => t.Poliklinik)
                .Index(t => t.Il);
            
            CreateTable(
                "dbo.Il",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SehirAdi = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ilce",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IlceAdi = c.String(nullable: false, maxLength: 30),
                        SehirId = c.Int(nullable: false),
                        Iller_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Il", t => t.Iller_Id)
                .Index(t => t.Iller_Id);
            
            CreateTable(
                "dbo.RandevuSaat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RandevuSaati = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vatandas",
                c => new
                    {
                        TC = c.String(nullable: false, maxLength: 128),
                        Ad = c.String(nullable: false),
                        Soyad = c.String(nullable: false),
                        DogumTarihi = c.DateTime(nullable: false),
                        Cinsiyet = c.String(nullable: false),
                        EMail = c.String(nullable: false),
                        Telefon = c.String(nullable: false),
                        GuvenlikSorusu = c.String(nullable: false),
                        GuvenlikCevabi = c.String(nullable: false),
                        Parola = c.String(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.TC);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Randevu", "HastaTC", "dbo.Vatandas");
            DropForeignKey("dbo.Randevu", "RandevuSaati", "dbo.RandevuSaat");
            DropForeignKey("dbo.Randevu", "Poliklinik", "dbo.Poliklinik");
            DropForeignKey("dbo.Randevu", "Il", "dbo.Il");
            DropForeignKey("dbo.Ilce", "Iller_Id", "dbo.Il");
            DropForeignKey("dbo.Hekim", "Brans", "dbo.Poliklinik");
            DropIndex("dbo.Ilce", new[] { "Iller_Id" });
            DropIndex("dbo.Randevu", new[] { "Il" });
            DropIndex("dbo.Randevu", new[] { "Poliklinik" });
            DropIndex("dbo.Randevu", new[] { "RandevuSaati" });
            DropIndex("dbo.Randevu", new[] { "HastaTC" });
            DropIndex("dbo.Hekim", new[] { "Brans" });
            DropTable("dbo.Vatandas");
            DropTable("dbo.RandevuSaat");
            DropTable("dbo.Ilce");
            DropTable("dbo.Il");
            DropTable("dbo.Randevu");
            DropTable("dbo.Poliklinik");
            DropTable("dbo.Hekim");
        }
    }
}
