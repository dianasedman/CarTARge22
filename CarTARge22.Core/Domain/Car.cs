﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTARge22.Core.Domain
{
    public class Car
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public DateTime Year { get; set; }
        public string Transmission { get; set; }

        public string Color { get; set; }
        public string Fuel { get; set; }
        public int TopSpeed { get; set; }

        //For database only
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
