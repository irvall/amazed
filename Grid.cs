
using System.Numerics;

public class Grid
{
    private Cell[][] cells;
    public int rows;
    public int cols;

    public Grid(int rowCount, int colCount)
    {
        this.rows = rowCount;
        this.cols = colCount;
        cells = new Cell[rows][];
        for (int i = 0; i < cols; i++)
        {
            cells[i] = new Cell[cols];
            for (int j = 0; j < rows; j++)
            {
                cells[i][j] = new Cell(j, i);
            }
        }
        CreateNeighbourhood();
    }

    public Cell? this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols)
                return null;

            return cells[row][col];
        }
        set
        {
            if (row >= 0 && row < rows && col >= 0 && col < cols)
                cells[row][col] = value!;
        }
    }

    public override string ToString()
    {
        return Util.GridString(this);
    }

    public Cell RandomCell()
    {
        var random = new Random();
        int row = random.Next() % rows;
        int col = random.Next() % cols;
        return this[row, col]!;
    }

    public int Size => rows * cols;

    public IEnumerator<Cell[]> Rows()
    {
        foreach (Cell[] row in cells)
        {
            yield return row;
        }
    }

    public IEnumerator<Cell> Cells()
    {
        foreach (Cell[] row in cells)
        {
            foreach (Cell cell in row)
            {
                yield return cell;
            }
        }
    }

    public void HighlightRow(int row)
    {
        for (int col = 0; col < cols; col++)
        {
            this[row, col]!.Mark = "*";
        }
    }

    public void HighlightCol(int col)
    {
        for (int row = 0; row < rows; row++)
        {
            this[row, col]!.Mark = "*";
        }
    }


    private void CreateNeighbourhood()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var currentCell = cells[row][col];
                currentCell.west = this[row, col - 1];
                currentCell.east = this[row, col + 1];
                currentCell.north = this[row - 1, col];
                currentCell.south = this[row + 1, col];

            }
        }
    }

    public int ManhattanDistance(Cell a, Cell b)
        => Math.Abs(a.col - b.col) + Math.Abs(a.row - b.row);


    public List<Cell> ShortestPath(int startX, int startY, int goalX, int goalY)
    {

        var S = new Stack<(Cell, List<Cell>)>();
        var discovered = new HashSet<Cell>();
        var current = this[startX, startY]!;
        var goal = this[goalX, goalY]!;
        current.Mark = "A";
        goal.Mark = "B";
        S.Push((current, new List<Cell>()));
        while (S.Any())
        {
            var (v, path) = S.Pop();
            if (v == goal) return path;
            var neighbours = v.Neighbours().Where(n => v.IsLinked(n));
            foreach (var n in neighbours)
            {
                if (!discovered.Contains(n))
                {
                    if (n == goal) return path;
                    discovered.Add(n);
                    var updatedPath = path.ToList();
                    updatedPath.Add(n);
                    S.Push((n, updatedPath));
                }
            }
        }

        return new List<Cell>();
    }

}