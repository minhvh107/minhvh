namespace Minhvh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableErrorCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorCodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ErrorCodes");
        }
    }
}
