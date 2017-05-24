using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OrganizeFundamental.Data.Migrations
{
	public partial class InitialOrganizeFundamental : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
				name: "Calendar");

			migrationBuilder.EnsureSchema(
				name: "CoupleScriptureLog");

			migrationBuilder.EnsureSchema(
				name: "Scripture");

			migrationBuilder.EnsureSchema(
				name: "UtahEmployee");

			migrationBuilder.CreateTable(
				name: "EventLabels",
				schema: "Calendar",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Title = table.Column<string>(maxLength: 100, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EventLabels", x => x.ID);
				});

			migrationBuilder.CreateTable(
				name: "Organizations",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Label = table.Column<string>(maxLength: 50, nullable: false),
					ParentOrganizationID = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Organizations", x => x.ID);
					table.ForeignKey(
						name: "FK_Organizations_Organizations_ParentOrganizationID",
						column: x => x.ParentOrganizationID,
						principalTable: "Organizations",
						principalColumn: "ID",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Persons",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					FirstName = table.Column<string>(nullable: true),
					LastName = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Persons", x => x.ID);
				});

			migrationBuilder.CreateTable(
				name: "Books",
				schema: "Scripture",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					FullTitle = table.Column<string>(nullable: true),
					Label = table.Column<string>(maxLength: 20, nullable: false),
					ParentID = table.Column<int>(nullable: true),
					ShortLabel = table.Column<string>(maxLength: 8, nullable: true),
					Summary = table.Column<string>(nullable: true),
					Title = table.Column<string>(maxLength: 200, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Books", x => x.ID);
					table.ForeignKey(
						name: "FK_Books_Books_ParentID",
						column: x => x.ParentID,
						principalSchema: "Scripture",
						principalTable: "Books",
						principalColumn: "ID",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "AccrualTypes",
				schema: "UtahEmployee",
				columns: table => new
				{
					ID = table.Column<byte>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Code = table.Column<string>(maxLength: 4, nullable: false),
					Label = table.Column<string>(maxLength: 100, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AccrualTypes", x => x.ID);
				});

			migrationBuilder.CreateTable(
				name: "EmployeePayPeriods",
				schema: "UtahEmployee",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					CheckDate = table.Column<DateTime>(nullable: false),
					EndDate = table.Column<DateTime>(nullable: false),
					StartDate = table.Column<DateTime>(nullable: false),
					YearRank = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EmployeePayPeriods", x => x.ID);
				});

			migrationBuilder.CreateTable(
				name: "PaidHolidayObservances",
				schema: "UtahEmployee",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Date = table.Column<DateTime>(nullable: false),
					EventLabelID = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PaidHolidayObservances", x => x.ID);
					table.ForeignKey(
						name: "FK_PaidHolidayObservances_EventLabels_EventLabelID",
						column: x => x.EventLabelID,
						principalSchema: "Calendar",
						principalTable: "EventLabels",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "SharedScriptures",
				schema: "CoupleScriptureLog",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Created = table.Column<DateTime>(nullable: false),
					PersonID = table.Column<int>(nullable: false),
					ScriptureReference = table.Column<string>(maxLength: 200, nullable: false),
					Shared = table.Column<DateTime>(nullable: false),
					SharingPersonID = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SharedScriptures", x => x.ID);
					table.ForeignKey(
						name: "FK_SharedScriptures_Persons_PersonID",
						column: x => x.PersonID,
						principalTable: "Persons",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_SharedScriptures_Persons_SharingPersonID",
						column: x => x.SharingPersonID,
						principalTable: "Persons",
						principalColumn: "ID",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "EmployeePayRate",
				schema: "UtahEmployee",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Effective = table.Column<DateTime>(nullable: false),
					PersonID = table.Column<int>(nullable: false),
					Rate = table.Column<decimal>(type: "DECIMAL(9,4)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EmployeePayRate", x => x.ID);
					table.ForeignKey(
						name: "FK_EmployeePayRate_Persons_PersonID",
						column: x => x.PersonID,
						principalTable: "Persons",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Hours",
				schema: "UtahEmployee",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					AccrualID = table.Column<int>(nullable: true),
					Date = table.Column<DateTime>(nullable: false),
					Hours = table.Column<double>(nullable: false),
					Note = table.Column<string>(nullable: true),
					PersonID = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Hours", x => x.ID);
					table.ForeignKey(
						name: "FK_Hours_Persons_PersonID",
						column: x => x.PersonID,
						principalTable: "Persons",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "WorkStamps",
				schema: "UtahEmployee",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					IsConsideredWorking = table.Column<bool>(nullable: false),
					IsLunch = table.Column<bool>(nullable: false),
					PersonID = table.Column<int>(nullable: false),
					Stamp = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_WorkStamps", x => x.ID);
					table.ForeignKey(
						name: "FK_WorkStamps_Persons_PersonID",
						column: x => x.PersonID,
						principalTable: "Persons",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Chapter",
				schema: "Scripture",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					BookID = table.Column<int>(nullable: false),
					Number = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Chapter", x => x.ID);
					table.ForeignKey(
						name: "FK_Chapter_Books_BookID",
						column: x => x.BookID,
						principalSchema: "Scripture",
						principalTable: "Books",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Accruals",
				schema: "UtahEmployee",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					AccrualTypeID = table.Column<byte>(nullable: false),
					PersonID = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Accruals", x => x.ID);
					table.ForeignKey(
						name: "FK_Accruals_AccrualTypes_AccrualTypeID",
						column: x => x.AccrualTypeID,
						principalSchema: "UtahEmployee",
						principalTable: "AccrualTypes",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Accruals_Persons_PersonID",
						column: x => x.PersonID,
						principalTable: "Persons",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Verses",
				schema: "Scripture",
				columns: table => new
				{
					ID = table.Column<long>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					ChapterID = table.Column<int>(nullable: false),
					Number = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Verses", x => x.ID);
					table.ForeignKey(
						name: "FK_Verses_Chapter_ChapterID",
						column: x => x.ChapterID,
						principalSchema: "Scripture",
						principalTable: "Chapter",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_SharedScriptures_PersonID",
				schema: "CoupleScriptureLog",
				table: "SharedScriptures",
				column: "PersonID");

			migrationBuilder.CreateIndex(
				name: "IX_SharedScriptures_SharingPersonID",
				schema: "CoupleScriptureLog",
				table: "SharedScriptures",
				column: "SharingPersonID");

			migrationBuilder.CreateIndex(
				name: "IX_Organizations_ParentOrganizationID",
				table: "Organizations",
				column: "ParentOrganizationID");

			migrationBuilder.CreateIndex(
				name: "IX_Books_ParentID",
				schema: "Scripture",
				table: "Books",
				column: "ParentID");

			migrationBuilder.CreateIndex(
				name: "IX_Chapter_BookID",
				schema: "Scripture",
				table: "Chapter",
				column: "BookID");

			migrationBuilder.CreateIndex(
				name: "IX_Verses_ChapterID",
				schema: "Scripture",
				table: "Verses",
				column: "ChapterID");

			migrationBuilder.CreateIndex(
				name: "IX_Accruals_AccrualTypeID",
				schema: "UtahEmployee",
				table: "Accruals",
				column: "AccrualTypeID");

			migrationBuilder.CreateIndex(
				name: "IX_Accruals_PersonID",
				schema: "UtahEmployee",
				table: "Accruals",
				column: "PersonID");

			migrationBuilder.CreateIndex(
				name: "IX_EmployeePayRate_PersonID",
				schema: "UtahEmployee",
				table: "EmployeePayRate",
				column: "PersonID");

			migrationBuilder.CreateIndex(
				name: "IX_Hours_PersonID",
				schema: "UtahEmployee",
				table: "Hours",
				column: "PersonID");

			migrationBuilder.CreateIndex(
				name: "IX_PaidHolidayObservances_EventLabelID",
				schema: "UtahEmployee",
				table: "PaidHolidayObservances",
				column: "EventLabelID");

			migrationBuilder.CreateIndex(
				name: "IX_WorkStamps_PersonID",
				schema: "UtahEmployee",
				table: "WorkStamps",
				column: "PersonID");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "SharedScriptures",
				schema: "CoupleScriptureLog");

			migrationBuilder.DropTable(
				name: "Organizations");

			migrationBuilder.DropTable(
				name: "Verses",
				schema: "Scripture");

			migrationBuilder.DropTable(
				name: "Accruals",
				schema: "UtahEmployee");

			migrationBuilder.DropTable(
				name: "EmployeePayPeriods",
				schema: "UtahEmployee");

			migrationBuilder.DropTable(
				name: "EmployeePayRate",
				schema: "UtahEmployee");

			migrationBuilder.DropTable(
				name: "Hours",
				schema: "UtahEmployee");

			migrationBuilder.DropTable(
				name: "PaidHolidayObservances",
				schema: "UtahEmployee");

			migrationBuilder.DropTable(
				name: "PunchHours",
				schema: "UtahEmployee");

			migrationBuilder.DropTable(
				name: "ViewPayPeriods",
				schema: "UtahEmployee");

			migrationBuilder.DropTable(
				name: "WorkStamps",
				schema: "UtahEmployee");

			migrationBuilder.DropTable(
				name: "Chapter",
				schema: "Scripture");

			migrationBuilder.DropTable(
				name: "AccrualTypes",
				schema: "UtahEmployee");

			migrationBuilder.DropTable(
				name: "EventLabels",
				schema: "Calendar");

			migrationBuilder.DropTable(
				name: "Persons");

			migrationBuilder.DropTable(
				name: "Books",
				schema: "Scripture");
		}
	}
}
