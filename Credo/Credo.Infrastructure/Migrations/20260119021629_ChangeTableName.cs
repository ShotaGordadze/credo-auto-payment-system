using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Credo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoPaymentAccounts_Accounts_AccountId",
                table: "AutoPaymentAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AutoPaymentAccounts",
                table: "AutoPaymentAccounts");

            migrationBuilder.RenameTable(
                name: "AutoPaymentAccounts",
                newName: "AutoPaymentTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_AutoPaymentAccounts_AccountId_TargetAccountNumber_FrequencyInDays",
                table: "AutoPaymentTemplate",
                newName: "IX_AutoPaymentTemplate_AccountId_TargetAccountNumber_FrequencyInDays");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AutoPaymentTemplate",
                table: "AutoPaymentTemplate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoPaymentTemplate_Accounts_AccountId",
                table: "AutoPaymentTemplate",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoPaymentTemplate_Accounts_AccountId",
                table: "AutoPaymentTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AutoPaymentTemplate",
                table: "AutoPaymentTemplate");

            migrationBuilder.RenameTable(
                name: "AutoPaymentTemplate",
                newName: "AutoPaymentAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_AutoPaymentTemplate_AccountId_TargetAccountNumber_FrequencyInDays",
                table: "AutoPaymentAccounts",
                newName: "IX_AutoPaymentAccounts_AccountId_TargetAccountNumber_FrequencyInDays");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AutoPaymentAccounts",
                table: "AutoPaymentAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoPaymentAccounts_Accounts_AccountId",
                table: "AutoPaymentAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
