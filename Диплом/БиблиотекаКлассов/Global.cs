using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public static class Global
    {
     
    }

public abstract class Entry
{
    public string name { get; set; }
} 

public static class Collection
{
    public static List<T> ToList<T>(ObservableCollection<T> _input)
    {
        List<T> output = new List<T>();

        foreach(T c in _input)
        {
            output.Add(c);
        }
        return output;
    }

    public static ObservableCollection<T> ToCollection<T>(List<T> _input)
    {
        ObservableCollection<T> output = new ObservableCollection<T>();

        foreach (T c in _input)
        {
            output.Add(c);
        }
        return output;
    }


}


