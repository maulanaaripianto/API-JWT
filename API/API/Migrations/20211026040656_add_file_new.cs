﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_file_new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_M_Employee",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Employee", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "Tb_M_University",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_University", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_Account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_Account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Tb_T_Account_Tb_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "Tb_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tb_M_Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gpa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Tb_M_University",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_Profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_Profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Tb_T_Profiling_Tb_M_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Tb_M_Education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_T_Profiling_Tb_T_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "Tb_T_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_M_Education_UniversityId",
                table: "Tb_M_Education",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_M_Employee_Email",
                table: "Tb_M_Employee",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_M_Employee_Phone",
                table: "Tb_M_Employee",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_Profiling_EducationId",
                table: "Tb_T_Profiling",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_T_Profiling");

            migrationBuilder.DropTable(
                name: "Tb_M_Education");

            migrationBuilder.DropTable(
                name: "Tb_T_Account");

            migrationBuilder.DropTable(
                name: "Tb_M_University");

            migrationBuilder.DropTable(
                name: "Tb_M_Employee");
        }
    }
}
