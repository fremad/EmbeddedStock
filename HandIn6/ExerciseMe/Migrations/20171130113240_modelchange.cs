using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ExerciseMe.Migrations
{
    public partial class modelchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutID",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "WorkoutID",
                table: "Exercises",
                newName: "WorkoutForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WorkoutID",
                table: "Exercises",
                newName: "IX_Exercises_WorkoutForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutForeignKey",
                table: "Exercises",
                column: "WorkoutForeignKey",
                principalTable: "Workouts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutForeignKey",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "WorkoutForeignKey",
                table: "Exercises",
                newName: "WorkoutID");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WorkoutForeignKey",
                table: "Exercises",
                newName: "IX_Exercises_WorkoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutID",
                table: "Exercises",
                column: "WorkoutID",
                principalTable: "Workouts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
