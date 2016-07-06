namespace DoListFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bool_complete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.List_Items", "is_complete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.List_Items", "is_complete");
        }
    }
}
