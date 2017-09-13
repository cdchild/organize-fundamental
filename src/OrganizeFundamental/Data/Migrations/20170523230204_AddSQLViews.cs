using Microsoft.EntityFrameworkCore.Migrations;

namespace OrganizeFundamental.Data.Migrations
{
	public partial class AddSQLViews : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
CREATE VIEW [UtahEmployee].[AccrualHours] AS (
SELECT
 p.FirstName, p.LastName,
 pp.ID PayPeriodID, pp.YearRank, pp.StartDate, pp.EndDate, pp.CheckDate, pp.IsCurrent, pp.IsFuture, pp.IsPast,
 a.PersonID, a.AccrualTypeID,
 t.Code, t.Label,
 h.AccrualID, h.Note, h.ID HourID, h.Date, h.Hours, SUM(h.Hours) OVER(PARTITION BY a.ID ORDER BY h.Date ASC, h.ID DESC ROWS UNBOUNDED PRECEDING) Balance
FROM UtahEmployee.Hours h
 JOIN UtahEmployee.ViewPayPeriods pp ON h.Date BETWEEN pp.StartDate AND DATEADD(DAY, 1, pp.EndDate)
 JOIN UtahEmployee.Accruals a ON a.ID = h.AccrualID
 JOIN UtahEmployee.AccrualTypes t ON t.ID = a.AccrualTypeID
 JOIN dbo.Persons p ON p.ID = a.PersonID)

GO

CREATE VIEW  [UtahEmployee].[PunchHours] AS
(
SELECT
	PunchPair.PersonID,
	PunchPair.FirstName,
	PunchPair.LastName,
	PunchPair.PayPeriodID,
	PunchPair.YearRank,
	PunchPair.StartDate,
	PunchPair.EndDate,
	PunchPair.CheckDate,
	PunchPair.IsCurrentPayPeriod,
	PunchPair.IsFuturePayPeriod,
	PunchPair.IsPastPayPeriod,
	CAST(PunchPair.Stamp AS DATE) [Date],
	PunchPair.IsConsideredWorking,
	PunchPair.IsLunch,
	CAST(CASE WHEN MAX(PunchPair.HasPotentialError) > 0 THEN 1 ELSE 0 END AS BIT) HasPotentialError,
	SUM(PunchPair.TotalMinutesDifference) / 60 [HoursTally],
	SUM(PunchPair.TotalMinutesDifference) % 60 [MinutesTally],
	CAST(SUM(PunchPair.TotalMinutesDifference) AS DOUBLE PRECISION) / 60 [ActualHours],
	(SUM(PunchPair.TotalMinutesDifference) / 60) + ROUND(CAST((SUM(PunchPair.TotalMinutesDifference) % 60) AS DOUBLE PRECISION)/15, 0) * 15 / 60 [RoundedHours]
FROM (
	SELECT
		Punches.PersonID ,
		Punches.FirstName ,
		Punches.LastName ,
		Punches.PayPeriodID ,
		Punches.YearRank ,
		Punches.StartDate ,
		Punches.EndDate ,
		Punches.CheckDate ,
		Punches.IsCurrentPayPeriod,
		Punches.IsFuturePayPeriod,
		Punches.IsPastPayPeriod,
		Punches.StampID ,
		Punches.Stamp ,
		Punches.IsConsideredWorking ,
		Punches.IsLunch ,
		Punches.IsFirstInPunch ,
		Punches.DayPunchNum,
		(Punches.DayPunchNum % 2) IsOddPunch,
		CASE WHEN Punches.IsFirstInPunch = 1 AND (Punches.DayPunchNum <> 1 OR Punches.IsConsideredWorking <> 1) THEN 1 ELSE 0 END HasPotentialError,
		MIN(stampsOccurringAfter.Stamp) NextStamp,
		DATEDIFF(MINUTE,Punches.Stamp,MIN(stampsOccurringAfter.Stamp)) TotalMinutesDifference
	FROM (
		SELECT
			p.ID PersonID,
			p.FirstName,
			p.LastName,
			pp.ID PayPeriodID,
			pp.YearRank,
			pp.StartDate,
			pp.EndDate,
			pp.CheckDate,
			pp.IsCurrent IsCurrentPayPeriod,
			pp.IsFuture IsFuturePayPeriod,
			pp.IsPast IsPastPayPeriod,
			s.ID StampID,
			s.Stamp,
			s.IsConsideredWorking,
			s.IsLunch,
			CASE WHEN MIN(sameDayWorkingStamps.Stamp) = s.Stamp THEN 1 ELSE 0 END IsFirstInPunch,
			ROW_NUMBER() OVER(PARTITION BY DATEPART(YEAR, s.Stamp), DATEPART(DAYOFYEAR, s.Stamp) ORDER BY s.Stamp) DayPunchNum
		FROM
			UtahEmployee.WorkStamps s
			JOIN dbo.Persons p ON p.ID = s.PersonID
			JOIN UtahEmployee.ViewPayPeriods pp ON s.Stamp BETWEEN pp.StartDate AND DATEADD(DAY, 1, pp.EndDate)
			LEFT OUTER JOIN UtahEmployee.WorkStamps sameDayWorkingStamps
				ON DATEPART(YEAR,sameDayWorkingStamps.Stamp) = DATEPART(YEAR,s.Stamp)
				 AND DATEPART(DAYOFYEAR, sameDayWorkingStamps.Stamp) = DATEPART(DAYOFYEAR, s.Stamp)
				 AND sameDayWorkingStamps.PersonID = s.PersonID
				 AND sameDayWorkingStamps.IsConsideredWorking = 1
		GROUP BY
			p.ID,
			p.FirstName,
			p.LastName,
			pp.ID,
			pp.YearRank,
			pp.StartDate,
			pp.EndDate,
			pp.CheckDate,
			pp.IsCurrent,
			pp.IsFuture,
			pp.IsPast,
			s.ID,
			s.Stamp,
			s.IsConsideredWorking,
			s.IsLunch
		) AS Punches
		JOIN UtahEmployee.WorkStamps stampsOccurringAfter
			ON DATEPART(YEAR,stampsOccurringAfter.Stamp) = DATEPART(YEAR,Punches.Stamp)
			AND DATEPART(DAYOFYEAR, stampsOccurringAfter.Stamp) = DATEPART(DAYOFYEAR,Punches.Stamp)
			AND stampsOccurringAfter.PersonID = Punches.PersonID
			AND Punches.Stamp < stampsOccurringAfter.Stamp
	GROUP BY
		Punches.PersonID,
		Punches.FirstName,
		Punches.LastName,
		Punches.PayPeriodID,
		Punches.YearRank,
		Punches.StartDate,
		Punches.EndDate,
		Punches.CheckDate,
		Punches.IsCurrentPayPeriod,
		Punches.IsFuturePayPeriod,
		Punches.IsPastPayPeriod,
		Punches.StampID,
		Punches.Stamp,
		Punches.IsConsideredWorking,
		Punches.IsLunch,
		Punches.IsFirstInPunch,
		Punches.DayPunchNum
	) AS PunchPair
GROUP BY
	PunchPair.PersonID,
	PunchPair.FirstName,
	PunchPair.LastName,
	PunchPair.PayPeriodID,
	PunchPair.YearRank,
	PunchPair.StartDate,
	PunchPair.EndDate,
	PunchPair.CheckDate,
	PunchPair.IsCurrentPayPeriod,
	PunchPair.IsFuturePayPeriod,
	PunchPair.IsPastPayPeriod,
	CAST(PunchPair.Stamp AS DATE),
	PunchPair.IsConsideredWorking,
	PunchPair.IsLunch
)

GO

CREATE VIEW [UtahEmployee].[ViewPayPeriods] AS (
SELECT
	pp.ID,
	pp.YearRank,
	pp.StartDate,
	pp.EndDate,
	pp.CheckDate,
	CAST(CASE WHEN GETDATE() BETWEEN pp.StartDate AND DATEADD(DAY, 1, pp.EndDate) THEN 1 ELSE 0 END AS BIT) IsCurrent,
	CAST(CASE WHEN GETDATE() < pp.StartDate THEN 1 ELSE 0 END AS BIT) IsFuture,
	CAST(CASE WHEN DATEADD(DAY, 1, pp.EndDate) < GETDATE() THEN 1 ELSE 0 END AS BIT) IsPast
FROM UtahEmployee.EmployeePayPeriods pp)

GO

CREATE VIEW [UtahEmployee].[WorkTimeCardEntries] AS
(
	SELECT
		p.ID PersonID,
		p.FirstName,
		p.LastName,
		pp.ID PayPeriodID,
		pp.YearRank,
		pp.StartDate,
		pp.EndDate,
		pp.CheckDate,
		pp.IsCurrent IsCurrentPayPeriod,
		pp.IsFuture IsFuturePayPeriod,
		pp.IsPast IsPastPayPeriod,
		s.ID StampID,
		s.Stamp,
		s.IsConsideredWorking,
		s.IsLunch
	FROM
		UtahEmployee.WorkStamps s
		JOIN dbo.Persons p ON p.ID = s.PersonID
		JOIN UtahEmployee.ViewPayPeriods pp ON s.Stamp BETWEEN pp.StartDate AND DATEADD(DAY, 1, pp.EndDate)
)
");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
DROP VIEW [UtahEmployee].[AccrualHours]

DROP VIEW [UtahEmployee].[PunchHours]

DROP VIEW [UtahEmployee].[ViewPayPeriods]

DROP VIEW [UtahEmployee].[WorkTimeCardEntries]

");

		}
	}
}
