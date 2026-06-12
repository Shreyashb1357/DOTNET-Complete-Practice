namespace Model;

public readonly record struct Iteminfo(double Cost, int Stock)
{
    public static Iteminfo Parse(string name)
    {
        string[] segment = name.Split('&');
        double c = double.Parse(segment[0][5..]);
        int s = int.Parse(segment[1][6..]);
        return new Iteminfo(c, s);
    }
}