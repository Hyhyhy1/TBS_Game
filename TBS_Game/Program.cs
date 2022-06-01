using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBS_Game
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var gameForm = new GameForm();
            gameForm.Text = "TBS_Game";
            gameForm.Size = new System.Drawing.Size(1000, 700);
            gameForm.BackColor = System.Drawing.Color.Black;
            Application.Run(gameForm);
        }
    }
}
