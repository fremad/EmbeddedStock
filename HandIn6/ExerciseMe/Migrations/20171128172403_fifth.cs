using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ExerciseMe.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_ApplicationUserId",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Workouts",
                newName: "OwnerApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_ApplicationUserId",
                table: "Workouts",
                newName: "IX_Workouts_OwnerApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_OwnerApplicationUserId",
                table: "Workouts",
                column: "OwnerApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_OwnerApplicationUserId",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "OwnerApplicationUserId",
                table: "Workouts",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_OwnerApplicationUserId",
                table: "Workouts",
                newName: "IX_Workouts_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_ApplicationUserId",
                table: "Workouts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
