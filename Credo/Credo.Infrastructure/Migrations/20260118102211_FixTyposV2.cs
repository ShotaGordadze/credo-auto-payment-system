using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Credo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTyposV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Subscribers_SubscriberId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "providerName",
                table: "Providers",
                newName: "ProviderName");

            migrationBuilder.RenameColumn(
                name: "SubscriberId",
                table: "Accounts",
                newName: "CustomerSubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_SubscriberId",
                table: "Accounts",
                newName: "IX_Accounts_CustomerSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Subscribers_CustomerSubscriptionId",
                table: "Accounts",
                column: "CustomerSubscriptionId",
                principalTable: "Subscribers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Subscribers_CustomerSubscriptionId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "ProviderName",
                table: "Providers",
                newName: "providerName");

            migrationBuilder.RenameColumn(
                name: "CustomerSubscriptionId",
                table: "Accounts",
                newName: "SubscriberId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_CustomerSubscriptionId",
                table: "Accounts",
                newName: "IX_Accounts_SubscriberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Subscribers_SubscriberId",
                table: "Accounts",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
