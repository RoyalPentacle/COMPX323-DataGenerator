using COMPX323_Generator.Entity;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COMPX323_Generator.MongoEntity
{
    public class MongoVehicle : MongoAsset
    {
        private string _registrationNumber;
        private static HashSet<string> _usedRego = new HashSet<string>();

        [BsonElement("registration_number")]
        public string RegistrationNumber
        {
            get { return _registrationNumber; }
            set { _registrationNumber = value; }
        }

        public MongoVehicle() : base("VEHICLE")
        {
            GenerateRego(); 
            bool uniqueRego = false;
            while (!uniqueRego)
            {
                uniqueRego = !_usedRego.Contains(_registrationNumber);
                if (!uniqueRego)
                    GenerateRego();
            }
            _usedRego.Add(_registrationNumber);
        }

        private void GenerateRego()
        {
            _registrationNumber = $"{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{(char)Form_DataGenerator.GlobalRandom.Next(65, 91)}{Form_DataGenerator.GlobalRandom.Next(1000).ToString().PadLeft(3, '0')}";
        }
    }
}
