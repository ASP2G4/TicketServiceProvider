using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketServiceProvider.Migrations
{
    /// <inheritdoc />
    public partial class categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketEntities",
                table: "TicketEntities");

            migrationBuilder.AlterColumn<string>(
                name: "EventId",
                table: "TicketEntities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "TicketEntities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "TicketEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "TicketEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketEntities",
                table: "TicketEntities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CategoryEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketEntities_CategoryId",
                table: "TicketEntities",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntities_CategoryEntity_CategoryId",
                table: "TicketEntities",
                column: "CategoryId",
                principalTable: "CategoryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntities_CategoryEntity_CategoryId",
                table: "TicketEntities");

            migrationBuilder.DropTable(
                name: "CategoryEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketEntities",
                table: "TicketEntities");

            migrationBuilder.DropIndex(
                name: "IX_TicketEntities_CategoryId",
                table: "TicketEntities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TicketEntities");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TicketEntities");

            migrationBuilder.DropColumn(
                name: "EventName",
                table: "TicketEntities");

            migrationBuilder.AlterColumn<string>(
                name: "EventId",
                table: "TicketEntities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketEntities",
                table: "TicketEntities",
                column: "EventId");
        }
    }
}
