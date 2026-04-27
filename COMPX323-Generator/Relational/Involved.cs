using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Relational
{
    public class Involved
    {
        private string _role;
        private string _description;

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Involved(int personID, int incidentID)
        {
            // A complicated one.
            // Role needs to be randomized, but random within categories (officer/non-officer, etc)
            // Description of involvement must match the role, but also match the incident type.
            // Having incident type might be useful to have actually, so we know.
            // Regardless, random all those things appropriately, construct an SQL command, and send it.
            // We don't need to check anything, as this should only really be called when an incident is created.

            // Whoops, some of this should be in an AddDataToTable function.
        }
    }
}
