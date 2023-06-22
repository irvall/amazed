public class DistanceGrid : Grid
{
    public DistanceGrid(int colCount, int rowCount) : base(colCount, rowCount)
    {
    }

    public Distances Distances { get; set; }

    public override string CellContent(Cell cell)
    {
        return Distances[cell] != null ? Util.SingleDigit(Distances[cell] ?? default(int)) : " ";
    }

    public Distances LongestPath()
    {
        var distances = this[0, 0].Distances();
        var (maxCell, maxDist) = distances.Max();
        var maxDistances = maxCell.Distances();
        var (newMaxCell, newMaxDist) = maxDistances.Max();
        return maxDistances.PathTo(newMaxCell);
    }
}