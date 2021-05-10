using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealius.Models
{
     class CalculatorDealInfo
    {
        public DateTime StartDate { get; set; }
        public string LeaseType { get; set; }
        public int Term { get; set; }
        public int SpaceRequired { get; set; }

        public double RatePerSf { get; set; }
        public int TenantRepFee { get; set; }
    }
}
