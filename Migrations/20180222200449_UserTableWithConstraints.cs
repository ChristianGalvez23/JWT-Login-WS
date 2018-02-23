using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace loginwebapi.Migrations
{
    public partial class UserTableWithConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BornDate = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: false),
                    Male = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    SignUpDate = table.Column<DateTime>(type: "date", nullable: false, defaultValue: new DateTime(2018, 2, 22, 13, 4, 49, 735, DateTimeKind.Local))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
