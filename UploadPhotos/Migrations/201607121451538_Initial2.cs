namespace UploadPhotos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emotions", "faceRectangle_height", "dbo.Facerectangles");
            DropForeignKey("dbo.Emotions", "scores_anger", "dbo.Scores");
            DropIndex("dbo.Emotions", new[] { "faceRectangle_height" });
            DropIndex("dbo.Emotions", new[] { "scores_anger" });
            AlterColumn("dbo.Emotions", "faceRectangle_height", c => c.Int(nullable: false));
            AlterColumn("dbo.Emotions", "scores_anger", c => c.Single(nullable: false));
            CreateIndex("dbo.Emotions", "faceRectangle_height");
            CreateIndex("dbo.Emotions", "scores_anger");
            AddForeignKey("dbo.Emotions", "faceRectangle_height", "dbo.Facerectangles", "height", cascadeDelete: true);
            AddForeignKey("dbo.Emotions", "scores_anger", "dbo.Scores", "anger", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emotions", "scores_anger", "dbo.Scores");
            DropForeignKey("dbo.Emotions", "faceRectangle_height", "dbo.Facerectangles");
            DropIndex("dbo.Emotions", new[] { "scores_anger" });
            DropIndex("dbo.Emotions", new[] { "faceRectangle_height" });
            AlterColumn("dbo.Emotions", "scores_anger", c => c.Single());
            AlterColumn("dbo.Emotions", "faceRectangle_height", c => c.Int());
            CreateIndex("dbo.Emotions", "scores_anger");
            CreateIndex("dbo.Emotions", "faceRectangle_height");
            AddForeignKey("dbo.Emotions", "scores_anger", "dbo.Scores", "anger");
            AddForeignKey("dbo.Emotions", "faceRectangle_height", "dbo.Facerectangles", "height");
        }
    }
}
