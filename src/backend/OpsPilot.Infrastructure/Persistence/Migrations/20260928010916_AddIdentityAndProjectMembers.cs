using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpsPilot.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityAndProjectMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OWNER_ID",
                table: "PROJECT",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UPDATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FULL_NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LAST_LOGIN_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CREATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UPDATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROJECT_MEMBER",
                columns: table => new
                {
                    PROJECT_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ROLE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JOINED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true),
                    CREATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UPDATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECT_MEMBER", x => new { x.PROJECT_ID, x.USER_ID });
                    table.ForeignKey(
                        name: "FK_PROJECT_MEMBER_PROJECT_PROJECT_ID",
                        column: x => x.PROJECT_ID,
                        principalTable: "PROJECT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROJECT_MEMBER_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REFRESH_TOKEN",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TOKEN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EXPIRES_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    REVOKED_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true),
                    CREATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UPDATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REFRESH_TOKEN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REFRESH_TOKEN_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLE",
                columns: table => new
                {
                    USER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ROLE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true),
                    CREATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UPDATE_BY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ROLE", x => new { x.USER_ID, x.ROLE_ID });
                    table.ForeignKey(
                        name: "FK_USER_ROLE_ROLE_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_ROLE_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_CODE",
                table: "PROJECT",
                column: "CODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_OWNER_ID",
                table: "PROJECT",
                column: "OWNER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECT_MEMBER_USER_ID",
                table: "PROJECT_MEMBER",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REFRESH_TOKEN_TOKEN",
                table: "REFRESH_TOKEN",
                column: "TOKEN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REFRESH_TOKEN_USER_ID",
                table: "REFRESH_TOKEN",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLE_ROLE_ID",
                table: "USER_ROLE",
                column: "ROLE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PROJECT_USER_OWNER_ID",
                table: "PROJECT",
                column: "OWNER_ID",
                principalTable: "USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROJECT_USER_OWNER_ID",
                table: "PROJECT");

            migrationBuilder.DropTable(
                name: "PROJECT_MEMBER");

            migrationBuilder.DropTable(
                name: "REFRESH_TOKEN");

            migrationBuilder.DropTable(
                name: "USER_ROLE");

            migrationBuilder.DropTable(
                name: "ROLE");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropIndex(
                name: "IX_PROJECT_CODE",
                table: "PROJECT");

            migrationBuilder.DropIndex(
                name: "IX_PROJECT_OWNER_ID",
                table: "PROJECT");

            migrationBuilder.DropColumn(
                name: "OWNER_ID",
                table: "PROJECT");
        }
    }
}
