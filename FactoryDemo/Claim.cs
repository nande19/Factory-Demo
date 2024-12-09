using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    internal class Claim
    {
        private string _state;

        // State property of the claim
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                // If the state of the claim changes to "complete", notify the related policy
                if (_state == "complete")
                {
                    Policy?.RemoveClaim();
                }
            }
        }

        // Reference to the related policy
        public Policy Policy { get; set; }
    }
}