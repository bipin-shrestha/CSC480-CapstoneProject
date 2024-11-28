using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetRehome.Migrations
{
    /// <inheritdoc />
    public partial class NewTableAdditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adopters_Shelters_ShelterId",
                table: "Adopters");

            migrationBuilder.DropIndex(
                name: "IX_Adopters_ShelterId",
                table: "Adopters");

            migrationBuilder.DropColumn(
                name: "ShelderAddress",
                table: "Shelters");

            migrationBuilder.DropColumn(
                name: "ShelterId",
                table: "Adopters");

            migrationBuilder.AlterColumn<string>(
                name: "ShelterName",
                table: "Shelters",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "ShelterAddress",
                table: "Shelters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShelterAddress",
                table: "Shelters");

            migrationBuilder.AlterColumn<int>(
                name: "ShelterName",
                table: "Shelters",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ShelderAddress",
                table: "Shelters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShelterId",
                table: "Adopters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Adopters_ShelterId",
                table: "Adopters",
                column: "ShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adopters_Shelters_ShelterId",
                table: "Adopters",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "ShelterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
