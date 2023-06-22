public class ColoredGrid : Grid
{

    private int _maximum;
    private Distances _distances;

    public ColoredGrid(int colCount, int rowCount) : base(colCount, rowCount)
    {
    }

    public Distances Distances
    {
        get => _distances;
        set
        {
            var (maxCell, maxDistance) = value.Max();
            _distances = value;
            _maximum = maxDistance;
        }
    }


    // public ColoredGrid(int colCount, int rowCount) : base(colCount, rowCount)
    // {
    // }


    public override Color? BackgroundColorFor(Cell cell)
    {
        if (Distances[cell] == null) return null;
        var distance = Distances[cell]!;
        var intensity = (float)(_maximum - distance) / _maximum;
        var dark = (byte)(255 * intensity);
        var bright = (byte)(128 + (127 * intensity));
        return Color.FromRgb(dark, bright, dark);
    }


}