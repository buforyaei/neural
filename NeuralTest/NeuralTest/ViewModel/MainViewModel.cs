using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NeuralTest.Model;

namespace NeuralTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand LoadCmd { get; set; }
        public MainViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            LoadCmd = new RelayCommand(Load);
        }

        private  void Load()
        {
            var testModels = LoadData();
            var b = LoadCrimeCategoryList();

        }

        private IEnumerable<TrainingModel> NormalizeDatesInTrains(IEnumerable<TrainingModel> trainingModels)
        {
            var maxDate = DateTime.MinValue;
            var minDate = DateTime.MaxValue;
            foreach (var t in trainingModels)
            {
                if (t.Dates < minDate)
                    minDate = t.Dates;
                if (t.Dates > maxDate)
                    maxDate = t.Dates;
            }
            foreach (var t in trainingModels)
            {
                t.NormalizedDate = ((DateTimeToDouble(t.Dates) - DateTimeToDouble(minDate))
                                    /(DateTimeToDouble(maxDate) - (DateTimeToDouble(minDate))));
                                    
            }
            return trainingModels;
        }

        private double DateTimeToDouble(DateTime dateTime)
        {
            return Double.Parse(dateTime.Year.ToString() + dateTime.Month.ToString()
                                + dateTime.Day.ToString() + dateTime.Hour.ToString()
                                + dateTime.Minute.ToString());
        }
        private IEnumerable<string>LoadCrimeCategoryList()
        {
            var crimeList = new List<string>();
            var yourData = File.ReadAllLines(Consts.Consts.TrainingPath)
               .Skip(1)
               .Select(x => x.Split(','));
            foreach (var t in yourData)
            {
                if (!crimeList.Contains(t[1]))
                {
                    crimeList.Add(t[1]);
                }
            }
            return crimeList;
        }

     
        private IEnumerable<TrainingModel> LoadData()
        {
            var yourData = File.ReadAllLines(Consts.Consts.TrainingPath)
                .Skip(1)
                .Select(x => x.Split(','));

            return DeserializeTrains(yourData);

        }

        private IEnumerable<TrainingModel> DeserializeTrains(IEnumerable<string[]> tab)
        {
            var trainingModels = new ObservableCollection<TrainingModel>();
            foreach (var t in tab)
            {
                var date = DateTime.Parse(t[0]);
                var x = (double.Parse(t[t.Count() - 2].Replace(".", ",")));
                var y = (double.Parse(t[t.Count() - 1].Replace(".", ",")));
                trainingModels.Add(new TrainingModel(date,x,y));
            }
            
            return NormalizeDatesInTrains(trainingModels);
        }
    }
}