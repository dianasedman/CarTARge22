using CarTARge22.Core.Dto;
using CarTARge22.Core.ServiceInterface;
using CarTARge22.Data;
using CarTARge22.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TransmissionType = CarTARge22.Core.Dto.TransmissionType;

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
                Transmission = (TransmissionType)vm.Transmission,
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carsServices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarsDetailsViewModel();
            vm.Id = id;
            vm.Name = car.Name;
            vm.Brand = car.Brand;
            vm.Year = car.Year;
            vm.Transmission = car.Transmission;
            vm.Color = car.Color;
            vm.Fuel = car.Fuel;
            vm.TopSpeed = car.TopSpeed;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carsServices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarsCreateUdateVIewModel();
            vm.Id = id;
            vm.Name = car.Name;
            vm.Brand = car.Brand;
            vm.Year = car.Year;
            if (Enum.TryParse(typeof(TransmissionType), car.Transmission, out var transmission))
            {
                vm.Transmission = (Models.Cars.TransmissionType)(TransmissionType)transmission;
            }
            else
            {
                return NotFound();
            }
            vm.Color = car.Color;
            vm.Fuel = car.Fuel;
            vm.TopSpeed = car.TopSpeed;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarsCreateUdateVIewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Brand = vm.Brand,
                Year = vm.Year,
                Transmission = (TransmissionType)vm.Transmission,
                Color = vm.Color,
                Fuel = vm.Fuel,
                TopSpeed = vm.TopSpeed,

                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };
            var result = await _carsServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carsServices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarsDeleteViewModel();

            vm.Id = car.Id;
            vm.Name = car.Name;
            vm.Brand = car.Brand;
            vm.Year = car.Year;
            vm.Transmission = car.Transmission;
            vm.Color = car.Color;
            vm.Fuel = car.Fuel;
            vm.TopSpeed = car.TopSpeed;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _carsServices.Delete(id);

            if (carId == null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
