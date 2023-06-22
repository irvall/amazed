var g = new ColoredGrid(int.Parse(args[0]), int.Parse(args[1]));
var start = g[g.rows / 2, g.cols / 2];
Sidewinder.On(g);
ImageHelper.AnimatedDjikstra(g, start);

