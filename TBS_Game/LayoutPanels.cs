using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBS_Game
{
    public class LayoutPanels
    {
        static public int FieldSize = 128;
        static private int FieldHeight = MapCreator.MapHeight;
        static private int FieldWidth = MapCreator.MapWidth;

        /// <summary>
        /// данный метод создает TableLayoutPanel для хранения ячеек
        /// </summary>
        /// <returns></returns>
        public static TableLayoutPanel CreateFieldPanel()
        {
            var panel = new TableLayoutPanel();
            panel.Location = new Point(-100, -100);

            panel.ColumnCount = FieldWidth;
            panel.RowCount = FieldHeight;

            panel.Width = FieldWidth * FieldSize;
            panel.Height = FieldHeight * FieldSize;
            panel.Margin = new Padding(0);

            for (int i = 0; i < FieldHeight; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100/FieldHeight));
            }
            for (int j = 0; j < FieldWidth; j++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100/FieldWidth));
            }
            panel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            return panel;
        }
        //public static TableLayoutPanel CreateMainPanel(Button turnButton)
        //{
        //    var panel = new TableLayoutPanel();
        //    panel.Dock = DockStyle.Fill;
        //    panel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
        //    panel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
        //    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));
        //    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
        //    //panel.Controls.Add(new Panel() { BackColor = Color.Transparent, Dock = DockStyle.Fill }, 0, 0);
        //    //panel.Controls.Add(new Panel() { BackColor = Color.Transparent, Dock = DockStyle.Fill }, 0, 1);
        //    //panel.Controls.Add(new Panel() { BackColor = Color.Transparent, Dock = DockStyle.Fill }, 1, 0);
        //    panel.Controls.Add(turnButton, 1, 1);
        //    return panel;
        //}
        public static TableLayoutPanel CreateMainPanel(TableLayoutPanel gameField, Button turnButton)
        {
            var panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;

            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            panel.Controls.Add(gameField, 0, 0);
            panel.Controls.Add(new Panel() { BackColor = GameForm.Players[GameForm.CurrentPlayerIndex].InterfaceColor, Dock = DockStyle.Fill, Margin = new Padding(0)}, 0, 1);
            panel.Controls.Add(new Panel() { BackColor = GameForm.Players[GameForm.CurrentPlayerIndex].InterfaceColor, Dock = DockStyle.Fill, Margin = new Padding(0)}, 1, 0);
            panel.Controls.Add(turnButton, 1, 1);
            return panel;
        }
    }
}
