using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Entity
{
    public class Station
    {
        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public Station()
        {
            // Randomize the address.
            // Query the address to check if it already exists
            // Reroll if it already exists
            // Construct sql command to add station to table.
        }
    }
}
