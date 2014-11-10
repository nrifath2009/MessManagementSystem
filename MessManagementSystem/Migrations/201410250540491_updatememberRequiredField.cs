namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatememberRequiredField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "ContactNo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "ContactNo", c => c.String());
            AlterColumn("dbo.Members", "Email", c => c.String());
            AlterColumn("dbo.Members", "Name", c => c.String());
        }
    }
}
