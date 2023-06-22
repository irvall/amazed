using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Gif;

public class ImageHelper
{


    public static Image<Rgba32> CreatePNG(Grid g, int cellSize = 10, string name = "maze")
    {

        var width = g.cols * cellSize;
        var height = g.rows * cellSize;

        using (Image<Rgba32> image = new Image<Rgba32>(width + 1, height + 1))
        {
            // Draw a line from point (50, 50) to (350, 250)
            image.Mutate(x => x.Fill(Color.White));

            var lines = new List<PointF[]>();
            var rects = new Dictionary<RectangleF, Color>();
            g.Cells().ForEach(cell =>
            {
                var x1 = cell.col * cellSize;
                var y1 = cell.row * cellSize;
                var x2 = (cell.col + 1) * cellSize;
                var y2 = (cell.row + 1) * cellSize;
                var ul = new PointF(x1, y1);
                var ur = new PointF(x2, y1);
                var lr = new PointF(x2, y2);
                var ll = new PointF(x1, y2);
                if (cell.north == null)
                    lines.Add(new[] { ul, ur });
                if (cell.west == null)
                    lines.Add(new[] { ul, ll });
                if (!cell.IsLinked(cell.east))
                    lines.Add(new[] { ur, lr });
                if (!cell.IsLinked(cell.south))
                    lines.Add(new[] { ll, lr });

                var color = g.BackgroundColorFor(cell);
                if (color != null)
                {
                    var rect = new RectangleF(x1, y1, Math.Abs(x1 - x2), Math.Abs(y1 - y2));
                    rects[rect] = (Color)color;
                }
            });

            image.Mutate(ctx =>
            {
                rects.ToList().ForEach(pair =>
                    ctx.Fill(pair.Value, pair.Key));
                lines.ForEach(line =>
                    ctx.DrawLines(Color.Black, 1, line));
            });

            // Save the image as a PNG file
            string outputPath = $"{name}.png";
            image.Save(outputPath);
            Console.WriteLine("Image saved to: " + outputPath);
            return image;
        }
    }

    public static void AnimatedDjikstra(Grid g, Cell origin)
    {

        int width = 600;
        int height = 600;


        // Delay between frames in (1/100) of a second.
        const int frameDelay = 100;

        // For demonstration: use images with different colors.
        Color[] colors = { Color.Green, Color.Red };

        // Create empty image.
        using Image<Rgba32> gif = new(width, height, Color.Blue);

        // Set animation loop repeat count to 5.
        var gifMetaData = gif.Metadata.GetGifMetadata();
        gifMetaData.RepeatCount = 5;

        // Set the delay until the next image is displayed.
        GifFrameMetadata metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();
        metadata.FrameDelay = frameDelay;
        for (int i = 0; i < colors.Length; i++)
        {
            // Create a color image, which will be added to the gif.
            using Image<Rgba32> image = new(width, height, colors[i]);

            // Set the delay until the next image is displayed.
            metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
            metadata.FrameDelay = frameDelay;

            // Add the color image to the gif.
            gif.Frames.AddFrame(image.Frames.RootFrame);
        }

        // Save the final result.
        gif.SaveAsGif("output.gif");

    }
}
