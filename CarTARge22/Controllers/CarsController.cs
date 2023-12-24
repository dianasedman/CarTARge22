﻿using CarTARge22.Core.Dto;
using CarTARge22.Core.ServiceInterface;
using CarTARge22.Data;
using CarTARge22.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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
                Transmission = (Core.Dto.TransmissionType)vm.Transmission,
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
        
    }
}
