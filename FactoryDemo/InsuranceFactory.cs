using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    abstract class InsuranceFactory
    {
        public abstract InsurancePolicy CreatePolicy();

    }
}
