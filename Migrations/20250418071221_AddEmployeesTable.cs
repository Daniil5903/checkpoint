using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace checkpoint.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passes_Employees_EmployeeId",
                table: "Passes");

            migrationBuilder.DropForeignKey(
                name: "FK_Passes_Visitors_VisitorId",
                table: "Passes");

            migrationBuilder.DropTable(
                name: "CheckpointEmployees");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Passes_EmployeeId",
                table: "Passes");

            migrationBuilder.DropIndex(
                name: "IX_Passes_VisitorId",
                table: "Passes");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Passes");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Passes");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Passes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerType",
                table: "Passes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CheckpointEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckpointEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckpointEmployees");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Passes");

            migrationBuilder.DropColumn(
                name: "OwnerType",
                table: "Passes");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Passes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitorId",
                table: "Passes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckpointEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckpointEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passes_EmployeeId",
                table: "Passes",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Passes_VisitorId",
                table: "Passes",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passes_Employees_EmployeeId",
                table: "Passes",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passes_Visitors_VisitorId",
                table: "Passes",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id");
        }
    }
}
