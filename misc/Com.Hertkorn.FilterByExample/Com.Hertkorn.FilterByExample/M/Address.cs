using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.FilterByExample.M
{
    public class Address
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public Country Country { get; set; }
    }
}
