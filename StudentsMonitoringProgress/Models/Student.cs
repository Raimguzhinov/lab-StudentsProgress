using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentsMonitoringProgress.Models
{
    public class Student : INotifyPropertyChanged
    {
        private string _name = null!;
        private int _visualProg;
        private int _mathAnalysis;
        private int _electrotechnic;
        private int _computerMath;
        private int _physicalSport;
        private int _architecture;
        private int _networks;
        private double _averageMark;

        public string Name 
        { 
            get => _name; 
            set => SetField(ref _name, value); 
        }

        public int VisualProg 
        { 
            get => _visualProg; 
            set => SetField(ref _visualProg, value); 
        }

        public int MathAnalysis 
        { 
            get => _mathAnalysis; 
            set => SetField(ref _mathAnalysis, value); 
        }

        public int Electrotechnic 
        { 
            get => _electrotechnic; 
            set => SetField(ref _electrotechnic, value); 
        }

        public int ComputerMath 
        { 
            get => _computerMath; 
            set => SetField(ref _computerMath, value); 
        }

        public int PhysicalSport 
        { 
            get => _physicalSport; 
            set => SetField(ref _physicalSport, value); 
        }

        public int Architecture 
        { 
            get => _architecture; 
            set => SetField(ref _architecture, value); 
        }

        public int Networks 
        { 
            get => _networks; 
            set => SetField(ref _networks, value); 
        }

        public double AverageMark 
        { 
            get => _averageMark; 
            set => SetField(ref _averageMark, value); 
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
