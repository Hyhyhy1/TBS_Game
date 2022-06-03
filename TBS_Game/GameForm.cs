using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static TBS_Game.MapCreator;
using static TBS_Game.UnitMap;
using static TBS_Game.Player;

namespace TBS_Game
{
    public partial class GameForm : Form
    {
        public static Player[] Players;
        public static int CurrentPlayerIndex;
        public static bool SelectionInProggres = false;
        internal static Unit SelectedUnit = null;
        internal static List<int> DefeatedPlayersIndexes = new List<int>();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        public static TableLayoutPanel Panel;
        private const int WM_SETREDRAW = 11;
        public GameForm()
        {
            Players = InitializePlayers();
            CurrentPlayerIndex = 0;
            DoubleBuffered = true;
            GenerateMap(16,8);
            Panel = LayoutPanels.CreateFieldPanel();
            InitializeMap(Panel);
            Controls.Add(LayoutPanels.CreateMainPanel(Panel,GetNextTurnButton()));
        }

        /// <summary>
        /// немного перегруженный метод, делающий все и сразу, но проект нужно было заканчивать \(0_o\)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void CellClicked(int row, int column)
        {
            if ((row < 0) || (column < 0)) return;

            if (SelectedUnit != null)
            {              
                if (SelectedUnit.isMoved)
                {
                    SelectedUnit = null;
                    return;
                }

                var selectedUnit = SelectedUnit;
                var targetUnit = UnitsPositions[row, column];

                if (selectedUnit == targetUnit)
                {
                    SelectedUnit = null;
                    return;
                }


                if (targetUnit == null)
                {
                    Unit.Move(selectedUnit, targetUnit, row, column);
                    return;
                }                

                if (selectedUnit.Ovner == targetUnit.Ovner)
                    return;

                if (selectedUnit == targetUnit)
                    return;

                if (selectedUnit.unitType == UnitType.Spearman)
                {
                    if (targetUnit.unitType != UnitType.Knight && targetUnit.unitType != UnitType.Castle)
                        return;

                    else
                    {
                        targetUnit.Ovner.OwnedUnits.Remove(targetUnit);
                        Unit.Move(selectedUnit, targetUnit, row, column);
                        return;
                    }
                }

                if(selectedUnit.unitType == UnitType.Knight)
                {
                    if (targetUnit.unitType != UnitType.Swordsman && targetUnit.unitType != UnitType.Castle)
                        return;

                    else
                    {
                        targetUnit.Ovner.OwnedUnits.Remove(targetUnit);
                        Unit.Move(selectedUnit, targetUnit, row, column);
                        return;
                    }
                }

                if (selectedUnit.unitType == UnitType.Swordsman)
                {
                    if (targetUnit.unitType != UnitType.Spearman && targetUnit.unitType != UnitType.Castle)
                        return;

                    else
                    {
                        targetUnit.Ovner.OwnedUnits.Remove(targetUnit);
                        Unit.Move(selectedUnit, targetUnit, row, column);
                        return;
                    }
                }
            }

            if (UnitsPositions[row, column] == null) return;

            else if (UnitsPositions[row, column] == Players[CurrentPlayerIndex].Castle && !SelectionInProggres)
            {
                SelectionInProggres = true;
                Form unitSelector = new UnitSelectorForm(new Point(row,column));
                unitSelector.Size = new Size(3 * LayoutPanels.FieldSize, LayoutPanels.FieldSize);
                unitSelector.Show();
            }
            else if(UnitsPositions[row, column].Ovner == Players[CurrentPlayerIndex] && SelectedUnit == null)
            {
                SelectedUnit = UnitsPositions[row, column];
            }
        }

        private void Event_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var cellPosition = Panel.GetCellPosition((Control)sender);
                CellClicked(cellPosition.Row, cellPosition.Column);
            }
        }

        /// <summary>
        /// Этот метод переключает ход и сбрасывает значение IsMoved у юнитов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextTurnButton_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            SelectionInProggres = false;

            if (DefeatedPlayersIndexes.Count == 3)
            {
                Form finalForm = new Form();
                finalForm.Controls.Add(new Label() { Text = "Победа!", Dock = DockStyle.Fill });
                finalForm.Show();
                
            }
                

            var table = ctrl.Parent as TableLayoutPanel;
            if (CurrentPlayerIndex == Players.Count() - 1)
            {
                CurrentPlayerIndex = 0;
                foreach(var player in Players)
                {
                    foreach (var unit in player.OwnedUnits)
                        unit.isMoved = false;
                }
            }
            else CurrentPlayerIndex += 1;
            ctrl.BackColor = Players[CurrentPlayerIndex].InterfaceColor;
            table.GetControlFromPosition(0,1).BackColor = Players[CurrentPlayerIndex].InterfaceColor;
            table.GetControlFromPosition(1,0).BackColor = Players[CurrentPlayerIndex].InterfaceColor;
            SelectedUnit = null;
        }

        /// <summary>
        /// этот метод создает pictureBox - ячейку поля
        /// </summary>
        /// <param name="pic">картинка для ячейки</param>
        /// <returns>pictureBox - ячейка поля</returns>
        public PictureBox GetPicture(Bitmap pic)
        {
            var picture = new PictureBox();
            
            picture.BackgroundImage = pic;
            picture.Margin = new Padding(0);
            picture.Dock = DockStyle.Fill;
            picture.SizeMode = PictureBoxSizeMode.Zoom;

            picture.MouseDown += Event_MouseDown;
            return picture;
        }

        /// <summary>
        /// этот метод заполняет карту картинками
        /// </summary>
        /// <param name="panel"></param>
        public void InitializeMap(TableLayoutPanel panel)
        {
            var rnd = new Random();
            panel.SuspendLayout();


            for (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++)
                {
                    var picture = new PictureBox();
                    switch (Map[i, j])
                    {
                        case Cell.Grass:

                            picture = GetPicture(Resource1.Grass);
                            break;

                        case Cell.SmallForest:
                            var pictures = new[] { Resource1.SmallForest1, Resource1.SmallForest2 };
                            picture = GetPicture(pictures[rnd.Next(0, 1)]);

                            break;

                        case Cell.Forest:
                            pictures = new[] { Resource1.BigForest1, Resource1.BigForest2 };
                            picture = GetPicture(pictures[rnd.Next(0, 1)]);
                            
                            break;
                    }
                    InitializeUnits(picture, i, j);

                    panel.Controls.Add(picture, j, i);
                }
            panel.ResumeLayout();
        }

        /// <summary>
        /// этот метот создает кнопку следующего хода
        /// </summary>
        /// <returns></returns>
        private Button GetNextTurnButton()
        {
            var button = new Button() { Text = "Следующий Ход", ForeColor = Color.White };
            button.BackColor = Players[CurrentPlayerIndex].InterfaceColor;
            button.Margin = new Padding(0);
            button.Dock = DockStyle.Fill;
            button.Click += NextTurnButton_Click;
            return button;            
        }
    }
}
