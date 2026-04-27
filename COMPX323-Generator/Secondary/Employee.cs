using COMPX323_Generator.Primary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Secondary
{
    public class Employee : Person
    {
        private int _irdNumber;
        private string _rank; // If you have an officers rank, you're an officer, if your rank is dispatcher, you're a dispatcher.
        private int badge_number; // unique or null, I'm pretty sure that's a constraint.


        public int IrdNumber
        {
            get { return _irdNumber; }
            set { _irdNumber = value; }
        }

        public string Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public int BadgeNumber
        {
            get { return badge_number; }
            set { badge_number = value; }
        }

        public Employee() : base()
        {
            // randomly pick ird, rank, badge
            // rest of the data is already randomly assigned by the base person constructor.
        }

        public override void AddDataToTable()
        {
            // Convert the employee and underlying person to an SQL command.
            // Add the person.
            // Get their person_id.
            // Query the IRD number and Badge Number to ensure they're unique
            // Reroll them if they aren't.
            // Add the employee.
        }

    }
}
