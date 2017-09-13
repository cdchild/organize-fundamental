using Microsoft.AspNetCore.Mvc;

namespace OrganizeFundamental.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult System()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
