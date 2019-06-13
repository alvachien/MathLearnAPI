using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathLearnAPI.Migrations
{
    public partial class IntroduceQuestionBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "awardplan");

            migrationBuilder.DropTable(
                name: "permuser");

            migrationBuilder.DropTable(
                name: "quizfaillog");

            migrationBuilder.DropTable(
                name: "quizsection");

            migrationBuilder.DropTable(
                name: "quizuser");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "useraward");

            migrationBuilder.DropTable(
                name: "quiz");

            migrationBuilder.CreateTable(
                name: "questionbank",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CONTENT = table.Column<string>(nullable: false),
                    ATTACHMENT1 = table.Column<byte[]>(nullable: true),
                    ATTACHMENT2 = table.Column<byte[]>(nullable: true),
                    ATTACHMENT3 = table.Column<byte[]>(nullable: true),
                    ATTACHMENT4 = table.Column<byte[]>(nullable: true),
                    ATTACHMENT5 = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionbank", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "qbklink",
                columns: table => new
                {
                    QBID = table.Column<int>(nullable: false),
                    KWGID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qbklink", x => new { x.QBID, x.KWGID });
                    table.ForeignKey(
                        name: "FK_QstnBkKwdg_KWID",
                        column: x => x.KWGID,
                        principalTable: "knowledge",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QstnBkKwdg_QBID",
                        column: x => x.QBID,
                        principalTable: "questionbank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_qbklink_KWGID",
                table: "qbklink",
                column: "KWGID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "qbklink");

            migrationBuilder.DropTable(
                name: "questionbank");

            migrationBuilder.CreateTable(
                name: "awardplan",
                columns: table => new
                {
                    planid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    award = table.Column<int>(nullable: false),
                    createdby = table.Column<string>(maxLength: 50, nullable: true),
                    minavgtime = table.Column<int>(nullable: true),
                    minscore = table.Column<int>(nullable: true),
                    quizcontrol = table.Column<string>(maxLength: 250, nullable: true),
                    quiztype = table.Column<short>(nullable: false),
                    tgtuser = table.Column<string>(maxLength: 50, nullable: false),
                    validfrom = table.Column<DateTime>(type: "date", nullable: false),
                    validto = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_awardplan", x => x.planid);
                });

            migrationBuilder.CreateTable(
                name: "permuser",
                columns: table => new
                {
                    userid = table.Column<string>(maxLength: 50, nullable: false),
                    monitor = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permuser", x => new { x.userid, x.monitor });
                });

            migrationBuilder.CreateTable(
                name: "quiz",
                columns: table => new
                {
                    quizid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    attenduser = table.Column<string>(maxLength: 50, nullable: false),
                    basicinfo = table.Column<string>(maxLength: 250, nullable: true),
                    quiztype = table.Column<short>(nullable: false),
                    submitdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quiz", x => x.quizid);
                });

            migrationBuilder.CreateTable(
                name: "quizuser",
                columns: table => new
                {
                    userid = table.Column<string>(maxLength: 50, nullable: false),
                    award = table.Column<string>(maxLength: 5, nullable: true),
                    awardplan = table.Column<string>(maxLength: 5, nullable: true),
                    deletionflag = table.Column<bool>(nullable: true),
                    displayas = table.Column<string>(maxLength: 50, nullable: false),
                    others = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizuser", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    TAG = table.Column<string>(maxLength: 50, nullable: false),
                    REFTYPE = table.Column<short>(nullable: false),
                    REFID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => new { x.TAG, x.REFTYPE, x.REFID });
                });

            migrationBuilder.CreateTable(
                name: "useraward",
                columns: table => new
                {
                    aid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    adate = table.Column<DateTime>(type: "date", nullable: false),
                    award = table.Column<int>(nullable: false),
                    planid = table.Column<int>(nullable: true),
                    publish = table.Column<bool>(nullable: true),
                    qid = table.Column<int>(nullable: true),
                    used = table.Column<string>(maxLength: 50, nullable: true),
                    userid = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_useraward", x => x.aid);
                });

            migrationBuilder.CreateTable(
                name: "quizfaillog",
                columns: table => new
                {
                    quizid = table.Column<int>(nullable: false),
                    failidx = table.Column<int>(nullable: false),
                    expected = table.Column<string>(maxLength: 250, nullable: false),
                    inputted = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizfaillog", x => new { x.quizid, x.failidx });
                    table.ForeignKey(
                        name: "FK_quizfaillog_quiz",
                        column: x => x.quizid,
                        principalTable: "quiz",
                        principalColumn: "quizid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quizsection",
                columns: table => new
                {
                    quizid = table.Column<int>(nullable: false),
                    section = table.Column<int>(nullable: false),
                    faileditems = table.Column<int>(nullable: false),
                    timespent = table.Column<int>(nullable: false),
                    totalitems = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizsection", x => new { x.quizid, x.section });
                    table.ForeignKey(
                        name: "FK_quizsection_quiz",
                        column: x => x.quizid,
                        principalTable: "quiz",
                        principalColumn: "quizid",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
