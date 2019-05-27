namespace yarn_rider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        MovieName = c.String(),
                        Date = c.String(),
                        Rate = c.Int(nullable: false),
                        PosterURL = c.String(),
                        MovieURL = c.String(),
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
                        Date = c.String(),
                        Movie_MovieID = c.Int(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.Movie_MovieID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        ConfirmPassword = c.String(),
                        EmailID = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsEmailVerified = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                        ReviewID = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.UserMovies",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Movie_MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Movie_MovieID })
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID, cascadeDelete: true)
                .Index(t => t.User_UserID)
                .Index(t => t.Movie_MovieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.UserMovies", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.UserMovies", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Movie_MovieID", "dbo.Movies");
            DropIndex("dbo.UserMovies", new[] { "Movie_MovieID" });
            DropIndex("dbo.UserMovies", new[] { "User_UserID" });
            DropIndex("dbo.Reviews", new[] { "User_UserID" });
            DropIndex("dbo.Reviews", new[] { "Movie_MovieID" });
            DropTable("dbo.UserMovies");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Movies");
        }
    }
}
