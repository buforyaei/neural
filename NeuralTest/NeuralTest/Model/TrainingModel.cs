using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralTest.Model
{
    public class TrainingModel
    {
         public TrainingModel(DateTime dates, double x, double y)
        {
            Dates = dates;
            //Category = category;
            //Descript = descript;
            //DayOfWeek = dayOfWeek;
            //PdDistrict = pdDistrict;
            //Resolution = resolution;
            X = x;
            Y = y;
        }
         //string category, string descript, string dayOfWeek, string pdDistrict, string resolution,

         public TrainingModel()
        {
        }

        public DateTime Dates { get; set; }
        //public string Category { get; set; }
        //public string Descript { get; set; }
        //public string DayOfWeek { get; set; }
        //public string PdDistrict { get; set; }
        //public string Resolution { get; set; }
        //public string Address { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double NormalizedY { get; set; }
        public double NormalizedX { get; set; }
        public double NormalizedDate { get; set; }

    }
}
