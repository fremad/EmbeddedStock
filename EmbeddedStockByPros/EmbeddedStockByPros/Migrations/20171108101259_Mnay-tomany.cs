using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EmbeddedStockByPros.Migrations
{
    public partial class Mnaytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ComponentTypes_ComponentTypeId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ComponentTypeId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ComponentTypeId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryComponenttypebindings",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ComponentTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryComponenttypebindings", x => new { x.CategoryId, x.ComponentTypeId });
                    table.ForeignKey(
                        name: "FK_CategoryComponenttypebindings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryComponenttypebindings_ComponentTypes_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentTypes",
                        principalColumn: "ComponentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryComponenttypebindings_ComponentTypeId",
                table: "CategoryComponenttypebindings",
                column: "ComponentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryComponenttypebindings");

            migrationBuilder.AddColumn<long>(
                name: "ComponentTypeId",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ComponentTypeId",
                table: "Categories",
                column: "ComponentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ComponentTypes_ComponentTypeId",
                table: "Categories",
                column: "ComponentTypeId",
                principalTable: "ComponentTypes",
                principalColumn: "ComponentTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
