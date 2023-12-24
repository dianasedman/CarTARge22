using CarTARge22.Core.Dto;
using CarTARge22.Core.ServiceInterface;
using CarTARge22.Data;
using CarTARge22.Models.Cars;
using Microsoft.AspNetCore.Mvc;

namespace CarTARge22.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarTARge22Context _context;
        private readonly ICarsServices _carsServices;

        public CarsController
            (
            CarTARge22Context context,
            ICarsServices carsServices
            )
        {
            _context = context;
            _carsServices = carsServices;
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
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarsCreateUdateVIewModel result = new CarsCreateUdateVIewModel();
            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarsCreateUdateVIewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Brand = vm.Brand,
                Year = vm.Year,
                Transmission = vm.Transmission,
                Color = vm.Color,
                Fuel = vm.Fuel,
                TopSpeed = vm.TopSpeed,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };
            var result = await _carsServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }
    }
}
