namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        MealId = c.Int(nullable: false, identity: true),
                        MealDate = c.DateTime(nullable: false),
                        MemberId = c.Int(nullable: false),
                        NoOfMeal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.MealId)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meals", "MemberId", "dbo.Members");
            DropIndex("dbo.Meals", new[] { "MemberId" });
            DropTable("dbo.Meals");
        }
    }
}
