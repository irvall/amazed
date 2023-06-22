public class Cell
{
    public int row;
    public int col;
    public HashSet<Cell> Links { get; set; }
    public Cell? north, south, east, west;

    public Cell(int row, int col)
    {
        this.row = row;
        this.col = col;
        Links = new HashSet<Cell>();
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + row.GetHashCode();
        hash = hash * 23 + col.GetHashCode();
        return hash;
    }

    public void Link(Cell other, bool bothWays)
    {
        if (other == null) return;
        Links.Add(other);
        if (bothWays)
            other.Link(this, false);
    }

    public void UnLink(Cell other, bool bothWays)
    {
        Links.Remove(other);
        if (bothWays)
            other.UnLink(this, false);
    }

    public bool IsLinked(Cell? other)
    {
        return other != null && Links.Contains(other);
    }

    public List<Cell> Neighbours() => new List<Cell>
        {
            north,
            south,
            east,
            west
        }
        .Where(cell => cell != null)
        .ToList();

    public override bool Equals(object? other) =>
        other is Cell && this.row == ((Cell)other).row && this.col == ((Cell)other).col;


    public Distances Distances() {
        var distances = new Distances(this);
        var frontier = new List<Cell> { this };
        var newFrontier = new List<Cell> { };
        while (frontier.Count > 0)
        {
            newFrontier.Clear();
            frontier.ForEach(cell =>
            {
                foreach (var n in cell.Links)
                {
                    if (!distances.Contains(n))
                    {
                        distances[n] = distances[cell] + 1;
                        newFrontier.Add(n);
                    }
                }
            });
            frontier = newFrontier.ToList();
        }
        return distances;

    }



}
