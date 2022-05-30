using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBS_Game
{
    public class UnitSelectorForm : Form
    {
        public UnitSelectorForm()
        {
            var panel = new TableLayoutPanel();
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute,128));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128));
            panel.Controls.Add(GetPictureBox(Resource1.graySpearman), 0, 0);
            panel.Controls.Add(GetPictureBox(Resource1.graySwordsman), 1, 0);
            panel.Controls.Add(new Panel(), 2, 0);//Resource1.grayKnight
        }

        private PictureBox GetPictureBox(Bitmap picture)
        {
            var pictureBox = new PictureBox();
            pictureBox.Image = picture;
            pictureBox.Click += PictureBox_Click;
            return pictureBox;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            
        }
    }
}
