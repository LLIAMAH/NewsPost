using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsPost.Data.Migrations
{
    public partial class AddedNewsAndRevisions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Comments",
                newName: "NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                newName: "IX_Comments_NewsId");

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateApproved = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Revisions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revisions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_AuthorId",
                table: "News",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_PostId",
                table: "Revisions",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_News_NewsId",
                table: "Comments",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_News_NewsId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Revisions");

            migrationBuilder.RenameColumn(
                name: "NewsId",
                table: "Comments",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_NewsId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
