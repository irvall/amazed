public class PathFinding
{

    private Grid grid;


    public PathFinding(Grid g)
    {
        grid = g;
    }

    public List<Cell> DFS(int startY, int startX, int goalY, int goalX)
    {
        System.Console.WriteLine($"Start {startX} {startY}");
        System.Console.WriteLine($"Goal {goalX} {goalY}");
        var S = new Stack<(Cell, List<Cell>)>();
        var discovered = new HashSet<Cell>();
        var current = grid[startX, startY]!;
        var goal = grid[goalX, goalY]!;
        var shortestPath = new List<Cell> { current };
        var finished = false;
        S.Push((current, shortestPath));
        while (S.Any())
        {
            var (v, path) = S.Pop();
            if (finished || v == goal)
            {
                shortestPath = path;
                break;
            }
            foreach (var n in v.Links)
            {
                if (!discovered.Contains(n))
                {
                    discovered.Add(n);
                    var updatedPath = path.ToList();
                    updatedPath.Add(n);
                    if (n == goal)
                    {
                        finished = true;
                        shortestPath = updatedPath;
                    }
                    S.Push((n, updatedPath));
                }
                if (finished) break;
            }
        }

        shortestPath.ForEach(cell => cell.Body = "*");
        shortestPath.FirstOrDefault().Body = "A";
        shortestPath.LastOrDefault().Body = "B";
        return shortestPath;
    }

    public Dictionary<Cell, int> Distances(Cell root)
    {
        var distances = new Dictionary<Cell, int>();
        distances[root] = 0;
        var frontier = new List<Cell> { root };
        var newFrontier = new List<Cell> { };
        while (frontier.Count > 0)
        {
            newFrontier.Clear();
            frontier.ForEach(cell =>
            {
                foreach (var n in cell.Links)
                {
                    if (!distances.ContainsKey(n))
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