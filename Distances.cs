public class Distances
{

    private Cell _root;
    private Dictionary<Cell, int> _distanceMap;

    public Distances(Cell root)
    {
        _root = root;
        _distanceMap = new Dictionary<Cell, int>();
        _distanceMap[root] = 0;
    }

    public bool Contains(Cell cell)
    {
        return _distanceMap.ContainsKey(cell);
    }

    public int? this[Cell cell]
    {
        get
        {
            if (!_distanceMap.ContainsKey(cell))
                return null;
            return _distanceMap[cell];
        }
        set
        {
            _distanceMap[cell] = (int)value;
        }
    }

    public IEnumerable<(Cell cell, int distance)> CellDistancePairs()
    {
        return _distanceMap.Select(kv => (kv.Key, kv.Value));
    }

    private Dictionary<Cell, int> CalculateDistances(Cell root)
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

    public Distances PathTo(Cell goal)
    {
        var current = goal;
        var breadcrumbs = new Distances(_root);
        breadcrumbs[current] = _distanceMap[current];
        while (current != _root)
        {
            foreach (var neighbour in current.Links)
            {
                if (_distanceMap[neighbour] < _distanceMap[current])
                {
                    breadcrumbs[neighbour] = _distanceMap[neighbour];
                    current = neighbour;
                    break;
                }
            }
        }
        return breadcrumbs;
    }

    public (Cell maxCell, int maxDistance) Max()
    {
        var maxCell = _root;
        var maxDistance = 0;

        foreach (var (cell, distance) in CellDistancePairs())
        {
            if (distance > maxDistance)
            {
                maxDistance = distance;
                maxCell = cell;
            }
        }
        return (maxCell, maxDistance);
    }

}