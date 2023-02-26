using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
namespace StudentsMonitoringProgress.Models;

public class Serialization
{
    public static void SaveDataToXmlFile(ObservableCollection<Student> students, string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Student>));
        using (FileStream stream = new FileStream(fileName, FileMode.Create))
        {
            serializer.Serialize(stream, students);
        }
    }
    public static ObservableCollection<Student> LoadDataFromXmlFile(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Student>));
        using (FileStream stream = new FileStream(fileName, FileMode.Open))
        {
            return (serializer.Deserialize(stream) as ObservableCollection<Student>)!;
        }
    }
}