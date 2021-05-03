using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIP6
{
    interface IMail
    {
        string Address { get; set; }
        int Index { get; set; }
        string City { get; set; }
        string Country { get; set; }
        string Name { get; set; }
        string ToString();
    }
}
