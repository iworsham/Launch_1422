using Microsoft.AspNetCore.Mvc;
using CaddyShackMVC.DataAccess;
using CaddyShackMVC.Models;

using Microsoft.EntityFrameworkCore;

namespace CaddyShackMVC.Controllers
{
	public class GolfBagsController : Controller
	{
		private readonly CaddyShackContext _context;

		public GolfBagsController(CaddyShackContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var golfBags = _context.GolfBags.ToList();
			return View(golfBags);
		}
		[Route("/Golfbags/{golfbagId:int}")]
		public IActionResult Show(int golfBagId)
		{
            var golfBag = _context.GolfBags
                .Where(g => g.Id == golfBagId)
                .Include(g => g.Clubs)
                .FirstOrDefault();
            return View(golfBag);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var golfbag = _context.GolfBags.Find(Id);
            _context.GolfBags.Remove(golfbag);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult New()
        {
            return View();
        }
   
        [HttpPost]
        public IActionResult Create(GolfBag golfBag)
        {

            _context.GolfBags.Add(golfBag);
            _context.SaveChanges();
            var newgolfBagId = golfBag.Id;
            return RedirectToAction("show");
        }
        [Route("/golfbags/edit/{golfBagId:int}")]
        public IActionResult Edit(int golfBagId)
        {
            var golfBag = _context.GolfBags.Find(golfBagId);
            return View(golfBag);
        }

        [HttpPost]
        public IActionResult Update(GolfBag golfBag, int id)
        {
            golfBag.Id = id;
            _context.GolfBags.Update(golfBag);
            _context.SaveChanges();

            return Redirect($"/golfbags/show/{golfBag.Id}");
        }

    }
}
