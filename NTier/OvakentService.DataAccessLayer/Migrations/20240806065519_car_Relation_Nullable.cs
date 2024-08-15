using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OvakentService.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class car_Relation_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Arventos_ArventoId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_AppUserId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ArventoId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Arventos_ArventoId",
                table: "Cars",
                column: "ArventoId",
                principalTable: "Arventos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_AppUserId",
                table: "Cars",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Arventos_ArventoId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_AppUserId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ArventoId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Arventos_ArventoId",
                table: "Cars",
                column: "ArventoId",
                principalTable: "Arventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_AppUserId",
                table: "Cars",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
