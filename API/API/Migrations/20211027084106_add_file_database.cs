using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_file_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_M_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_AccountRoles",
                columns: table => new
                {
                    Account_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_AccountRoles", x => x.Account_Id);
                    table.ForeignKey(
                        name: "FK_Tb_T_AccountRoles_Tb_M_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Tb_M_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_T_AccountRoles_Tb_T_Account_AccountNIK",
                        column: x => x.AccountNIK,
                        principalTable: "Tb_T_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_AccountRoles_AccountNIK",
                table: "Tb_T_AccountRoles",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_AccountRoles_RolesId",
                table: "Tb_T_AccountRoles",
                column: "RolesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_T_AccountRoles");

            migrationBuilder.DropTable(
                name: "Tb_M_Roles");
        }
    }
}
