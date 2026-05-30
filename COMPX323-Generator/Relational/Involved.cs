using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Relational
{
    public class Involved
    {
        private string _role;
        private string _description;

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Involved(int personID, int incidentID, bool officer)
        {
            // A complicated one.
            // Role needs to be randomized, but random within categories (officer/non-officer, etc)
            // Description of involvement must match the role, but also match the incident type.
            // Having incident type might be useful to have actually, so we know.
            // Regardless, random all those things appropriately, construct an SQL command, and send it.
            // We don't need to check anything, as this should only really be called when an incident is created.

            if (officer)
            {
                _role = "Officer";
                _description = "This guy showed up and saved the day, truly heroic";
            }
            else
            {
                switch(Form_DataGenerator.GlobalRandom.Next(3))
                {
                    case 0: 
                        _role = "Witness";
                        _description = "This guy saw EVERYTHING. Should really interview him.";
                        break;
                    case 1:
                        _role = "Suspect";
                        _description = "This guy may have done the deed.";
                        break;
                    case 2:
                        _role = "Victim";
                        _description = "This guy got it rough, what a shame.";
                        break;
                }
            }

            Debug.WriteLine("-----Involved");
            if (officer)
                Debug.WriteLine("---Officer");
            else
                Debug.WriteLine("---Civilian");
            Debug.WriteLine($"Person ID: {personID}");
            Debug.WriteLine($"Incident ID: {incidentID}");
            Debug.WriteLine($"Role: {_role}");
            Debug.WriteLine($"Description: {_description}");

            if (Form_DataGenerator.OracleDB)
            {
                string comm = $@"INSERT INTO A_Involved (person_id, incident_id, role, description) VALUES (
                {personID},
                {incidentID},
                '{_role}',
                '{_description}'
                )";
                Debug.WriteLine(comm);
                if (Form_DataGenerator.SQLWriter != null)
                {
                    Form_DataGenerator.SQLWriter.Write(comm);
                    Form_DataGenerator.SQLWriter.WriteLine(";");
                }
                Form_DataGenerator.ExecuteOracleCommand(comm);
            }
        }

    }
}
