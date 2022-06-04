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
        /// данный метод создает TableLayoutPanel для хранения ячеек карты
        /// </summary>
        /// <returns></returns>
        public static TableLayoutPanel CreateFieldPanel()
        {
            FieldHeight = MapCreator.MapHeight;
            FieldWidth = MapCreator.MapWidth;

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

        /// <summary>
        /// данный метод создает таблицу с интерфейсом во время игры
        /// </summary>
        /// <param name="gameField">таблица с игровым полем</param>
        /// <param name="turnButton">кнопка смены хода</param>
        /// <returns></returns>
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

        /// <summary>
        /// этот метод создает главное меню в игре
        /// </summary>
        /// <param name="startGame">кнопка "начать игру"</param>
        /// <param name="rules">кнопка "правила"</param>
        /// <returns>таблица с главным меню</returns>
        public static TableLayoutPanel GetGameStartPanel(Button startGame, Button rules)
        {
            var panel = new TableLayoutPanel();
            panel.BackColor = Color.LightGreen;
            panel.Dock = DockStyle.Fill;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent,100));

            panel.Controls.Add(new Panel() { ForeColor = Color.Transparent },0,0);
            panel.Controls.Add(GetMenuTable(startGame, rules),1,0);
            panel.Controls.Add(new Panel() { ForeColor = Color.Transparent },2,0);
            return panel;
        }

        /// <summary>
        /// этот метод создает центральный столбец таблицы с главным меню (да, это таблица в таблице)
        /// </summary>
        /// <param name="startGame">кнопка "начать игру"</param>
        /// <param name="rules">кнопка "правила"</param>
        /// <returns></returns>
        private static TableLayoutPanel GetMenuTable(Button startGame, Button rules)
        {
            var firstPanel = new TableLayoutPanel();
            firstPanel.Dock = DockStyle.Fill;
            firstPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            firstPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            firstPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 40));

            var secondPanel = new TableLayoutPanel();
            secondPanel.Dock = DockStyle.Fill;
            secondPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            secondPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            secondPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            secondPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            secondPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            secondPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            secondPanel.Controls.Add(startGame, 0, 0);
            secondPanel.Controls.Add(rules, 0, 1);
            secondPanel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 0, 2);
            secondPanel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 0, 3);
            secondPanel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 0, 4);

            firstPanel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 0, 0);
            firstPanel.Controls.Add(secondPanel, 0, 1);
            return firstPanel;
        }

        /// <summary>
        /// этот метод создает страницу с правилами
        /// </summary>
        /// <param name="backToMenu"></param>
        /// <returns>таблица с правилами игры</returns>
        public static TableLayoutPanel GetRulesTable(Button backToMenu)
        {
            var panel = new TableLayoutPanel();
            panel.BackColor = Color.LightGreen;
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 75));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));

            var text = new Label()
            {
                Text = "Данная игра расчитана на 4х игроков. Каждый игрок выбирает свой цвет. В начале своего хода игрок может создать одного из трех юнитов: всадника, копейщика и мечника." +
                Environment.NewLine + "Цель игры - привести любого юнита в замок противника, тем самым уничтожив замок. Игра заканчивается когда на поле останется только один замок." +
                Environment.NewLine +
                Environment.NewLine + "Дальность хода юнитов отличается. " +
                Environment.NewLine + "Всадник может переместиться на 2 клетки в любом направлении, либо на 1 по диагонали. " +
                Environment.NewLine + "Мечник и копейщик могут переместиться на одну клетку в любом направлении, либо на 1 по диагонали.",
                Dock = DockStyle.Fill,
            };

            panel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 0, 0);
            panel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 0, 1);
            panel.Controls.Add(backToMenu, 0, 2);
            panel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 1, 0);
            panel.Controls.Add(text, 1, 1);
            panel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 1, 2);
            panel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 2, 0);
            panel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 2, 1);
            panel.Controls.Add(new Panel() { ForeColor = Color.Transparent }, 2, 2);
            panel.Dock = DockStyle.Fill;
            return panel;
        }
    }
}
