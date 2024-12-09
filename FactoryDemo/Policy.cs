using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    internal class Policy
    {
        // Event handler delegate for ClaimCompleted event
        public delegate void ClaimCompletedHandler(object sender, EventArgs e);

        // Event that gets triggered when a claim is completed
        public event ClaimCompletedHandler ClaimCompleted;

        // Number of claims against the policy
        private int _numberOfClaims;

        public int NumberOfClaims
        {
            get { return _numberOfClaims; }
            private set
            {
                _numberOfClaims = value;
                if (_numberOfClaims == 0)
                {
                    OnClaimCompleted(EventArgs.Empty); // Trigger event when all claims are completed
                }
            }
        }

        // Constructor
        public Policy()
        {
            NumberOfClaims = 0;
        }

        // Method to add a claim
        public void AddClaim()
        {
            NumberOfClaims++;
        }

        // Method to remove a claim
        public void RemoveClaim()
        {
            NumberOfClaims--;
        }

        // Method to notify observers when a claim is completed
        protected virtual void OnClaimCompleted(EventArgs e)
        {
            ClaimCompleted?.Invoke(this, e);
        }
    }
}