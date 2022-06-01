using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TBS_Game
{
    public class UnitSelectorForm : Form
    {
        Point SelectedCastlePosition;
        bool UnitCreated = false;
        //массив битмапов нужен для сравнения картинки кнопки с картинкой создаваемого юнита. При запросе картинки из ресурсов результат сравнения - false
        private Bitmap[] Pictures = new Bitmap[] { Resource1.grayKnight, Resource1.graySwordsman, Resource1.graySpearman };
        public UnitSelectorForm(Point selectedCastlePosition)
        {
            SelectedCastlePosition = selectedCastlePosition;
            var panel = GetSelectionPanel();
            Controls.Add(panel);
            FormClosed += UnitSelectorForm_FormClosed;
        }

        private PictureBox GetPictureBox(Bitmap picture)
        {
            var pictureBox = new PictureBox
            {
                BackgroundImage = picture
            };
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Margin = new Padding(0);
            pictureBox.Click += PictureBox_Click;
            return pictureBox;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            List<Point> points = new List<Point>();
            var pictureBox = (Control)sender;

            for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                {
                    if (dx != 0 && dy != 0) continue;
                    points.Add(new Point(SelectedCastlePosition.X+dx, SelectedCastlePosition.Y + dy));
                }

            points = points.Where(point => UnitMap.UnitsPositions[point.X, point.Y] == null).ToList();

            if (points.Count() > 0)
            {
                if (pictureBox.BackgroundImage == Pictures[0])
                {
                    UnitMap.UnitsPositions[points[0].X, points[0].Y] = new Knight(GameForm.Players[GameForm.CurrentPlayerIndex], points[0]);
                }
                else if (pictureBox.BackgroundImage == Pictures[1])
                {
                    UnitMap.UnitsPositions[points[0].X, points[0].Y] = new Swordsman(GameForm.Players[GameForm.CurrentPlayerIndex], points[0]);
                }
                else if(pictureBox.BackgroundImage == Pictures[2])
                {
                    UnitMap.UnitsPositions[points[0].X, points[0].Y] = new Spearman(GameForm.Players[GameForm.CurrentPlayerIndex], points[0]);
                }
                GameForm.Players[GameForm.CurrentPlayerIndex].OwnedUnits.Add(UnitMap.UnitsPositions[points[0].X, points[0].Y]);
                UnitCreated = true;
            }
            UnitMap.InitializeUnits(GameForm.Panel.GetControlFromPosition(points[0].Y, points[0].X) as PictureBox, points[0].X, points[0].Y);
            GameForm.SelectionInProggres = false;
            Close();    
        }

        /// <summary>
        /// конструктор таблицы для элементов окна
        /// </summary>
        /// <returns></returns>
        private TableLayoutPanel GetSelectionPanel()
        {
            var panel = new TableLayoutPanel();
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            panel.Controls.Add(GetPictureBox(Pictures[2]), 0, 0);
            panel.Controls.Add(GetPictureBox(Pictures[1]), 1, 0);
            panel.Controls.Add(GetPictureBox(Pictures[0]), 2, 0);
            panel.Dock = DockStyle.Fill;
            return panel;
        }

        /// <summary>
        /// Данный метод срабатывает при закрытии формы и предотвращает блокировку возможности создания юнита при отмене выбора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnitSelectorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (UnitCreated)
                return;
            else GameForm.SelectionInProggres = false;
        }
    }
}
