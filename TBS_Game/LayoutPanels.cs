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
        static private int FieldsCount = MapCreator.MapSize;

        /// <summary>
        /// данный метод создает TableLayoutPanel для хранения ячеек
        /// </summary>
        /// <returns></returns>
        public static TableLayoutPanel CreateFieldPanel()
        {
            var panel = new TableLayoutPanel();
            panel.Location = new Point(-100, -100);

            panel.ColumnCount = FieldsCount;
            panel.RowCount = FieldsCount;

            panel.Width = FieldsCount * FieldSize;
            panel.Height = FieldsCount * FieldSize;

            for (int i = 0; i < FieldsCount; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, FieldSize));
            }
            for (int j = 0; j < FieldsCount; j++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, FieldSize));
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
