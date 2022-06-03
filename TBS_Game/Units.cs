using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static TBS_Game.MapCreator;
using static TBS_Game.GameForm;

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
            CastlesPossiblePositions.Add(new Point(1, 8));
            CastlesPossiblePositions.Add(new Point(1, 14));
            //CastlesPossiblePositions.Add(new Point(1, 22));
            CastlesPossiblePositions.Add(new Point(6, 1));
            CastlesPossiblePositions.Add(new Point(6, 8));
            CastlesPossiblePositions.Add(new Point(6, 14));
            //CastlesPossiblePositions.Add(new Point(6, 22));
            //CastlesPossiblePositions.Add(new Point(13, 1));
            //CastlesPossiblePositions.Add(new Point(13, 8));
            //CastlesPossiblePositions.Add(new Point(13, 15));
            //CastlesPossiblePositions.Add(new Point(13, 22));
        }

        public static void GenerateCastles()
        {
            var rnd = new Random();
            GetPossiblePositions();
            UnitsPositions = new Unit[MapHeight, MapWidth];
            for (int i = 0; i < CastlesPositions.Length; i++)
            {
                var pos = CastlesPossiblePositions[rnd.Next(CastlesPossiblePositions.Count)];
                CastlesPossiblePositions.Remove(pos);
                CastlesPositions[i] = pos;
                var castle = new Castle(GameForm.Players[i],pos);
                UnitsPositions[pos.X, pos.Y] = castle;
                //GameForm.Players[i].OwnedUnits.Add(castle);
                GameForm.Players[i].Castle = castle;
            }   
        }

        public static void InitializeUnits(PictureBox picture, int i, int j)
        {
            if (UnitsPositions[i, j] != null)
            {
                switch (UnitsPositions[i, j].unitType)
                {
                    case UnitType.Castle:
                        picture.Image = Resource1.Castle;
                        break;

                    case UnitType.Swordsman:
                        if (UnitsPositions[i, j].Ovner == Players[0])
                            picture.Image = Resource1.redSwordsman;

                        else if (UnitsPositions[i, j].Ovner == Players[1])
                            picture.Image = Resource1.blueSwordsman;

                        else if (UnitsPositions[i, j].Ovner == Players[2])
                            picture.Image = Resource1.greenSwordsman;

                        else if (UnitsPositions[i, j].Ovner == Players[3])
                            picture.Image = Resource1.graySwordsman;

                        break;

                    case UnitType.Spearman:
                        if (UnitsPositions[i, j].Ovner == Players[0])
                            picture.Image = Resource1.redSpearman;

                        else if (UnitsPositions[i, j].Ovner == Players[1])
                            picture.Image = Resource1.blueSpearman;

                        else if (UnitsPositions[i, j].Ovner == Players[2])
                            picture.Image = Resource1.greenSpearman;

                        else if (UnitsPositions[i, j].Ovner == Players[3])
                            picture.Image = Resource1.graySpearman;

                        break;

                    case UnitType.Knight:
                        if (UnitsPositions[i, j].Ovner == Players[0])
                            picture.Image = Resource1.redKnight;

                        else if (UnitsPositions[i, j].Ovner == Players[1])
                            picture.Image = Resource1.blueKnight;

                        else if (UnitsPositions[i, j].Ovner == Players[2])
                            picture.Image = Resource1.greenKnight;

                        else if (UnitsPositions[i, j].Ovner == Players[3])
                            picture.Image = Resource1.grayKnight;

                        break;
                }
            }
            else picture.Image = null;
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
        public Point UnitPosition;
        internal UnitType unitType;
        protected int moveDistance;
        internal bool isMoved;
        public Player Ovner;

        public static void Move(Unit selectedUnit, Unit targetUnit, int row, int column)
        {
            if (targetUnit != null && targetUnit.unitType == UnitType.Castle)
                DefeatedPlayersIndexes.Add(Array.IndexOf(Players, targetUnit.Ovner));

            if (!IsPossibleMove(selectedUnit, row, column))
                return;

            UnitMap.UnitsPositions[selectedUnit.UnitPosition.X, selectedUnit.UnitPosition.Y] = null;
            UnitMap.UnitsPositions[row, column] = selectedUnit;
            UnitMap.InitializeUnits(GameForm.Panel.GetControlFromPosition(selectedUnit.UnitPosition.Y, selectedUnit.UnitPosition.X) as PictureBox, selectedUnit.UnitPosition.X, selectedUnit.UnitPosition.Y);
            selectedUnit.UnitPosition = new Point(row, column);
            UnitMap.InitializeUnits(GameForm.Panel.GetControlFromPosition(column, row) as PictureBox, row, column);
            SelectedUnit = null;
            selectedUnit.isMoved = true;
        }

        private static bool IsPossibleMove(Unit selectedUnit, int row, int column)
        {
            var dx = Math.Abs(row - selectedUnit.UnitPosition.X);
            var dy = Math.Abs(column - selectedUnit.UnitPosition.Y);
            int delta = dx + dy;

            if(selectedUnit.unitType == UnitType.Knight)
                return delta < 3;

            else
                return (dx<2 && dy<2);
            
        }
    }

    public class Spearman : Unit
    {
        public Spearman(Player ovner, Point position)
        {
            unitType = UnitType.Spearman;
            moveDistance = 1;
            isMoved = false;
            Ovner = ovner;
            UnitPosition = position;
        }
    }

    public class Swordsman : Unit
    {
        public Swordsman(Player ovner, Point position)
        {
            unitType = UnitType.Swordsman;
            moveDistance = 1;
            isMoved = false;
            Ovner = ovner;
            UnitPosition = position;
        }
    }

    public class Knight : Unit
    {
        public Knight(Player ovner, Point position)
        {
            unitType = UnitType.Knight;
            moveDistance = 2;
            isMoved = false;
            Ovner = ovner;
            UnitPosition = position;
        }
    }

    public class Castle : Unit
    {
        public Castle(Player ovner, Point position)
        {
            unitType = UnitType.Castle;
            moveDistance = 0;
            isMoved = true;
            Ovner = ovner;
            UnitPosition = position;
        }
    }
}
