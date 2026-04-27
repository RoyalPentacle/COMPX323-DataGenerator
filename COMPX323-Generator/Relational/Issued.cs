using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Relational
{
    public class Issued
    {
        private DateTime _dateIssued;
        private DateTime _dateReturned;

        public DateTime DateIssued
        {
            get { return _dateIssued; }
            set { _dateIssued = value; }
        }

        public DateTime DateReturned
        {
            get { return _dateReturned; }
            set { _dateReturned = value; }
        }

        public Issued()
        {
            // Randomize date issued.
            // For some entries, randomize date returned, or NULL.
            // Grab a random assetID, grab a random employee IRD.
            // Ensure the employee you grab is of a role that makes sense, e.g. firearm to officer, not dispatcher.
            // Try to ensure things like multiple vehicles are not issued to the same officer at the same time, before a previous is returned.
            // Ensure no two assets are issued more than once without a return date on all but one entry.
        }
    }
}
