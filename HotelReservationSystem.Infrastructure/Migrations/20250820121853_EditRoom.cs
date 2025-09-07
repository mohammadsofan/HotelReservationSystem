using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoomNumber",
                table: "Rooms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Rooms");
        }
    }
}
