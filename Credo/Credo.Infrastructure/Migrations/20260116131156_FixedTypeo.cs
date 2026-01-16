using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Credo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedTypeo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetAccountNumeber",
                table: "AutoPaymentAccounts",
                newName: "TargetAccountNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AutoPaymentAccounts_AccountId_TargetAccountNumeber_FrequencyInDays",
                table: "AutoPaymentAccounts",
                newName: "IX_AutoPaymentAccounts_AccountId_TargetAccountNumber_FrequencyInDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetAccountNumber",
                table: "AutoPaymentAccounts",
                newName: "TargetAccountNumeber");

            migrationBuilder.RenameIndex(
                name: "IX_AutoPaymentAccounts_AccountId_TargetAccountNumber_FrequencyInDays",
                table: "AutoPaymentAccounts",
                newName: "IX_AutoPaymentAccounts_AccountId_TargetAccountNumeber_FrequencyInDays");
        }
    }
}
