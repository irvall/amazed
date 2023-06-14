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
                body = $" {(cell.Mark.Length > 0 ? cell.Mark : " ")} ";
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

}