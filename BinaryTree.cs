public class BinaryTree : IMazeAlgorithm
{
    public void Run(Grid g)
    {
        var random = new Random();
        var rows = g.Rows();
        while (rows.MoveNext())
        {
            var row = rows.Current;
            for (int i = 0; i < row.Length; i++)
            {
                var currentCell = row[i];
                if (currentCell.col == row.Length - 1) currentCell.Highlight = true;
            }
        }
    }
}