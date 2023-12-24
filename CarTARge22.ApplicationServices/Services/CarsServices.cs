using CarTARge22.Core.Domain;
using CarTARge22.Core.Dto;
using CarTARge22.Core.ServiceInterface;
using CarTARge22.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTARge22.ApplicationServices.Services
{
    public class CarsServices : ICarsServices
    {
        private readonly CarTARge22Context _context;

        public CarsServices
            (
            CarTARge22Context context
            )
        {
            _context = context;
        }

        public async Task<Car> Create( CarDto dto )
        {
            Car car = new Car();

            car.Id = Guid.NewGuid();
            car.Name = dto.Name;
            car.Brand = dto.Brand;
            car.Year = dto.Year;
            car.Transmission = dto.Transmission.ToString();
            car.Color = dto.Color;
            car.Fuel = dto.Fuel;
            car.TopSpeed = dto.TopSpeed;

            car.CreatedAt = DateTime.Now;
            car.ModifiedAt = DateTime.Now;

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> Update(CarDto dto)
        {
            Car domain = new();

            domain.Id = Guid.NewGuid();
            domain.Name = dto.Name;
            domain.Brand = dto.Brand;
            domain.Year = dto.Year;
            domain.Transmission = dto.Transmission.ToString();
            domain.Color = dto.Color;
            domain.Fuel = dto.Fuel;
            domain.TopSpeed = dto.TopSpeed;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();
            return domain;
        }

        public async Task<Car> DetailsAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Car> Delete (Guid id)
        {
            var carId = await _context.Cars
                .FirstOrDefaultAsync(x =>x.Id == id);

            _context.Cars .Remove(carId);
            await _context.SaveChangesAsync();

            return carId;
        }
    }
}
