using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class ThirdRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThirdRounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdRounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Association",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(nullable: true),
                    ThirdRoundId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Association", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Association_ThirdRounds_ThirdRoundId",
                        column: x => x.ThirdRoundId,
                        principalTable: "ThirdRounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chronology",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThirdRoundId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chronology", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chronology_ThirdRounds_ThirdRoundId",
                        column: x => x.ThirdRoundId,
                        principalTable: "ThirdRounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MelodyGuess",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<int>(nullable: false),
                    ThirdRoundId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MelodyGuess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MelodyGuess_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MelodyGuess_ThirdRounds_ThirdRoundId",
                        column: x => x.ThirdRoundId,
                        principalTable: "ThirdRounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssociationVariant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    AssociationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociationVariant_Association_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Association",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChronologyItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(nullable: false),
                    FileId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ChronologyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChronologyItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChronologyItem_Chronology_ChronologyId",
                        column: x => x.ChronologyId,
                        principalTable: "Chronology",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChronologyItem_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MelodyGuessVariant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    MelodyGuessId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MelodyGuessVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MelodyGuessVariant_MelodyGuess_MelodyGuessId",
                        column: x => x.MelodyGuessId,
                        principalTable: "MelodyGuess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Association_ThirdRoundId",
                table: "Association",
                column: "ThirdRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociationVariant_AssociationId",
                table: "AssociationVariant",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_Chronology_ThirdRoundId",
                table: "Chronology",
                column: "ThirdRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_ChronologyItem_ChronologyId",
                table: "ChronologyItem",
                column: "ChronologyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChronologyItem_FileId",
                table: "ChronologyItem",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_MelodyGuess_FileId",
                table: "MelodyGuess",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_MelodyGuess_ThirdRoundId",
                table: "MelodyGuess",
                column: "ThirdRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_MelodyGuessVariant_MelodyGuessId",
                table: "MelodyGuessVariant",
                column: "MelodyGuessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociationVariant");

            migrationBuilder.DropTable(
                name: "ChronologyItem");

            migrationBuilder.DropTable(
                name: "MelodyGuessVariant");

            migrationBuilder.DropTable(
                name: "Association");

            migrationBuilder.DropTable(
                name: "Chronology");

            migrationBuilder.DropTable(
                name: "MelodyGuess");

            migrationBuilder.DropTable(
                name: "ThirdRounds");
        }
    }
}
