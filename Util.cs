using System.Diagnostics;
using System.Text;

public class Util
{

    public static string GridString(Grid g)
    {
        string corner = "+";
        string wideBar = $"---{corner}";
        string body = "   ";
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Grid size {g.rows}x{g.cols}:");

        // Top boundary
        sb.AppendLine($"{corner}{string.Concat(Enumerable.Repeat(wideBar, g.cols))}");
        IEnumerator<Cell[]> rowsEnumerator = g.Rows();
        while (rowsEnumerator.MoveNext())
        {
            Cell[] row = rowsEnumerator.Current;

            // Cell contents
            StringBuilder top = new StringBuilder("|");
            StringBuilder bottom = new StringBuilder(corner);

            foreach (Cell cell in row)
            {
                body = $" {g.CellContent(cell)} ";
                string eastBoundary = cell.east != null && cell.IsLinked(cell.east) ? " " : "|";
                top.Append(body + eastBoundary);
                body = "   ";
                string southBoundary = cell.south != null && cell.IsLinked(cell.south) ? body : "---";
                bottom.Append(southBoundary + corner);
            }

            sb.AppendLine(top.ToString());
            sb.AppendLine(bottom.ToString());
        }

        return sb.ToString();
    }

    public static string SingleDigit(int digit)
    {
        if (digit >= 0 && digit <= 9) return digit.ToString();
        digit -= 10;
        char newChar = (char)('a' + digit);
        if (newChar > 'z')
        {
            digit -= 26;
            newChar = (char)('A' + digit);
        }
        return newChar.ToString();
    }

    public static T MeasureTime<T>(Func<T> action)
    {
        var stopwatch = Stopwatch.StartNew();
        var value = action.Invoke();
        stopwatch.Stop();
        Console.WriteLine($"Executed in {stopwatch.ElapsedMilliseconds}ms");
        return value;
    }

}