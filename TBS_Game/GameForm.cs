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

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        public static TableLayoutPanel Panel;
        private const int WM_SETREDRAW = 11;
        public GameForm()
        {
            Players = InitializePlayers();
            CurrentPlayerIndex = 0;
            DoubleBuffered = true;
            //MouseWheel += Event_MouseWheel;
            GenerateMap(16,8);
            Panel = LayoutPanels.CreateFieldPanel();
            InitializeMap(Panel);
            Controls.Add(LayoutPanels.CreateMainPanel(Panel,GetNextTurnButton()));
            //Controls.Add(Panel);
        }

        private void CellClicked(int row, int column)
        {
            if ((row < 0) || (column < 0)) return;

            if (UnitsPositions[row, column] == null) return;

            else if (UnitsPositions[row, column] == Players[CurrentPlayerIndex].Castle && !SelectionInProggres)
            {
                SelectionInProggres = true;
                Form unitSelector = new UnitSelectorForm(new Point(row,column));
                unitSelector.Size = new Size(3 * LayoutPanels.FieldSize, LayoutPanels.FieldSize);
                unitSelector.Show();
            }

        }

        private Point oldLocation;
        private bool isDragging = false;

        private void Event_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = true;
            }

            else if (e.Button == MouseButtons.Left)
            {
                var cellPosition = Panel.GetCellPosition((Control)sender);
                CellClicked(cellPosition.Row, cellPosition.Column);
            }
        }

        private void Event_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var newLocation = MousePosition;
                Panel.Location = new Point(Panel.Location.X + (int)(2 * newLocation.X) - oldLocation.X, Panel.Location.Y + (int)(2 * newLocation.Y) - oldLocation.Y);
            }

            oldLocation = new Point(2 * MousePosition.X, 2 * MousePosition.Y);
        }

        private void Event_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = false;
            }
        }

        private void Event_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                SendMessage(this.Handle, WM_SETREDRAW, false, 0);
                var delta = e.Delta > 0 ? LayoutPanels.FieldSize : -LayoutPanels.FieldSize;
                if (Panel.Width + delta > LayoutPanels.FieldSize * 10) Panel.Width += delta;
                if (Panel.Height + delta > LayoutPanels.FieldSize * 10) Panel.Height += delta;
            }
            finally
            {
                SendMessage(this.Handle, WM_SETREDRAW, true, 0);
                Refresh();
            }
        }

        private void NextTurnButton_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            int defeatedPlayersCount = 0;
            var table = ctrl.Parent as TableLayoutPanel;
            if (CurrentPlayerIndex == Players.Count() - 1)
            {
                CurrentPlayerIndex = 0;
            }
                
            else if (Players[CurrentPlayerIndex] == new Player(string.Empty, Color.Transparent))
            {
                defeatedPlayersCount += 1;
                CurrentPlayerIndex += 1;
                if (defeatedPlayersCount == 3)
                    throw new NotImplementedException();
            }
            else CurrentPlayerIndex += 1;
            ctrl.BackColor = Players[CurrentPlayerIndex].InterfaceColor;
            table.GetControlFromPosition(0,1).BackColor = Players[CurrentPlayerIndex].InterfaceColor;
            table.GetControlFromPosition(1,0).BackColor = Players[CurrentPlayerIndex].InterfaceColor;
            Invalidate();
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

            picture.MouseUp += Event_MouseUp;
            picture.MouseDown += Event_MouseDown;
            //picture.MouseMove += Event_MouseMove;
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
