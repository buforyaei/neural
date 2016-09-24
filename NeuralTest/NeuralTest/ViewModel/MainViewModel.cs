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

        private string _learningRate;
        private string _momentum;
        private string _sigmoidAlpha;
        private string _numberOfRecords;
        private string _hiddenNeuronsNumber;
        private string _iterationsNumber;

        private string _errorNumber;
        private bool _isLearningDone;

        private string _year;
        private string _month;
        private string _day;
        private string _hour;
        private string _minute;
        private string _width;
        private string _height;

        private IEnumerable<TrainingModel> _dataList;

        public ICommand LoadCmd { get; set; }

        public MainViewModel()
        {
            InitializeCommands();
        }
        public string LearningRate
        {
            get { return _learningRate; }
            set { Set(ref _learningRate, value); }
        }
        public string Momentum
        {
            get { return _momentum; }
            set { Set(ref _momentum, value); }
        }
        public string SigmoidAlpha
        {
            get { return _sigmoidAlpha; }
            set { Set(ref _sigmoidAlpha, value); }
        }
        public string NumberOfRecords
        {
            get { return _numberOfRecords; }
            set { Set(ref _numberOfRecords, value); }
        }
        public string HiddenNeuronsNumber
        {
            get { return _hiddenNeuronsNumber; }
            set { Set(ref _hiddenNeuronsNumber, value); }
        }
        public string IterationsNumber
        {
            get { return _iterationsNumber; }
            set { Set(ref _iterationsNumber, value); }
        }
        public string ErrorNumber
        {
            get { return _errorNumber; }
            set { Set(ref _errorNumber, value); }
        }
        public bool IsLearningDone
        {
            get { return _isLearningDone; }
            set { Set(ref _isLearningDone, value); }
        }
        public string Year
        {
            get { return _year; }
            set { Set(ref _year, value); }
        }
        public string Month
        {
            get { return _month; }
            set { Set(ref _month, value); }
        }
        public string Day
        {
            get { return _day; }
            set { Set(ref _day, value); }
        }
        public string Hour
        {
            get { return _hour; }
            set { Set(ref _hour, value); }
        }
        public string Minute
        {
            get { return _minute; }
            set { Set(ref _minute, value); }
        }
        public string Width
        {
            get { return _width; }
            set { Set(ref _width, value); }
        }
        public string Height
        {
            get { return _height; }
            set { Set(ref _height, value); }
        }

        public IEnumerable<TrainingModel> DataList
        {
            get { return _dataList; }
            set { Set(ref _dataList, value); }
        }
        private void InitializeCommands()
        {
            LoadCmd = new RelayCommand(Load);
        }

        private  void Load()
        {
            var testModels = LoadData();
            var b = LoadCrimeCategoryList();
            DataList = testModels;

        }

       

        private IEnumerable<TrainingModel> NormalizeDatesInTrains(IEnumerable<TrainingModel> trainingModels) //and NormalizeDayOfWeek
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
                t.NormalizedDate = (DateTimeToDouble(t.Dates) - DateTimeToDouble(minDate))
                                   /(DateTimeToDouble(maxDate) - DateTimeToDouble(minDate));
            }
            return trainingModels;
        }

        private IEnumerable<TrainingModel> NormalizeXY(IEnumerable<TrainingModel> trainingModels)
        {
            var trainingModelsWithoutBugs = trainingModels.ToList();
            trainingModelsWithoutBugs.Clear();
            foreach (var t in trainingModels)
            {
                if (t.X != -120.5 && t.Y != 90)
                {
                    trainingModelsWithoutBugs.Add(t);
                }
                else
                {
                    int a = 9;
                }
            }
            double minX = -121;
            double maxX = -123;
            double minY = 38;
            double maxY = 37;
            foreach (var t in trainingModelsWithoutBugs)
            {
                if (t.X < minX)
                    minX = t.X;
                if (t.X > maxX)
                    maxX = t.X;
                if (t.Y < minY)
                    minY = t.Y;
                if (t.Y > maxY)
                    maxY = t.Y;

            }
            foreach (var t in trainingModelsWithoutBugs)
            {
                t.NormalizedX = (t.X - minX)/(maxX - minX);
                t.NormalizedY = (t.Y - minY) / (maxY - minY);

            }

            return trainingModelsWithoutBugs;
        }

        private double DateTimeToDouble(DateTime dateTime)
        {
            return Double.Parse(dateTime.Year + dateTime.Month.ToString()
                                + dateTime.Day + dateTime.Hour
                                + dateTime.Minute);
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
                var x = double.Parse(t[t.Count() - 2].Replace(".", ","));
                var y = double.Parse(t[t.Count() - 1].Replace(".", ","));
                var category = t[1];
                trainingModels.Add(new TrainingModel(date,x,y,category));
            }
            var a = NormalizeXY(trainingModels);
            return NormalizeDatesInTrains(a);
        }
    }
}