using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Relational
{
    public class Employment
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public DateTime StartDate
        {
            get { return _startDate; } 
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public Employment()
        {
            // Roll start date.
            // Maybe roll end date.
            // Ensure every employee has atleast one employment.
            // Ensure every station has atleast one employment.
            // Ensure any employee with more than one employment record, has an end date on all but one of their records.
        }
    }
}
