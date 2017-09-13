using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizeFundamental.Models;
using OrganizeFundamental.Models.UtahEmployee;

namespace OrganizeFundamental.Controllers
{
	public class EmployeePayPeriodsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public EmployeePayPeriodsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: EmployeePayPeriods
		public async Task<IActionResult> Index()
		{
			return View(await _context.EmployeePayPeriods.ToListAsync());
		}

		// GET: EmployeePayPeriods/Create
		public IActionResult Create()
		{
			ViewData["IsCreating"] = true;

			return View();
		}

		// POST: EmployeePayPeriods/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,CheckDate,EndDate,StartDate,YearRank")] EmployeePayPeriod employeePayPeriod)
		{
			if (ModelState.IsValid)
			{
				_context.Add(employeePayPeriod);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewData["IsCreating"] = true;

			return View(employeePayPeriod);
		}

		// GET: EmployeePayPeriods/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var employeePayPeriod = await _context.EmployeePayPeriods.SingleOrDefaultAsync(m => m.ID == id);
			if (employeePayPeriod == null)
			{
				return NotFound();
			}
			return View("Create", employeePayPeriod);
		}

		// POST: EmployeePayPeriods/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,CheckDate,EndDate,StartDate,YearRank")] EmployeePayPeriod employeePayPeriod)
		{
			if (id != employeePayPeriod.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(employeePayPeriod);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EmployeePayPeriodExists(employeePayPeriod.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction("Index");
			}
			return View("Create", employeePayPeriod);
		}

		// GET: EmployeePayPeriods/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var employeePayPeriod = await _context.EmployeePayPeriods.SingleOrDefaultAsync(m => m.ID == id);
			if (employeePayPeriod == null)
			{
				return NotFound();
			}

			return View(employeePayPeriod);
		}

		// POST: EmployeePayPeriods/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var employeePayPeriod = await _context.EmployeePayPeriods.SingleOrDefaultAsync(m => m.ID == id);
			_context.EmployeePayPeriods.Remove(employeePayPeriod);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool EmployeePayPeriodExists(int id)
		{
			return _context.EmployeePayPeriods.Any(e => e.ID == id);
		}
	}
}
