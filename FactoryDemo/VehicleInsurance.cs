using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    internal class VehicleInsurance : InsurancePolicy
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public int YearOfRegistration { get; set; }
    }
}
