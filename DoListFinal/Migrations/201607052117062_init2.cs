namespace DoListFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
          //  DropTable("dbo.UserBanks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserBanks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AppUserID = c.String(),
                        Item_indexes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
