using CarTARge22.Data;
using CarTARge22.Models.Cars;
using Microsoft.AspNetCore.Mvc;

namespace CarTARge22.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarTARge22Context _context;

        public CarsController
            (
            CarTARge22Context context
            )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Cars
                .Select(x => new CarsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Brand = x.Brand,
                    Year = x.Year,
                    Transmission = x.Transmission,
                });
            return View();
        }
    }
}
