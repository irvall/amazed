using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;

public class ImageHelper
{

    public static Image<Rgba32> CreatePNG(Grid g, string name = "maze")
    {
        var cellSize = 100;

        var width = g.cols * cellSize;
        var height = g.rows * cellSize;
        cellSize -= 20;
        var halfCell = cellSize / 2;

        var linePen = new Pen(new SolidBrush(Color.Black), 1);

        using (Image<Rgba32> image = new Image<Rgba32>(width, height))
        {
            // Draw a line from point (50, 50) to (350, 250)
            image.Mutate(x => x.Fill(Color.White));

            var lines = new List<PointF[]>();
            var ids = new List<(int, PointF)>();
            for (int y = 0; y < g.rows; y++)
            {
                var py = y * cellSize + 100;
                for (int x = 0; x < g.cols; x++)
                {
                    var cell = g[y, x]!;
                    var id = y * g.rows + x;
                    var px = cellSize * x + 100;
                    ids.Add((id, new PointF(px - 5, py)));
                    var lil = 2;
                    var ul = new PointF(px - halfCell + lil, py - halfCell + lil);
                    var ur = new PointF(px + halfCell + lil, py - halfCell + lil);
                    var lr = new PointF(px + halfCell + lil, py + halfCell + lil);
                    var ll = new PointF(px - halfCell + lil, py + halfCell + lil);
                    if (!cell.IsLinked(cell.north))
                        lines.Add(new[] { ul, ur });
                    if (!cell.IsLinked(cell.south))
                        lines.Add(new[] { ll, lr });
                    if (!cell.IsLinked(cell.east))
                        lines.Add(new[] { ur, lr });
                    if (!cell.IsLinked(cell.west))
                        lines.Add(new[] { ul, ll });
                }
            }
            var family = SystemFonts.Collection.Families.FirstOrDefault(fam => fam.Name == "Microsoft Sans Serif");
            var font = new Font(family, 6);
            image.Mutate(ctx =>
            {
                lines.ForEach(line =>
                    ctx.DrawLines(linePen, line));
                ids.ForEach(id =>
                {
                    ctx.DrawText(id.Item1.ToString(), font, Color.Black, id.Item2);
                });
            });

            // Save the image as a PNG file
            string outputPath = $"{name}.png";
            image.Save(outputPath);
            Console.WriteLine("Image saved to: " + outputPath);
            return image;
        }
    }
}

internal record struct NewStruct(PointF From, PointF To)
{
    public static implicit operator (PointF From, PointF To)(NewStruct value)
    {
        return (value.From, value.To);
    }

    public static implicit operator NewStruct((PointF From, PointF To) value)
    {
        return new NewStruct(value.From, value.To);
    }
}