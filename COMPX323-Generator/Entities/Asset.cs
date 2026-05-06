using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Entity
{
    public class Asset
    {
        protected string _type;
        protected string _model;

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

        public Asset()
        {
            // randomize type and model
            // Specifically for equipment, not firearms or vehicles.
        }


        public virtual void AddDataToTable()
        {
            // convert the data to a SQL command, add it.
            // ID auto increments, so don't worry about it being unique.
        }

    }
}
