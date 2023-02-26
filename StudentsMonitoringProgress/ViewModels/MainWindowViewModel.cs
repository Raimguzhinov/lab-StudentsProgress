using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

using System.Reactive;
using ReactiveUI;
using StudentsMonitoringProgress.Models;

namespace StudentsMonitoringProgress.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Student> _students = null!;
        private int _index;
        private string _newStudentName = null!;
        private int _selectedVisualProgGrade;
        private int _selectedMathAnalysisGrade;
        private int _selectedElectrotechnicGrade;
        private int _selectedComputerMathGrade;
        private int _selectedPhysicalSportGrade;
        private int _selectedArchitectureGrade;
        private int _selectedNetworksGrade;
        private List<int> _gradeChoices = new List<int> { 0, 1, 2 };
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged("Students");
            }
        }
        public MainWindowViewModel()
        {
            Students = new ObservableCollection<Student>();
            AddStudentCommand = ReactiveCommand.Create(AddStudent);
            DeleteStudentCommand = ReactiveCommand.Create(DeleteStudent);
            SaveCommand = ReactiveCommand.Create(ExecuteSaveCommand);
            UploadCommand = ReactiveCommand.Create(ExecuteUploadCommand);
            // заполнение случайными данными
            Random rnd = new Random();
            string[] names = { "Максим Назипов", "Владислав Перчун", "Константин Ефремов", "Диас Раймгужинов", "Даниил Шлипев", "Сергей Малеев" };
            List<string> availableNames = new List<string>(names);
            
            for (int i = 0; i < names.Length; i++)
            {
                int index = rnd.Next(availableNames.Count); // выбираем случайный индекс из оставшихся имен
                string name = availableNames[index]; // выбираем случайное имя из оставшихся имен
                availableNames.Remove(name); // удаляем имя из списка доступных имен
                Student student = new Student();
                student.Name = name;
                student.VisualProg = rnd.Next(0, 2);
                student.MathAnalysis = rnd.Next(1, 3);
                student.Electrotechnic = rnd.Next(2, 3);
                student.ComputerMath = rnd.Next(0, 2);
                student.PhysicalSport = rnd.Next(1, 3);
                student.Architecture = rnd.Next(1, 3);
                student.Networks = rnd.Next(0, 2);
                student.AverageMark = Math.Round((double)(student.MathAnalysis + student.Electrotechnic + student.ComputerMath +
                                                           student.PhysicalSport + student.Architecture + student.Networks) / 6, 2);
                Students.Add(student);
            }
            // Вычисляем средний балл для каждого предмета
            double sumVisualProg = 0, sumMathAnalysis = 0, sumElectrotechnic = 0, sumComputerMath = 0, sumPhysicalSport = 0, sumArchitecture = 0, sumNetworks = 0;
            foreach (var student in Students)
            {
                sumVisualProg += student.VisualProg;
                sumMathAnalysis += student.MathAnalysis;
                sumElectrotechnic += student.Electrotechnic;
                sumComputerMath += student.ComputerMath;
                sumPhysicalSport += student.PhysicalSport;
                sumArchitecture += student.Architecture;
                sumNetworks += student.Networks;
            }
            // Вычисляем общий средний балл по всем студентам
            int count = Students.Count;
            double averageVisualProg = Math.Round(sumVisualProg / count, 2);
            double averageMathAnalysis = Math.Round(sumMathAnalysis / count, 2);
            double averageElectrotechnic = Math.Round(sumElectrotechnic / count, 2);
            double averageComputerMath = Math.Round(sumComputerMath / count, 2);
            double averagePhysicalSport = Math.Round(sumPhysicalSport / count, 2);
            double averageArchitecture = Math.Round(sumArchitecture / count, 2);
            double averageNetworks = Math.Round(sumNetworks / count, 2);
            // Устанавливаем значения свойств для отображения в интерфейсе
            ScVisualProg = averageVisualProg.ToString(CultureInfo.InvariantCulture);
            ScMathAnalysis = averageMathAnalysis.ToString(CultureInfo.InvariantCulture);
            ScElectrotechnic = averageElectrotechnic.ToString(CultureInfo.InvariantCulture);
            ScComputerMath = averageComputerMath.ToString(CultureInfo.InvariantCulture);
            ScPhysicalSport = averagePhysicalSport.ToString(CultureInfo.InvariantCulture);
            ScArchitecture = averageArchitecture.ToString(CultureInfo.InvariantCulture);
            ScNetworks = averageNetworks.ToString(CultureInfo.InvariantCulture);
            ScAverageMark = Math.Round((averageVisualProg + averageMathAnalysis + averageElectrotechnic + averageComputerMath + averagePhysicalSport + averageArchitecture + averageNetworks) / 7, 2).ToString(CultureInfo.InvariantCulture);
        }
        public ReactiveCommand<Unit, Unit> AddStudentCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteStudentCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> UploadCommand { get; }

        private void AddStudent()
        {
            Student newStudent = new Student()
            {
                Name = NewStudentName,
                VisualProg = SelectedVisualProgGrade,
                MathAnalysis = SelectedMathAnalysisGrade,
                Electrotechnic = SelectedElectrotechnicGrade,
                ComputerMath = SelectedComputerMathGrade,
                PhysicalSport = SelectedPhysicalSportGrade,
                Architecture = SelectedArchitectureGrade,
                Networks = SelectedNetworksGrade
            };
            Students.Add(newStudent);
            UpdateScAverageMarks();
            OnPropertyChanged(nameof(Students));
        }
        private void DeleteStudent()
        {
            Student selectedStudent = Students[Index];
            Students.Remove(selectedStudent);
            UpdateScAverageMarks();
            OnPropertyChanged(nameof(Students));
        }
        private void UpdateScAverageMarks()
        {
            if (Students.Count == 0)
            {
                ScAverageMark = ScVisualProg = ScMathAnalysis = ScElectrotechnic = ScComputerMath = ScPhysicalSport = ScArchitecture = ScNetworks = 0.ToString();
            }
            else
            {
                double total = 0, totalVisualProg = 0, totalMathAnalysis = 0, totalElectrotechnic = 0, totalComputerMath = 0, totalPhysicalSport = 0, totalArchitecture = 0, totalNetworks = 0;
                foreach (Student student in Students)
                {
                    total += student.AverageMark;
                    totalVisualProg += student.VisualProg;
                    totalMathAnalysis += student.MathAnalysis;
                    totalElectrotechnic += student.Electrotechnic;
                    totalComputerMath += student.ComputerMath;
                    totalPhysicalSport += student.PhysicalSport;
                    totalArchitecture += student.Architecture;
                    totalNetworks += student.Networks;
                }
                ScAverageMark = (total / Students.Count).ToString(CultureInfo.InvariantCulture);
                ScVisualProg = (totalVisualProg / Students.Count).ToString(CultureInfo.InvariantCulture);
                ScMathAnalysis = (totalMathAnalysis / Students.Count).ToString(CultureInfo.InvariantCulture);
                ScElectrotechnic = (totalElectrotechnic / Students.Count).ToString(CultureInfo.InvariantCulture);
                ScComputerMath = (totalComputerMath / Students.Count).ToString(CultureInfo.InvariantCulture);
                ScPhysicalSport = (totalPhysicalSport / Students.Count).ToString(CultureInfo.InvariantCulture);
                ScArchitecture = (totalArchitecture / Students.Count).ToString(CultureInfo.InvariantCulture);
                ScNetworks = (totalNetworks / Students.Count).ToString(CultureInfo.InvariantCulture);
            }
            OnPropertyChanged(nameof(ScAverageMark));
            OnPropertyChanged(nameof(ScVisualProg));
            OnPropertyChanged(nameof(ScMathAnalysis));
            OnPropertyChanged(nameof(ScElectrotechnic));
            OnPropertyChanged(nameof(ScComputerMath));
            OnPropertyChanged(nameof(ScPhysicalSport));
            OnPropertyChanged(nameof(ScArchitecture));
            OnPropertyChanged(nameof(ScNetworks));
        }
        public string ScAverageMark { get; set; }
        public string ScNetworks { get; set; }
        public string ScArchitecture { get; set; }
        public string ScPhysicalSport { get; set; }
        public string ScComputerMath { get; set; }
        public string ScElectrotechnic { get; set; }
        public string ScMathAnalysis { get; set; }
        public string ScVisualProg { get; set; }
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged("Index");
            }
        }
        public string NewStudentName
        {
            get { return _newStudentName; }
            set { this.RaiseAndSetIfChanged(ref _newStudentName, value); }
        }
        public int SelectedVisualProgGrade
        {
            get { return _selectedVisualProgGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedVisualProgGrade, value); }
        }
        public int SelectedMathAnalysisGrade
        {
            get { return _selectedMathAnalysisGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedMathAnalysisGrade, value); }
        }
        public int SelectedElectrotechnicGrade
        {
            get { return _selectedElectrotechnicGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedElectrotechnicGrade, value); }
        }
        public int SelectedComputerMathGrade
        {
            get { return _selectedComputerMathGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedComputerMathGrade, value); }
        }
        public int SelectedPhysicalSportGrade
        {
            get { return _selectedPhysicalSportGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedPhysicalSportGrade, value); }
        }
        public int SelectedArchitectureGrade
        {
            get { return _selectedArchitectureGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedArchitectureGrade, value); }
        }
        public int SelectedNetworksGrade
        {
            get { return _selectedNetworksGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedNetworksGrade, value); }
        }
        public List<int> GradeChoices
        {
            get => _gradeChoices;
            set => this.RaiseAndSetIfChanged(ref _gradeChoices, value);
        }
        private void ExecuteSaveCommand()
        {
            Serialization.SaveDataToXmlFile(Students, "../../../StudentsData.xml");
        }
        private void ExecuteUploadCommand()
        {
            Students = Serialization.LoadDataFromXmlFile("../../../StudentsData.xml");
            UpdateScAverageMarks();
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

}