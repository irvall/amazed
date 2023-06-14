public class Cell
{
    public int row;
    public int col;
    private HashSet<Cell>? links;
    public Cell? north, south, east, west;

    public string Mark { get; set; } = "";

    public Cell(int row, int col)
    {
        this.row = row;
        this.col = col;
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
        links ??= new HashSet<Cell>();
        links.Add(other);
        if (bothWays)
            other.Link(this, false);
    }

    public void UnLink(Cell other, bool bothWays)
    {
        if (links == null)
        {
            Console.WriteLine($"Warning: Call to UnLink({other},{bothWays}), but no cells to unlink!");
            return;
        }
        links.Remove(other);
        if (bothWays)
            other.UnLink(this, false);
    }

    public bool IsLinked(Cell other)
    {
        return links?.Contains(other) ?? false;
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


}
