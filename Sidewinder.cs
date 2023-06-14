using System.Diagnostics;

public class Sidewinder : IMazeAlgorithm
{
    public void Run(Grid g)
    {
        var random = new Random();
        var rows = g.Rows();
        while (rows.MoveNext())
        {
            var row = rows.Current;
            var currentRun = new List<Cell>();
            for (int i = 0; i < row.Length; i++)
            {
                System.Console.WriteLine(Util.GridString(g));
                var currentCell = row[i];
                currentRun.Add(currentCell);
                if (currentCell.col == 0)
                {
                    currentCell.Link(currentCell.east, true);
                    continue;
                }
                else Debug.Assert(currentCell.north != null);
                if (i == row.Length - 1)
                {
                    currentCell.Link(currentCell.north, true);
                    continue;
                }
                else Debug.Assert(currentCell.east != null);
                var heads = random.NextDouble() >= 0.5;
                Cell toVisit;
                if (heads)
                {
                    var randomIndex = random.Next(currentRun.Count);
                    System.Console.WriteLine($"Generated index {randomIndex} - currently at {i}");
                    System.Console.WriteLine("Run size " + currentRun.Count);
                    var randomCell = currentRun[randomIndex];
                    currentCell = randomCell;
                    toVisit = currentCell.north;
                }
                else toVisit = currentCell.east;

                currentCell.Link(toVisit, true);
            }
        }
    }
}