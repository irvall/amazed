using NUnit.Framework;

public class Tests
{

    [Test]
    public static void CreatePNG()
    {
        var dimensions = new[] { 1, 3, 5, 10 };
        var pairs = new List<(int, int)>();
        foreach (var y in dimensions)
        {
            foreach (var x in dimensions)
            {
                pairs.Add((x, y));
            }
        }
        var sidewinder = new Sidewinder();
        var grids = pairs.Select(pair => new Grid(pair.Item1, pair.Item2)).ToList();
        grids.ForEach(sidewinder.Run);
        grids.ForEach(grid => ImageHelper.CreatePNG(grid, $"output/maze_{grid.cols}x{grid.rows}"));

    }

}