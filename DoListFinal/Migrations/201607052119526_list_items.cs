namespace DoListFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class list_items : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.List_Items",
               c => new
               {
                   ID = c.Int(nullable: false, identity: true),
                   Description = c.String(),
                   priority = c.Int(nullable: false),
                   User_ID = c.String(),
               })
               .PrimaryKey(t => t.ID);


        }
        
        public override void Down()
        {
            DropTable("dbo.List_Items");
        }
    }
}
