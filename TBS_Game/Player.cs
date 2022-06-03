﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TBS_Game
{
    public class Player
    {
        public List<Unit> OwnedUnits = new List<Unit>();
        public readonly string PlayerColor;
        public readonly Color InterfaceColor;
        public Castle Castle;
        public Player(string color, Color interfaceColor)
        {
            PlayerColor = color;
            InterfaceColor = interfaceColor;
        }
        public static bool operator == (Player first, Player second)
        {
            return first.PlayerColor == second.PlayerColor;
        }

        public static bool operator !=(Player first, Player second)
        {
            return !first.PlayerColor.Equals(second.PlayerColor);
        }

        public static Player[] InitializePlayers()
        {
            List<Player> players = new List<Player>();
            players.Add(new Player("красного", Color.DarkRed));
            players.Add(new Player("синего", Color.LightSkyBlue));
            players.Add(new Player("зеленого", Color.LimeGreen));
            players.Add(new Player("серого", Color.LightGray));
            return players.ToArray();
        }
    }

}