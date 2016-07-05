namespace DoListFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_ID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.List_Items", "User_ID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.List_Items", "User_ID");
        }
    }
}
