using COMPX323_Generator.Primary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Secondary
{
    public class Vehicle : Asset
    {
        private int _registrationNumber;

        public int RegistrationNumber
        {
            get { return _registrationNumber; }
            set { _registrationNumber = value; }
        }

        public Vehicle()
        {
            // randomize rego
            // randomize asset type and model, specifically for vehicles.
        }

        public override void AddDataToTable()
        {
            // Convert the vehicle and underlying asset to an SQL command.
            // Add the asset
            // Get their asset_id.
            // Query the Registration_Number to ensure it's unique.
            // Reroll it if it isn't.
            // Add the vehicle.
        }
    }
}
