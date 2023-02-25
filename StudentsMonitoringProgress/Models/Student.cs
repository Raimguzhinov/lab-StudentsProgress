namespace StudentsMonitoringProgress.Models
{
    public class Student
    {
        public string Name { get; set; } = null!;
        public int VisualProg { get; set; }
        public int MathAnalysis { get; set; }
        public int EES { get; set; }
        public int ComputerMath { get; set; }
        public int PhysicalSport { get; set; }
        public int AEVM { get; set; }
        public int Networks { get; set; }
        public double AverageMark { get; set; }
    }
}