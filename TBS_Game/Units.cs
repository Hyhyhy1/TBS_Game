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
        public static Unit[,] UnitsPositions;
        private static List<Point> CastlesPossiblePositions = new List<Point>();
        public static Point[] CastlesPositions = new Point[4];

        private static void GetPossiblePositions()
        {
            CastlesPossiblePositions.Add(new Point(1, 1));
            CastlesPossiblePositions.Add(new Point(28, 1));
            CastlesPossiblePositions.Add(new Point(1, 28));
            CastlesPossiblePositions.Add(new Point(28, 28));
            CastlesPossiblePositions.Add(new Point(16,1));
            CastlesPossiblePositions.Add(new Point(15, 28));
            CastlesPossiblePositions.Add(new Point(28, 16));
            CastlesPossiblePositions.Add(new Point(1, 15));
            CastlesPossiblePositions.Add(new Point(15, 15));
            CastlesPossiblePositions.Add(new Point(28, 28));
        }

        public static void GenerateCastles()
        {
            var rnd = new Random();
            GetPossiblePositions();
            UnitsPositions = new Unit[MapSize, MapSize];
            for (int i = 0; i < CastlesPositions.Length; i++)
            {
                var pos = CastlesPossiblePositions[rnd.Next(CastlesPossiblePositions.Count)];
                CastlesPossiblePositions.Remove(pos);
                CastlesPositions[i] = pos;
                var castle = new Castle(GameForm.Players[i]);
                UnitsPositions[pos.X, pos.Y] = castle;
                GameForm.Players[i].OwnedUnits.Add(castle);
                GameForm.Players[i].Castle = castle;
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
        protected bool isAttacked;
        public Player Ovner;
    }

    public class Spearman : Unit
    {
        public Spearman(Player ovner)
        {
            unitType = UnitType.Spearman;
            moveDistance = 1;
            attackDistance = 1;
            isMoved = false;
            isAttacked = false;
            Ovner = ovner;
        }
    }

    public class Swordsman : Unit
    {
        public Swordsman(Player ovner)
        {
            unitType = UnitType.Swordsman;
            moveDistance = 1;
            attackDistance = 1;
            isMoved = false;
            isAttacked = false;
            Ovner = ovner;
        }
    }

    public class Knight : Unit
    {
        public Knight(Player ovner)
        {
            unitType = UnitType.Knight;
            moveDistance = 2;
            attackDistance = 1;
            isMoved = false;
            isAttacked = false;
            Ovner = ovner;
        }
    }

    public class Castle : Unit
    {
        public Castle(Player ovner)
        {
            unitType = UnitType.Castle;
            moveDistance = 0;
            attackDistance = 0;
            isMoved = true;
            isAttacked = true;
            Ovner = ovner;
        }
    }
}
