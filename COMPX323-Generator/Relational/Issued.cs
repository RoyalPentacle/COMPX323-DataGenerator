using COMPX323_Generator.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPX323_Generator.Relational
{
    public class Issued
    {
        private DateTime _dateIssued;
        private DateTime? _dateReturned;

        public DateTime DateIssued
        {
            get { return _dateIssued; }
            set { _dateIssued = value; }
        }

        public DateTime? DateReturned
        {
            get { return _dateReturned; }
            set { _dateReturned = value; }
        }

        private struct Issuance
        {
            public DateTime Issued;
            public DateTime? Returned;
            
            public Issuance (DateTime issued, DateTime? returned)
            {
                Issued = issued;
                Returned = returned;
            }
        }

        private void Issue(int minID, int maxID, string debugType)
        {
            List<Issuance> issues = new List<Issuance>();
            for (int i = minID; i < maxID; i++)
            {
                Form_DataGenerator.UpdateLabel($"Generating Issuance {i + 1}/{Asset._newestID} :: {Form_DataGenerator.GeneratorProgress}/{Form_DataGenerator.GeneratorMaxProgress}");
                issues.Clear();
                int year = Form_DataGenerator.GlobalRandom.Next(1970, 1973);
                int month = Form_DataGenerator.GlobalRandom.Next(1, 13);
                int day = Form_DataGenerator.GlobalRandom.Next(1, 29);
                _dateIssued = new DateTime(year, month, day);

                // Means some assets will not have issuance, but most will have multiple.
                while (Form_DataGenerator.GlobalRandom.Next(100) > 25)
                {
                    year = Form_DataGenerator.GlobalRandom.Next(_dateIssued.Year, _dateIssued.Year + 3);
                    if (year == _dateIssued.Year)
                    {
                        month = Form_DataGenerator.GlobalRandom.Next(_dateIssued.Month, 13);
                        if (month == _dateIssued.Month)
                        {
                            day = Form_DataGenerator.GlobalRandom.Next(_dateIssued.Day, 29);
                        }
                    }
                    else
                    {
                        month = Form_DataGenerator.GlobalRandom.Next(1, 13);
                        day = Form_DataGenerator.GlobalRandom.Next(1, 29);
                    }
                    _dateReturned = new DateTime(year, month, day);
                    issues.Add(new Issuance(_dateIssued, _dateReturned));

                    year = Form_DataGenerator.GlobalRandom.Next(_dateReturned.Value.Year, DateTime.Now.Year);
                    if (year == _dateReturned.Value.Year)
                    {
                        month = Form_DataGenerator.GlobalRandom.Next(_dateReturned.Value.Month, 13);
                        if (month == _dateReturned.Value.Month)
                        {
                            day = Form_DataGenerator.GlobalRandom.Next(_dateReturned.Value.Day, 29);
                            if (day == _dateReturned.Value.Day)
                            {
                                month += 1;
                                if (month > 12)
                                {
                                    month = 1;
                                    year += 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        month = Form_DataGenerator.GlobalRandom.Next(1, 13);
                        day = Form_DataGenerator.GlobalRandom.Next(1, 29);
                    }

                    _dateIssued = new DateTime(year, month, day);
                    if (year >= DateTime.Now.Year - 1)
                    {
                        _dateReturned = null;
                        issues.Add(new Issuance(_dateIssued, _dateReturned));
                        break;
                    }
                }
                Debug.WriteLine("-----Issuance");
                Debug.WriteLine($"---{debugType}");
                if (issues.Count == 0)
                    Debug.WriteLine($"!!Asset ID: {i} NOT ISSUED!!");
                for (int j = 0; j < issues.Count; j++)
                {
                    string ird = Employee.UsedIRDs.ElementAt(Form_DataGenerator.GlobalRandom.Next(Employee.UsedIRDs.Count));

                    Debug.WriteLine($"--Entry {j}");

                    Debug.WriteLine($"IRD: {ird}");
                    Debug.WriteLine($"Asset ID: {i}");
                    Debug.WriteLine($"Issued: {issues[j].Issued}");
                    Debug.WriteLine($"Returned: {issues[j].Returned}");

                    if (Form_DataGenerator.OracleDB)
                    {

                        string comm = $@"INSERT INTO A_Issued (ird_number, asset_id, date_issued, date_returned) VALUES (
                        '{ird}',
                        {i},
                        DATE '{issues[j].Issued.Year}-{issues[j].Issued.Month}-{issues[j].Issued.Day}',
                        {((issues[j].Returned != null) ? $"DATE '{issues[j].Returned.Value.Year}-{issues[j].Returned.Value.Month}-{issues[j].Returned.Value.Day}'" : "null")}
                        )";

                        Debug.WriteLine(comm);
                        if (Form_DataGenerator.SQLWriter != null)
                        {
                            Form_DataGenerator.SQLWriter.Write(comm);
                            Form_DataGenerator.SQLWriter.WriteLine(";");
                        }
                        Form_DataGenerator.ExecuteOracleCommand(comm);
                    }
                    else
                    {
                        //MongoDB
                    }
                }
                Form_DataGenerator.GeneratorProgress++;
                Form_DataGenerator.UpdateProgressBar();

            }
        }

        public Issued()
        {
            Issue(1, Asset.VehicleIDStart, "Equipment");
            Issue(Asset.VehicleIDStart, Asset.FirearmIDStart, "Vehicle");
            Issue(Asset.FirearmIDStart, Asset._newestID + 1, "Firearm");
            // Randomize date issued.
            // For some entries, randomize date returned, or NULL.
            // Grab a random assetID, grab a random employee IRD.
            // Ensure the employee you grab is of a role that makes sense, e.g. firearm to officer, not dispatcher.
            // Try to ensure things like multiple vehicles are not issued to the same officer at the same time, before a previous is returned.
            // Ensure no two assets are issued more than once without a return date on all but one entry.
        }
    }
}
