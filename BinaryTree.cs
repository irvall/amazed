using System.Diagnostics;

public class BinaryTree : IMazeAlgorithm
{
    public void Run(Grid g)
    {
        var random = new Random();
        var rows = g.Rows();
        for (int row = g.rows - 1; row >= 0; row--)
        {
            for (int col = 0; col < g.cols; col++)
            {
                var currentCell = g[row, col]!;
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
                Cell toVisit = heads ? currentCell.north : currentCell.east;
                currentCell.Link(toVisit, true);
            }
        }
    }
}