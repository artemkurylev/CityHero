using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CityHero.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.Id);
                });
            
            migrationBuilder.CreateTable(
                name: "place",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    coordX = table.Column<float>(nullable: false),
                    coordY = table.Column<float>(nullable: true),
                    description = table.Column<string>(maxLength: 100, nullable: true),
                    id_type = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_place", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    coordX = table.Column<float>(nullable: false),
                    coordY = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    text = table.Column<string>(maxLength: 40, nullable: false),
                    answer = table.Column<string>(maxLength: 20, nullable: false),
                    place_id = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.Id);
                });
            
            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 10, nullable: true),
                    user_id = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_ToUsers",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "visited_places",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(maxLength: 450, nullable: false),
                    place_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visited_places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_visited_places_ToTable_1",
                        column: x => x.place_id,
                        principalTable: "place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_visited_places_ToTable",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "place_area",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    place_id = table.Column<int>(nullable: false),
                    point_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_place_area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_place_area_place",
                        column: x => x.place_id,
                        principalTable: "place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_place_area_point",
                        column: x => x.point_id,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "answered_questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(maxLength: 450, nullable: false),
                    question_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answered_questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_answered_questions_ToTable_1",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_answered_questions_ToTable",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_answered_questions_question_id",
                table: "answered_questions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_answered_questions_user_id",
                table: "answered_questions",
                column: "user_id");
            
            migrationBuilder.CreateIndex(
                name: "IX_place_area_place_id",
                table: "place_area",
                column: "place_id");

            migrationBuilder.CreateIndex(
                name: "IX_place_area_point_id",
                table: "place_area",
                column: "point_id");

            migrationBuilder.CreateIndex(
                name: "IX_Test_user_id",
                table: "Test",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_visited_places_place_id",
                table: "visited_places",
                column: "place_id");

            migrationBuilder.CreateIndex(
                name: "IX_visited_places_user_id",
                table: "visited_places",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "answered_questions");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "place_area");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "visited_places");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "place");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
