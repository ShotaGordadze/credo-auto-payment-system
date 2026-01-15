using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Credo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProviderCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    providerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProviderAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderCategoryId = table.Column<int>(type: "int", nullable: false),
                    UId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_ProviderCategories_ProviderCategoryId",
                        column: x => x.ProviderCategoryId,
                        principalTable: "ProviderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Debt = table.Column<int>(type: "int", nullable: false),
                    SubscriberNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    UId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribers_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Providers_ProviderCategoryId",
                table: "Providers",
                column: "ProviderCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_ProviderId",
                table: "Subscribers",
                column: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "ProviderCategories");
        }
    }
}
