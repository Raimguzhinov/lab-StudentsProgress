using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                student.EES = rnd.Next(2, 3);
                student.ComputerMath = rnd.Next(0, 2);
                student.PhysicalSport = rnd.Next(1, 3);
                student.AEVM = rnd.Next(1, 3);
                student.Networks = rnd.Next(0, 2);
                student.AverageMark = Math.Round((double)(student.MathAnalysis + student.EES + student.ComputerMath +
                                                           student.PhysicalSport + student.AEVM + student.Networks) / 6, 2);
                Students.Add(student);
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}