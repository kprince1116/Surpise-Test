using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userroles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rolename = table.Column<string>(name: "role_name", type: "character varying(150)", maxLength: 150, nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("userroles_pkey", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    UserRoleId = table.Column<int>(type: "integer", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.UserId);
                    table.ForeignKey(
                        name: "users_UserRole_fkey",
                        column: x => x.UserRoleId,
                        principalTable: "userroles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "blogs",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    blogtitle = table.Column<string>(name: "blog_title", type: "character varying(500)", maxLength: 500, nullable: false),
                    blogcontent = table.Column<string>(name: "blog_content", type: "character varying(500)", maxLength: 500, nullable: false),
                    blogTAG = table.Column<string>(name: "blog_TAG", type: "character varying(500)", maxLength: 500, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("blogs_pkey", x => x.BlogId);
                    table.ForeignKey(
                        name: "blogs_User_fkey",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    commentcontent = table.Column<string>(name: "comment_content", type: "character varying(500)", maxLength: 500, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("comments_pkey", x => x.CommentId);
                    table.ForeignKey(
                        name: "comments_blogs_fkey",
                        column: x => x.BlogId,
                        principalTable: "blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blogs_UserId",
                table: "blogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_BlogId",
                table: "comments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_users_UserRoleId",
                table: "users",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "blogs");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "userroles");
        }
    }
}
