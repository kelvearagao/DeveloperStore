using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class ProductAndSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Rating_Rate = table.Column<decimal>(type: "numeric(3,2)", nullable: true),
                    Rating_Count = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleNumber = table.Column<int>(type: "integer", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Branch = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleItem_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Image", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Category A", "Product A Description", "product-a.jpg", 10.99m, "Product A" },
                    { 2, "Category B", "Product B Description", "product-b.jpg", 20.49m, "Product B" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Phone", "Role", "Status", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("6aacbe87-4877-46d6-97b2-3e8e5cdcf895"), new DateTime(2025, 3, 25, 2, 28, 30, 381, DateTimeKind.Utc).AddTicks(380), "jane.smith@example.com", "Ab#123123", "", "None", "Unknown", null, "Jane Smith" },
                    { new Guid("75beef65-3c82-4797-934d-11b9dff6cfad"), new DateTime(2025, 3, 25, 2, 28, 30, 381, DateTimeKind.Utc).AddTicks(370), "john.doe@example.com", "Ab#123123", "", "None", "Unknown", null, "John Doe" }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "Branch", "SaleDate", "SaleNumber", "TotalAmount", "UserId" },
                values: new object[] { new Guid("5dfbd0a0-ff92-440c-a2f2-cd59309ea2dc"), "Branch A", new DateTime(2025, 3, 25, 2, 28, 30, 381, DateTimeKind.Utc).AddTicks(550), 1001, 50.00m, new Guid("75beef65-3c82-4797-934d-11b9dff6cfad") });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "Branch", "IsCancelled", "SaleDate", "SaleNumber", "TotalAmount", "UserId" },
                values: new object[] { new Guid("dd909f22-1a99-4319-a9fe-ac399763ef61"), "Branch B", true, new DateTime(2025, 3, 25, 2, 28, 30, 381, DateTimeKind.Utc).AddTicks(560), 1002, 30.00m, new Guid("6aacbe87-4877-46d6-97b2-3e8e5cdcf895") });

            migrationBuilder.InsertData(
                table: "SaleItem",
                columns: new[] { "Id", "Discount", "ProductId", "Quantity", "SaleId", "TotalAmount", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 0m, 1, 2, new Guid("5dfbd0a0-ff92-440c-a2f2-cd59309ea2dc"), 0m, 10.00m },
                    { 2, 0m, 2, 1, new Guid("5dfbd0a0-ff92-440c-a2f2-cd59309ea2dc"), 0m, 30.00m },
                    { 3, 0m, 1, 3, new Guid("dd909f22-1a99-4319-a9fe-ac399763ef61"), 0m, 10.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_ProductId",
                table: "SaleItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_SaleId",
                table: "SaleItem",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId",
                table: "Sales",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleItem");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6aacbe87-4877-46d6-97b2-3e8e5cdcf895"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("75beef65-3c82-4797-934d-11b9dff6cfad"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");
        }
    }
}
