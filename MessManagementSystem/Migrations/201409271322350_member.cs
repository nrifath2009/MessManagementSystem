namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class member : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bazars",
                c => new
                    {
                        BazarId = c.Int(nullable: false, identity: true),
                        BazarDate = c.DateTime(nullable: false),
                        MemberId = c.Int(nullable: false),
                        BazarAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BazarId)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        JoiningDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        ContactNo = c.String(),
                    })
                .PrimaryKey(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bazars", "MemberId", "dbo.Members");
            DropIndex("dbo.Bazars", new[] { "MemberId" });
            DropTable("dbo.Members");
            DropTable("dbo.Bazars");
        }
    }
}
