namespace yarn_rider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataAnnoForReview : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Reviews", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Content", c => c.String());
            AlterColumn("dbo.Reviews", "Title", c => c.String());
        }
    }
}
