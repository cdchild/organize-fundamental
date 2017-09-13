using OrganizeFundamental.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrganizeFundamental.Controllers
{
	public class TimeCardController : Controller
	{
		readonly ApplicationDbContextFactory _dbContextFactory;
		public TimeCardController(ApplicationDbContextFactory dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}
		// GET: TimeCard
		public async Task<ActionResult> Index(int personID, int? payPeriodID = null)
		{
			var db = _dbContextFactory.Create();

			var payPeriodsTask = db
				.ViewPayPeriods
				.ToListAsync();

			if (!payPeriodID.HasValue)
			{
				if(await db.ViewPayPeriods.AnyAsync())
				{
					payPeriodID = await db.ViewPayPeriods.OrderByDescending(pp => pp.IsCurrent).ThenByDescending(pp => pp.StartDate).Select(pp => pp.ID).FirstAsync();
				}
				else
				{
					//No pay periods setup
					return View("NeedPayPeriods");
				}
			}

			var timeCardDaysTask = (
				from h in db.PunchHours
				where h.PersonID == personID
				where payPeriodID.HasValue && h.PayPeriodID == payPeriodID.Value
				where h.Date.HasValue
				group h by new
					{
						Date = h.Date.Value,
						h.PayPeriodID,
						h.PersonID,
					} into d

				join tce in db.WorkTimeCardEntries
					on d.Key equals new { Date = tce.Stamp.Date, tce.PayPeriodID, tce.PersonID }
					into pp
				select new
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
						Punches = pp.Select(pr =>
							new
							{
								pr.IsConsideredWorking,
								pr.IsLunch,
								pr.Stamp,
								pr.StampID
							})
					})
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

		public async Task<IActionResult> GetTimeCardPrintOut(int id, int ein, int days)
		{
			var personID = id;

			var date = System.DateTime.Today.AddDays(days);

			using (var db = _dbContextFactory.Create())
			{
				var personTask = _dbContextFactory.Create().Persons.SingleOrDefaultAsync(p => p.ID == personID);

				var payPeriodTask = _dbContextFactory.Create().EmployeePayPeriods.SingleOrDefaultAsync(pp => pp.StartDate <= date && date <= pp.EndDate);

				var hoursTask = (
					from h in db.PunchHours
					where h.PersonID == personID
					where h.StartDate <= date
					where date <= h.EndDate
					where h.Date.HasValue
					where h.IsConsideredWorking
					group h by h.Date.Value into d
					select d)
					.ToListAsync();

				ViewData["Person"] = await personTask;
				ViewData["PayPeriod"] = await payPeriodTask;
				ViewData["EIN"] = ein;

				return View(await hoursTask);
			}
		}
	}
}