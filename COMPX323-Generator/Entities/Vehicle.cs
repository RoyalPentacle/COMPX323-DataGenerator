using COMPX323_Generator.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COMPX323_Generator.Entity
{
    public class Vehicle : Asset
    {
        private string _registrationNumber;
        private static HashSet<string> _usedRego = new HashSet<string>();

        public string RegistrationNumber
        {
            get { return _registrationNumber; }
            set { _registrationNumber = value; }
        }

        public Vehicle() : base("VEHICLE")
        {
            GenerateRego();
            AddDataToTable();
        }

        private void GenerateRego()
        {
            _registrationNumber = $"{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0')}";
        }

        private void AddDataToTable()
        {
            // Convert the vehicle and underlying asset to an SQL command.
            // Add the asset
            // Get their asset_id.
            // Query the Registration_Number to ensure it's unique.
            // Reroll it if it isn't.
            // Add the vehicle.

            if (Form_DataGenerator.OracleDB)
            {
                bool uniqueRego = false;
                while (!uniqueRego)
                {
                    uniqueRego = !_usedRego.Contains(_registrationNumber);
                    if (!uniqueRego)
                        GenerateRego();
                }
                Debug.WriteLine("-----Vehicle");
                Debug.WriteLine($"Rego: {_registrationNumber}");
                Debug.WriteLine($"Asset ID: {_newestID}");

                string comm = $@"INSERT INTO A_Vehicles (registration_number, asset_id) VALUES (
                '{_registrationNumber}',
                {_newestID}
                )";
                Debug.WriteLine(comm);
                if (Form_DataGenerator.SQLWriter != null)
                {
                    Form_DataGenerator.SQLWriter.Write(comm);
                    Form_DataGenerator.SQLWriter.WriteLine(";");
                }
                Form_DataGenerator.ExecuteOracleCommand(comm);
                _usedRego.Add(_registrationNumber);
            }
            else
            {
                //MongoDB
            }

        }
    }
}
