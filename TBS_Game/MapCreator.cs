using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBS_Game
{
    public enum Cell
    {
        Void,
        Grass,
        SmallForest,
        Forest
    };
    
    public static class MapCreator
    {
        public static int MapSize { get; private set; }
        public static Cell[,] Map { get; set; }

        /// <summary>
        /// данный метод создает квадратную карту из случайных полей
        /// </summary>
        /// <param name="size">размер поля</param>
        public static void GenerateMap(int size)
        {
            var rand = new Random();
            MapSize = size;
            Point initialPoint = new Point(rand.Next(0, size), rand.Next(0, size));

            Map = new Cell[size,size];

            var queue = new Queue<Point>();
            queue.Enqueue(initialPoint);

            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (point.X < 0 || point.X >= MapSize || point.Y < 0 || point.Y >= MapSize) continue;
                if (Map[point.X, point.Y] != Cell.Void) continue;

                var neighbours = new List<Cell>();

                for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                    {
                        if (dx != 0 && dy != 0) continue;
                        if (point.X + dx < 0 || point.X + dx > MapSize - 1 || point.Y +dy <0 || point.Y + dy > MapSize - 1) continue;

                        var neighbourPosition = new Point(point.X + dx, point.Y + dy);
                        neighbours.Add(Map[neighbourPosition.X, neighbourPosition.Y]);
                        queue.Enqueue(neighbourPosition);
                    }

                var possibleCells = new List<Cell>() { Cell.SmallForest, Cell.Grass, Cell.Forest };

                if(point.X == initialPoint.X && point.Y == initialPoint.Y)
                    possibleCells = new List<Cell> { Cell.Grass };

                if (neighbours.Contains(Cell.Grass))
                    possibleCells.Remove(Cell.Forest);
                
                if (neighbours.Contains(Cell.Forest))
                    possibleCells.Remove(Cell.Grass);

                Map[point.X, point.Y] = possibleCells[rand.Next(possibleCells.Count)];
            }           
        }
        
    }
}
