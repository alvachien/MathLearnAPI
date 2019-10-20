using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathLearnAPI.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "awardplan",
                columns: table => new
                {
                    planid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tgtuser = table.Column<string>(maxLength: 50, nullable: false),
                    createdby = table.Column<string>(maxLength: 50, nullable: true),
                    validfrom = table.Column<DateTime>(type: "date", nullable: false),
                    validto = table.Column<DateTime>(type: "date", nullable: false),
                    quiztype = table.Column<short>(nullable: false),
                    quizcontrol = table.Column<string>(maxLength: 250, nullable: true),
                    minscore = table.Column<int>(nullable: true),
                    minavgtime = table.Column<int>(nullable: true),
                    award = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_awardplan", x => x.planid);
                });

            migrationBuilder.CreateTable(
                name: "knowledge",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CATEGORY = table.Column<byte>(nullable: true),
                    NAME = table.Column<string>(maxLength: 50, nullable: false),
                    CONTENT = table.Column<string>(nullable: false),
                    CAN_GENERATE = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_knowledge", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "questionbank",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CATEGORY = table.Column<byte>(nullable: false),
                    BRIEF_CONT = table.Column<string>(maxLength: 50, nullable: false),
                    CONTENT = table.Column<string>(nullable: false),
                    ANSWER = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionbank", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "quiz",
                columns: table => new
                {
                    quizid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    quiztype = table.Column<short>(nullable: false),
                    basicinfo = table.Column<string>(maxLength: 250, nullable: true),
                    attenduser = table.Column<string>(maxLength: 50, nullable: false),
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
                    displayas = table.Column<string>(maxLength: 50, nullable: false),
                    others = table.Column<string>(maxLength: 50, nullable: true),
                    award = table.Column<string>(maxLength: 5, nullable: true),
                    awardplan = table.Column<string>(maxLength: 5, nullable: true),
                    deletionflag = table.Column<bool>(nullable: true)
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
                    table.UniqueConstraint("AK_tag_REFID_REFTYPE_TAG", x => new { x.REFID, x.REFTYPE, x.TAG });
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
                    table.UniqueConstraint("AK_qbklink_KWGID_QBID", x => new { x.KWGID, x.QBID });
                    table.ForeignKey(
                        name: "FK_qbklink_kwid",
                        column: x => x.KWGID,
                        principalTable: "knowledge",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qbklink_qbid",
                        column: x => x.QBID,
                        principalTable: "questionbank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    table.UniqueConstraint("AK_quizfaillog_failidx_quizid", x => new { x.failidx, x.quizid });
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
                    timespent = table.Column<int>(nullable: false),
                    totalitems = table.Column<int>(nullable: false),
                    faileditems = table.Column<int>(nullable: false)
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
                    table.UniqueConstraint("AK_permuser_monitor_userid", x => new { x.monitor, x.userid });
                    table.ForeignKey(
                        name: "FK_permuser_monitor",
                        column: x => x.monitor,
                        principalTable: "quizuser",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_permuser_user",
                        column: x => x.userid,
                        principalTable: "quizuser",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "useraward",
                columns: table => new
                {
                    aid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userid = table.Column<string>(maxLength: 50, nullable: false),
                    adate = table.Column<DateTime>(type: "date", nullable: false),
                    award = table.Column<int>(nullable: false),
                    planid = table.Column<int>(nullable: true),
                    qid = table.Column<int>(nullable: true),
                    used = table.Column<string>(maxLength: 50, nullable: true),
                    publish = table.Column<bool>(nullable: true),
                    Quizid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_useraward", x => x.aid);
                    table.ForeignKey(
                        name: "FK_useraward_awardplan_planid",
                        column: x => x.planid,
                        principalTable: "awardplan",
                        principalColumn: "planid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_useraward_quiz_Quizid",
                        column: x => x.Quizid,
                        principalTable: "quiz",
                        principalColumn: "quizid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_useraward_quizuser_userid",
                        column: x => x.userid,
                        principalTable: "quizuser",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_useraward_planid",
                table: "useraward",
                column: "planid");

            migrationBuilder.CreateIndex(
                name: "IX_useraward_Quizid",
                table: "useraward",
                column: "Quizid");

            migrationBuilder.CreateIndex(
                name: "IX_useraward_userid",
                table: "useraward",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permuser");

            migrationBuilder.DropTable(
                name: "qbklink");

            migrationBuilder.DropTable(
                name: "quizfaillog");

            migrationBuilder.DropTable(
                name: "quizsection");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "useraward");

            migrationBuilder.DropTable(
                name: "knowledge");

            migrationBuilder.DropTable(
                name: "questionbank");

            migrationBuilder.DropTable(
                name: "awardplan");

            migrationBuilder.DropTable(
                name: "quiz");

            migrationBuilder.DropTable(
                name: "quizuser");
        }
    }
}
