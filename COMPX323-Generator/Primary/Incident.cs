using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Secondary
{
    public class Incident
    {
        private DateTime _timestamp;
        private string _type;
        private string _address;
        private string _description;

        

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Incident()
        {
            // randomize timestamp, address
            // description template will list number of officers, etc, involved
            // query specific rows from those tables to get specific people.
            // IMPORTANT, RANDOM AN INTEGER, AND GET THAT ROW. We need to make sure that it respects the seed provided, not whatever the db decides.
            // construct description from template
            // construct sql command for adding incident to table
            // construct involved objects for each person involved
            // randomize the involved components, but ensure it matches with the incident.
            // construct sql commands for those involved, add them to involved table.
        }

        public virtual void AddDataToTable()
        {
            // convert the data to a SQL command, add it.
            // ID auto increments, so don't worry about it being unique.
        }
    }
}
