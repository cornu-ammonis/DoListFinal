namespace DoListFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testinher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.List_Items", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.List_Items", "Discriminator");
        }
    }
}
