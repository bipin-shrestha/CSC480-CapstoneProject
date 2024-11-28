using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetRehome.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VaccinationRecords",
                table: "Pets",
                newName: "TrainingLevel");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "Pets",
                newName: "Neutered");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "Declawed",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ExcerciseRequirement",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FurType",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MedicalHistory",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "PetType",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SheddingLevel",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SocailLevel",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecieverId = table.Column<int>(type: "INTEGER", nullable: false),
                    MessageText = table.Column<string>(type: "TEXT", nullable: false),
                    SentDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReadDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MessageRead = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropColumn(
                name: "Declawed",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ExcerciseRequirement",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "FurType",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "MedicalHistory",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PetType",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "SheddingLevel",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "SocailLevel",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "TrainingLevel",
                table: "Pets",
                newName: "VaccinationRecords");

            migrationBuilder.RenameColumn(
                name: "Neutered",
                table: "Pets",
                newName: "Available");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
