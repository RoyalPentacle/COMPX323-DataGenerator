using COMPX323_Generator.Primary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Secondary
{
    public class Firearm : Asset
    {
        private int _serialNumber;
        
        public int SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        public Firearm()
        {
            // Randomize serial number
            // Randomize Asset type and model, specifically for firearms.
        }

        public override void AddDataToTable()
        {
            // Convert the firearm and underlying asset to an SQL command.
            // Add the asset
            // Get their asset_id.
            // Query the Serial_Number to ensure it's unique.
            // Reroll it if it isn't.
            // Add the firearm.
        }
    }
}
