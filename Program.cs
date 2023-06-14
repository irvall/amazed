using System.Diagnostics;

Grid g = new Grid(int.Parse(args[0]), int.Parse(args[1]));
var random = new Random();
var binary = new BinaryTree();
var sidewinder = new Sidewinder();
sidewinder.Run(g);
var stopwatch = new Stopwatch();
stopwatch.Start();
var path = g.ShortestPath(random.Next(g.rows), random.Next(g.cols), random.Next(g.rows), random.Next(g.cols));
var ms = stopwatch.ElapsedMilliseconds;
System.Console.WriteLine($"Found path in {ms}ms");
path.ForEach(c => c.Mark = "*");
Console.WriteLine(g);