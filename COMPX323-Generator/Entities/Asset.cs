using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Entity
{
    public class Asset
    {
        protected string _type;
        protected string _model;

        public static int _newestID = 0;

        public static int VehicleIDStart;

        public static int FirearmIDStart;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public Asset(string type)
        {
            _type = type;
            if (type == "EQUIPMENT")
                _model = File.ReadLines(@"Data/equipment.txt").Skip(Form_DataGenerator.GlobalRandom.Next(83)).FirstOrDefault();
            if (type == "VEHICLE")
                _model = File.ReadLines(@"Data/vehicles.txt").Skip(Form_DataGenerator.GlobalRandom.Next(97)).FirstOrDefault();
            if (type == "FIREARM")
                _model = File.ReadLines(@"Data/firearms.txt").Skip(Form_DataGenerator.GlobalRandom.Next(94)).FirstOrDefault();
            _model = _model.Replace("\'", "\'\'");
            AddDataToTable();
        }


        private void AddDataToTable()
        {
            if (Form_DataGenerator.OracleDB)
            {
                Debug.WriteLine("-----Asset");
                Debug.WriteLine($"Type: {_type}");
                Debug.WriteLine($"Model: {_model}");


                string comm = $@"INSERT INTO A_Assets (type, model) VALUES (
                '{_type}',
                '{_model}'
                )";

                Debug.WriteLine(comm);
                Form_DataGenerator.ExecuteDBCommand(comm);
                _newestID++;
            }
            else
            {
                //MongoDB
            }
        }

    }
}
