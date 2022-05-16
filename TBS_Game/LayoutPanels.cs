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
        static private int FieldSize = 120;
        static private int FieldsCount = MapCreator.MapSize;
        public static TableLayoutPanel CreateMainPanel()
        {
            var panel = new TableLayoutPanel();
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            return panel;
        }

        public static TableLayoutPanel CreateFieldPanel()
        {
            var panel = new TableLayoutPanel();
            //panel.SuspendLayout();
            panel.Dock = DockStyle.Fill;
            panel.Name = "field";
            panel.Location = new Point(100, 100);

            panel.ColumnCount = FieldsCount;
            panel.RowCount = FieldsCount;

            panel.Width = FieldsCount * FieldSize;
            panel.Height = FieldsCount * FieldSize;

            for (int i = 0; i < FieldsCount; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, FieldSize ));
            }
            for (int j = 0; j < FieldsCount; j++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, FieldSize));
            }
            panel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            return panel;
        }
    }
}
