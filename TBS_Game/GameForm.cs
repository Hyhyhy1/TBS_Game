﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static TBS_Game.MapCreator;

namespace TBS_Game
{
    public partial class GameForm : Form
    {
        TableLayoutPanel Panel;
        public GameForm()
        {
            DoubleBuffered = true;
            GenerateMap(40);
            Panel = LayoutPanels.CreateFieldPanel();
            Controls.Add(Panel);
            InitializeMap(Panel);
        }

        private Point oldLocation;
        private bool isDragging = false;

        private void Event_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = true;
            }
        }

        private void Event_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var newLocation = MousePosition;
                Panel.Location = new Point(Panel.Location.X + (int)(2 * newLocation.X) - oldLocation.X, Panel.Location.Y + (int)(2 * newLocation.Y) - oldLocation.Y);
                Invalidate();
            }

            oldLocation = new Point((2 * MousePosition.X), (2 * MousePosition.Y));
        }

        private void Event_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = false;
            }

            if (e.Button == MouseButtons.Left)
            {
                var cellPosition = Panel.GetCellPosition((Control)sender);
                //CellClicked(cellPosition.Row, cellPosition.Column);
            }
        }

        /// <summary>
        /// этот метод создает pictureBox - ячейку поля
        /// </summary>
        /// <param name="pic">картинка для ячейки</param>
        /// <returns>pictureBox - ячейка поля</returns>
        public PictureBox GetPicture(Bitmap pic)
        {
            var picture = new PictureBox();
            picture.Image = pic;
            picture.Margin = new Padding(0);
            picture.Dock = DockStyle.Fill;
            picture.SizeMode = PictureBoxSizeMode.AutoSize;

            picture.MouseUp += Event_MouseUp;
            picture.MouseDown += Event_MouseDown;
            picture.MouseMove += Event_MouseMove;
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

            for (int i = 0; i < MapSize; i++)
                for (int j = 0; j < MapSize; j++)
                {
                    var picture = new PictureBox();
                    switch (Map[i, j])
                    {
                        case Cell.Grass:

                            picture = GetPicture(Resource1.Grass);

                            panel.Controls.Add(picture, i, j);
                            break;

                        case Cell.SmallForest:
                            Bitmap[] pictures = new[] { Resource1.SmallForest1, Resource1.SmallForest2 };
                            picture = GetPicture(pictures[rnd.Next(0, 1)]);

                            panel.Controls.Add(picture, i, j);

                            break;

                        case Cell.Forest:
                            pictures = new[] { Resource1.BigForest1, Resource1.BigForest2 };
                            picture = GetPicture(pictures[rnd.Next(0, 1)]);

                            panel.Controls.Add(picture, i, j);
                            break;
                    }
                }
            panel.ResumeLayout();
        }
    }
}
