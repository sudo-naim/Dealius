﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealius.Models
{
    class LeaseVariables
    {
        public string RateType { get; set; }
        public double RatePerSf { get; set; }
        public double Rate { get; set; }
        public double AnnualPercentageIncrease { get; set; }
        public int RentAbatement { get; set; }
        public double TenantRepFee { get; set; }
    }
}
