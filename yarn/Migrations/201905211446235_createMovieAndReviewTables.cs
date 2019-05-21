namespace yarn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createMovieAndReviewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Rate = c.Int(nullable: false),
                        ReviewID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Rating = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Movie_MovieID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Movie_MovieID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.MovieApplicationUsers",
                c => new
                    {
                        Movie_MovieID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Movie_MovieID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Movie_MovieID)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.Users", "MovieID", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "ReviewID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieApplicationUsers", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.MovieApplicationUsers", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Movie_MovieID", "dbo.Movies");
            DropIndex("dbo.MovieApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.MovieApplicationUsers", new[] { "Movie_MovieID" });
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "Movie_MovieID" });
            DropColumn("dbo.Users", "ReviewID");
            DropColumn("dbo.Users", "MovieID");
            DropTable("dbo.MovieApplicationUsers");
            DropTable("dbo.Reviews");
            DropTable("dbo.Movies");
        }
    }
}
