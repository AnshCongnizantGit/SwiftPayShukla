using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftPay.Migrations
{
    /// <inheritdoc />
    public partial class EnsureRemitFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RemittanceRequestRemitId",
                table: "RemitValidations",
                type: "nvarchar(64)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemittanceRequestRemitId",
                table: "RemittanceDocuments",
                type: "nvarchar(64)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RemitValidations_RemittanceRequestRemitId",
                table: "RemitValidations",
                column: "RemittanceRequestRemitId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceDocuments_RemittanceRequestRemitId",
                table: "RemittanceDocuments",
                column: "RemittanceRequestRemitId");

            migrationBuilder.AddForeignKey(
                name: "FK_RemittanceDocuments_RemittanceRequests_RemittanceRequestRemitId",
                table: "RemittanceDocuments",
                column: "RemittanceRequestRemitId",
                principalTable: "RemittanceRequests",
                principalColumn: "RemitId");

            migrationBuilder.AddForeignKey(
                name: "FK_RemitValidations_RemittanceRequests_RemittanceRequestRemitId",
                table: "RemitValidations",
                column: "RemittanceRequestRemitId",
                principalTable: "RemittanceRequests",
                principalColumn: "RemitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RemittanceDocuments_RemittanceRequests_RemittanceRequestRemitId",
                table: "RemittanceDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_RemitValidations_RemittanceRequests_RemittanceRequestRemitId",
                table: "RemitValidations");

            migrationBuilder.DropIndex(
                name: "IX_RemitValidations_RemittanceRequestRemitId",
                table: "RemitValidations");

            migrationBuilder.DropIndex(
                name: "IX_RemittanceDocuments_RemittanceRequestRemitId",
                table: "RemittanceDocuments");

            migrationBuilder.DropColumn(
                name: "RemittanceRequestRemitId",
                table: "RemitValidations");

            migrationBuilder.DropColumn(
                name: "RemittanceRequestRemitId",
                table: "RemittanceDocuments");
        }
    }
}
