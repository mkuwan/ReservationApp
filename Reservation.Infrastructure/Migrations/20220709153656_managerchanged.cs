using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.Infrastructure.Migrations
{
    public partial class managerchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientMemberId",
                table: "Clients",
                newName: "ClientLoginId");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Managers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerLoginId",
                table: "Managers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ManagerStatus",
                table: "Managers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ManagerLoginId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ManagerStatus",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "ClientLoginId",
                table: "Clients",
                newName: "ClientMemberId");
        }
    }
}
