using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
     class VehicleInsuranceFactory : InsuranceFactory
    {
        public override InsurancePolicy CreatePolicy()
        {
            return new VehicleInsurance();
        }
    }
}
