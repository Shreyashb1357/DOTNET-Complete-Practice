using System.Xml.Serialization;
// class Typeparameter
// {
//     static string Select(int index, string first, string second)
//     {
//         if (index % 2 == 0)
//             return first;
//         return second;
//     }

//     static double Select(int index, double first, double second)
//     {
//         if (index % 2 == 0)
//             return first;
//         return second;
//     }


//     static double Select(double first, double second)
//     {
//         if (first.CompareTo(second) > 0)
//             return first;
//         return second;
//     }

//     static string Select(string first, string second)
//     {
//         if (first.CompareTo(second) >0)
//             return first;
//         return second;
//     }





//     static void Main(string[] args)
//     {
//         if ((args.Length > 0) && int.TryParse(args[0], out int s))
//         {
//             string ss = Select(s, "June", "july");
//             System.Console.WriteLine("Selected string : {0}", ss);
//             double i = Select(s, 7.25, 8.50);
//             System.Console.WriteLine($"Selected double : {i}");

//         }

//         else
//         {
//             string ss = Select("June", "july");
//             System.Console.WriteLine("Selected string : {0}", ss);
//             double i = Select(7.25, 8.50);
//             System.Console.WriteLine($"Selected double : {i}");
//         }
//     }
// }




class Typeparameter
{
    static T Select<T>(int index, T first, T second)
    {
        if (index % 2 == 0)
            return first;
        return second;
    }

    static T Select<T>(T first, T second) where T: IComparable<T>
    {
        if (first.CompareTo(second) > 0)
            return first;
        return second;
    }



    static void Main(string[] args)
    {
        if ((args.Length > 0) && int.TryParse(args[0], out int s))
        {
            string ss = Select(s, "June", "july");
            System.Console.WriteLine("Selected string : {0}", ss);
            double i = Select(s, 7.25, 8.50);
            System.Console.WriteLine($"Selected double : {i}");

        }

        else
        {
            string ss = Select("June", "july");
            System.Console.WriteLine("Selected string : {0}", ss);
            double i = Select(7.25, 8.50);
            System.Console.WriteLine($"Selected double : {i}");
        }
    }
}