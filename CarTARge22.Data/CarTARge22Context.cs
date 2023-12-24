using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using CarTARge22.Core.Domain;

namespace CarTARge22.Data
{
        public class CarTARge22Context : DbContext
        {
            public CarTARge22Context(DbContextOptions<CarTARge22Context> options) : base(options) { }

            public DbSet<Car> Cars { get; set; }

        }


}
