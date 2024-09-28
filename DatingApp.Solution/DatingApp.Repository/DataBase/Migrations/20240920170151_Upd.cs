using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApp.Repository.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class Upd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YourLikeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikeById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InsertedIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedIn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLikes_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLikes_AspNetUsers_InsertedById",
                        column: x => x.InsertedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLikes_AspNetUsers_LikeById",
                        column: x => x.LikeById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserLikes_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLikes_AspNetUsers_YourLikeId",
                        column: x => x.YourLikeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_DeletedById",
                table: "UserLikes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_InsertedById",
                table: "UserLikes",
                column: "InsertedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_LikeById_YourLikeId",
                table: "UserLikes",
                columns: new[] { "LikeById", "YourLikeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_ModifiedById",
                table: "UserLikes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_YourLikeId",
                table: "UserLikes",
                column: "YourLikeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLikes");
        }
    }
}
