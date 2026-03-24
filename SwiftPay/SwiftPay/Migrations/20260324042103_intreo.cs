using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftPay.Migrations
{
    /// <inheritdoc />
    public partial class intreo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_UserID",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserID",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCRecords_Users_UserID",
                table: "KYCRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationAlerts_Users_UserID",
                table: "NotificationAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RateLocks",
                table: "RateLocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FXQuotes",
                table: "FXQuotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeeRules",
                table: "FeeRules");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "RateLocks",
                newName: "RateLock");

            migrationBuilder.RenameTable(
                name: "FXQuotes",
                newName: "FXQuote");

            migrationBuilder.RenameTable(
                name: "FeeRules",
                newName: "FeeRule");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Phone",
                table: "User",
                newName: "IX_User_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRole",
                newName: "IX_UserRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "UserRoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RateLock",
                table: "RateLock",
                column: "LockID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FXQuote",
                table: "FXQuote",
                column: "QuoteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeeRule",
                table: "FeeRule",
                column: "FeeRuleID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_User_UserID",
                table: "AuditLogs",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_User_UserID",
                table: "Customers",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KYCRecords_User_UserID",
                table: "KYCRecords",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationAlerts_User_UserID",
                table: "NotificationAlerts",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_User_UserID",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_User_UserID",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCRecords_User_UserID",
                table: "KYCRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationAlerts_User_UserID",
                table: "NotificationAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RateLock",
                table: "RateLock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FXQuote",
                table: "FXQuote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeeRule",
                table: "FeeRule");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "RateLock",
                newName: "RateLocks");

            migrationBuilder.RenameTable(
                name: "FXQuote",
                newName: "FXQuotes");

            migrationBuilder.RenameTable(
                name: "FeeRule",
                newName: "FeeRules");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserId",
                table: "UserRoles",
                newName: "IX_UserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Phone",
                table: "Users",
                newName: "IX_Users_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "UserRoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RateLocks",
                table: "RateLocks",
                column: "LockID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FXQuotes",
                table: "FXQuotes",
                column: "QuoteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeeRules",
                table: "FeeRules",
                column: "FeeRuleID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_UserID",
                table: "AuditLogs",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserID",
                table: "Customers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KYCRecords_Users_UserID",
                table: "KYCRecords",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationAlerts_Users_UserID",
                table: "NotificationAlerts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
