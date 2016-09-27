using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizeFundamental.Models;

namespace OrganizeFundamental.Controllers
{
	public class PersonController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PersonController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Person
		public async Task<IActionResult> Index()
		{
			return View(await _context.Persons.ToListAsync());
		}

		// GET: Person/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var person = await _context.Persons.SingleOrDefaultAsync(m => m.ID == id);
			if (person == null)
			{
				return NotFound();
			}

			return View(person);
		}

		// GET: Person/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Person/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,FirstName,LastName")] Person person)
		{
			if (ModelState.IsValid)
			{
				_context.Add(person);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(person);
		}

		// GET: Person/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var person = await _context.Persons.SingleOrDefaultAsync(m => m.ID == id);
			if (person == null)
			{
				return NotFound();
			}
			return View(person);
		}

		// POST: Person/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName")] Person person)
		{
			if (id != person.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(person);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PersonExists(person.ID))
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
			return View(person);
		}

		// GET: Person/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var person = await _context.Persons.SingleOrDefaultAsync(m => m.ID == id);
			if (person == null)
			{
				return NotFound();
			}

			return View(person);
		}

		// POST: Person/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var person = await _context.Persons.SingleOrDefaultAsync(m => m.ID == id);
			_context.Persons.Remove(person);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool PersonExists(int id)
		{
			return _context.Persons.Any(e => e.ID == id);
		}
	}
}
