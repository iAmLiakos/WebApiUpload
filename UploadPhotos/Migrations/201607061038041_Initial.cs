namespace UploadPhotos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facerectangles",
                c => new
                    {
                        height = c.Int(nullable: false, identity: true),
                        left = c.Int(nullable: false),
                        top = c.Int(nullable: false),
                        width = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.height);
            
            CreateTable(
                "dbo.Emotions",
                c => new
                    {
                        EmotionId = c.Int(nullable: false, identity: true),
                        faceRectangle_height = c.Int(),
                        scores_anger = c.Single(),
                    })
                .PrimaryKey(t => t.EmotionId)
                .ForeignKey("dbo.Facerectangles", t => t.faceRectangle_height)
                .ForeignKey("dbo.Scores", t => t.scores_anger)
                .Index(t => t.faceRectangle_height)
                .Index(t => t.scores_anger);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        anger = c.Single(nullable: false),
                        contempt = c.Single(nullable: false),
                        disgust = c.Single(nullable: false),
                        fear = c.Single(nullable: false),
                        happiness = c.Single(nullable: false),
                        neutral = c.Single(nullable: false),
                        sadness = c.Single(nullable: false),
                        surprise = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.anger);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emotions", "scores_anger", "dbo.Scores");
            DropForeignKey("dbo.Emotions", "faceRectangle_height", "dbo.Facerectangles");
            DropIndex("dbo.Emotions", new[] { "scores_anger" });
            DropIndex("dbo.Emotions", new[] { "faceRectangle_height" });
            DropTable("dbo.Scores");
            DropTable("dbo.Emotions");
            DropTable("dbo.Facerectangles");
        }
    }
}
