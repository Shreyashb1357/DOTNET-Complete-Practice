namespace Model;

public readonly record struct Iteminfo(double Cost, int Stock)
{
    public static Iteminfo Parse(string name)
    {
        string[] segment = name.Split("and");
        double c = double.Parse(segment[0][11..].Trim());
        int s = int.Parse(segment[1][8..].Trim());
        return new Iteminfo(c, s);
    }
}