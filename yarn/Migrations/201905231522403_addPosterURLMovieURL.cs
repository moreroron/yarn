namespace yarn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPosterURLMovieURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "PosterURL", c => c.String());
            AddColumn("dbo.Movies", "MovieURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "MovieURL");
            DropColumn("dbo.Movies", "PosterURL");
        }
    }
}
