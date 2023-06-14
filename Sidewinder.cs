using System.Diagnostics;

public class Sidewinder : IMazeAlgorithm
{
    public void Run(Grid g)
    {
        var random = new Random();
        var rows = g.Rows();
        var run = new List<Cell>();
        for (int row = g.rows - 1; row >= 0; row--)
        {
            for (int col = 0; col < g.cols; col++)
            {
                var currentCell = g[row, col]!;
                run.Add(currentCell);
                if (row == 0)
                {
                    currentCell.Link(currentCell.east!, true);
                    continue;
                }
                else Debug.Assert(currentCell.north != null);
                if (col == g.cols - 1)
                {
                    currentCell.Link(currentCell.north, true);
                    continue;
                }
                else Debug.Assert(currentCell.east != null);
                var heads = random.NextDouble() >= 0.5;
                if (heads)
                {
                    currentCell.Link(currentCell.east, true);
                    continue;
                }
                var randomIndex = random.Next(run.Count);
                var randomCell = run[randomIndex];
                randomCell.Link(randomCell.north!, true);
                run.Clear();
            }
            run.Clear();
        }
    }
}