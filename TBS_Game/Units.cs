using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TBS_Game.MapCreator;

namespace TBS_Game
{
    public static class UnitMap
    {
        public static Unit[,] UnitsPositions = new Unit[MapSize, MapSize];
        public static List<Point> CastlesPositions;

        public static void GenerateUnitsLayout()
        {
            for (int i = 0; i < MapSize; i++)
                for(int j = 0; j < MapSize; j++)
                {
                    var neighbours = GetNeighbours(i, j, false);
                    if(!neighbours.Contains(Cell.SmallForest) && !neighbours.Contains(Cell.Forest) && !neighbours.Contains(Cell.Void) && CastlesPositions.Count <5)
                    {
                        foreach (var position in CastlesPositions)
                            if(Math.Sqrt(Math.Pow(i - position.X, 2) + Math.Pow(j - position.Y, 2)) < 5)
                                CastlesPositions.Add(new Point(i,j));
                    }

                }
        }

    }

    public enum UnitType
    {
        None,
        Swordsman,
        Spearman,
        Knight,
        Castle
    }

    public class Unit
    {
        public UnitType unitType;
        protected int moveDistance;
        protected int attackDistance;
        protected bool isMoved;
        public int Ovner;
    }

    public class Spearman : Unit
    {
        

        public Spearman(int ovner)
        {
            unitType = UnitType.Spearman;
            moveDistance = 1;
            attackDistance = 1;
            isMoved = false;
            Ovner = ovner;
        }
    }

    public class Swordsman : Unit
    {
        public Swordsman()
        {
            unitType = UnitType.Swordsman;
            moveDistance = 1;
            attackDistance = 1;
            isMoved = false;
        }
    }

    public class Knight : Unit
    {
        public Knight()
        {
            unitType = UnitType.Knight;
            moveDistance = 2;
            attackDistance = 1;
            isMoved = false;
        }
    }

    public class Castle : Unit
    {
        public Castle()
        {
            unitType = UnitType.Castle;
            moveDistance = 0;
            attackDistance = 0;
            isMoved = true;
        }
    }
}
