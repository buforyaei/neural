using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralTest.Model
{
    public class TrainingModel
    {
        public DateTime Dates { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string Category { get; set; }

        public double NormalizedY { get; set; }
        public double NormalizedX { get; set; }
        public double NormalizedDayOfWeek { get; set; }
        public double NormalizedHour { get; set; }
        public double NormalizedDayOfMonth { get; set; }
        public double NormalizedMonth { get; set; }
        public double NormalizedDate { get; set; }

        public TrainingModel() { }
        public TrainingModel(DateTime dates, double x, double y,string category)
        {
            Dates = dates;
            DayOfWeek = dates.DayOfWeek;
            NormalizedDayOfWeek = (double)((int)DayOfWeek)/7;
            NormalizedMonth = (double)((int) Dates.Month)/12;
            NormalizedDayOfMonth = (double) ((int) Dates.Day)/31;
            NormalizedHour = (double) ((int) Dates.Hour)/24;
            Category = category;
            X = x;
            Y = y;
        }
    }

    public struct Crimes
    {
        public const double Sucide = 0.9;
        //...

    }
}
