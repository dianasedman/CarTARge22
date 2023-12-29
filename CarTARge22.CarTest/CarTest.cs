using CarTARge22.Core.Dto;
using CarTARge22.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTARge22.CarTest
{
    public class CarTest : TestBase
    {

        [Fact]
        public async Task ShouldNot_DeleteByIdCar_WhenDidNotDeleteCar()
        {
            CarDto car = MockCarData();

            var carOne = await Svc<ICarsServices>().Create(car);
            var carTwo = await Svc<ICarsServices>().Create(car);

            var result = await Svc<ICarsServices>().Delete((Guid)carOne.Id);

            Assert.NotEqual(result.Id, carTwo.Id);
        }

        [Fact]
        public async Task ShouldNot_AddEmptyCar_WhenReturnResult()
        {
            //Arrange
            CarDto car = new();

            car.Name = "Accord";
            car.Brand = "Honda";
            car.Year = DateTime.Now;
            car.Transmission = TransmissionType.Automatic;
            car.Color = "white";
            car.Fuel = "Regular";
            car.TopSpeed = 200;
            car.CreatedAt = DateTime.Now;
            car.ModifiedAt = DateTime.Now;

            //Act
            var result = await Svc<ICarsServices>().Create(car);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdCar_WhenReturnNotEqual()
        {
            CarDto car = new();
            Guid wrongId = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("1e1426e3-6846-4105-1e44-f9e5fb9f8b25");

            await Svc<ICarsServices>().DetailsAsync(guid);

            Assert.NotEqual(wrongId, guid);
        }

        [Fact]
        public async Task ShouldNot_AddEmptyCAr_WhenReturnResult()
        {
            CarDto car = MockCarData();

            var result = await Svc<ICarsServices>().Create(car);

            Assert.NotNull(result);
        }

        private CarDto MockCarData()
        {
            CarDto car = new()
            {
                Id = null,
                Name = "E46",
                Brand = "BMW",
                Year = DateTime.Now.AddYears(-25),
                Transmission = TransmissionType.Manual,
                Color = "gray",
                Fuel = "Petrol",
                TopSpeed = 125,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,

            };

            return car;
        }
    }
}
