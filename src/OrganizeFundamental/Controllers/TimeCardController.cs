using OrganizeFundamental.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrganizeFundamental.Controllers
{
	[Route("Person/{personID:int}/TimeCard")]
	public class TimeCardController : Controller
	{
		readonly ApplicationDbContextFactory _applicationDbContextFactory;
		public TimeCardController(ApplicationDbContextFactory dbContextFactory)
		{
			_applicationDbContextFactory = dbContextFactory;
		}
		// GET: TimeCard
		public async Task<ActionResult> Index(int personID, int? payPeriodID = null)
		{
			var db = _applicationDbContextFactory.Create();

			var payPeriodsTask = db
				.ViewPayPeriods
				.ToListAsync();

			if (!payPeriodID.HasValue)
			{
				if(db.ViewPayPeriods.Any())
				{
					payPeriodID = await db.ViewPayPeriods.OrderByDescending(pp => pp.IsCurrent).ThenByDescending(pp => pp.StartDate).Select(pp => pp.ID).FirstAsync();
				}
				else
				{
					//No pay periods setup
					//TODO something that will return a view with message and link to setup pay periods
					//return View("");
				}
			}

			var timeCardDaysTask = db
				.PunchHours
				.Where(h =>
					h.PersonID == personID
					&& payPeriodID.HasValue && h.PayPeriodID == payPeriodID.Value)
				.GroupBy(h =>
					new
					{
						h.Date,
						h.PayPeriodID,
						h.PersonID,
					})
				.GroupJoin(db
					.WorkTimeCardEntries
					.Join(
						db.PunchPairs,
						tce => tce.StampID,
						p => p.StampID,
						(tce, p) =>
							new
							{
								p.DayPunchNum,
								tce.IsConsideredWorking,
								IsFirstInPunch = p.IsFirstInPunch ?? false,
								tce.IsLunch,
								IsOddPunch = p.IsOddPunch ?? false,
								HasPotentialError = p.HasPotentialError ?? false,
								p.NextStamp,
								tce.PayPeriodID,
								tce.PersonID,
								tce.StampID,
								tce.Stamp,
								p.TotalMinutesDifference
							}),
					d => new { d.Key.Date, d.Key.PersonID, d.Key.PayPeriodID },
					p => new { Date = p.Stamp.Date, p.PersonID, p.PayPeriodID },
					(d, p) =>
						new
						{
							d.Key.Date,
							d.Key.PayPeriodID,
							d.Key.PersonID,
							AggregatedHours = d.Select(h =>
								new
								{
									h.ActualHours,
									h.HasPotentialError,
									h.HoursTally,
									h.IsConsideredWorking,
									h.IsLunch,
									h.MinutesTally,
									h.RoundedHours
								}),
							Punches = p.Select(pr =>
								new
								{
									pr.DayPunchNum,
									pr.HasPotentialError,
									pr.IsConsideredWorking,
									pr.IsFirstInPunch,
									pr.IsLunch,
									pr.IsOddPunch,
									pr.NextStamp,
									pr.Stamp,
									pr.StampID,
									pr.TotalMinutesDifference
								})
						}
					)
				.ToListAsync();

			var payPeriods = await payPeriodsTask;
			var payPeriodDictionary = payPeriods
				.OrderByDescending(pp => pp.ID)
				.Select(pp =>
					new SelectListItem
					{
						Text = pp.YearRank + "(" + pp.StartDate + "-" + pp.EndDate + ")",
						Selected = pp.ID == payPeriodID.Value,
						Value = pp.ID.ToString()
					})
				.ToList();

			//build the time card using payperiod metadata
			var payPeriod = payPeriods.First(pp => pp.ID == payPeriodID.Value);
			

			var timeCardDays = await timeCardDaysTask;

			return View();
		}
	}
}