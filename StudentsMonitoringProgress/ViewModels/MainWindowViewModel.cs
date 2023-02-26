using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using StudentsMonitoringProgress.Models;

namespace StudentsMonitoringProgress.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Student> _students = null!;
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged("Students");
            }
        }

        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged("Index");
            }
        }

        public MainWindowViewModel()
        {
            Students = new ObservableCollection<Student>();

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
            ScVisualProg = averageVisualProg.ToString(CultureInfo.CurrentCulture);
            ScMathAnalysis = averageMathAnalysis.ToString(CultureInfo.CurrentCulture);
            ScElectrotechnic = averageElectrotechnic.ToString(CultureInfo.CurrentCulture);
            ScComputerMath = averageComputerMath.ToString(CultureInfo.CurrentCulture);
            ScPhysicalSport = averagePhysicalSport.ToString(CultureInfo.CurrentCulture);
            ScArchitecture = averageArchitecture.ToString(CultureInfo.CurrentCulture);
            ScNetworks = averageNetworks.ToString(CultureInfo.CurrentCulture);
            ScAverageMark = Math.Round((averageVisualProg + averageMathAnalysis + averageElectrotechnic + averageComputerMath + averagePhysicalSport + averageArchitecture + averageNetworks) / 7, 2).ToString(CultureInfo.CurrentCulture);

        }
        public string ScAverageMark { get; set; }
        public string ScNetworks { get; set; }
        public string ScArchitecture { get; set; }
        public string ScPhysicalSport { get; set; }
        public string ScComputerMath { get; set; }
        public string ScElectrotechnic { get; set; }
        public string ScMathAnalysis { get; set; }
        public string ScVisualProg { get; set; }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}