﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WPFGame.Data
{
    public class StageGraphics
    {
        public StageGraphics() { }

        public Double WindowWidth = (double)Application.Current.Resources["WindowWidth"];
        public Double WindowHeight = (double)Application.Current.Resources["WindowHeight"];
        public Size BackgroundSize = new Size((double)Application.Current.Resources["WindowWidth"], (double)Application.Current.Resources["WindowHeight"]);
        public Point FloorPos = new Point(0, (float)(double)Application.Current.Resources["WindowHeight"] - 98);
        public Point BackgroundPos = new Point(0, 0);

        //Menu graphics stored as {background, floor}
        public List<string> MainMenuGraphics = new List<string> { "../../VisualAssets/Backgrounds/Main_Menu_BG.png"};

        public List<string> Backgrounds = new List<string>
        {
            "../../VisualAssets/Backgrounds/BG_0.png",
            "../../VisualAssets/Backgrounds/BG_1.png",
            "../../VisualAssets/Backgrounds/BG_2.png"

        };

        public string EnterDoorMessage = "../../VisualAssets/Door/Door_text_white.png";
        public string DoorGraphic = "../../VisualAssets/Door/door.png";
        public string ChurchMenu = "";

        public string coin = "../../VisualAssets/Coin/Coin.png";

        public List<string> Hearts = new List<string>
        {
            "../../VisualAssets/Hearts/dead.png",
            "../../VisualAssets/Hearts/1heart.png",
            "../../VisualAssets/Hearts/2hearts.png",
            "../../VisualAssets/Hearts/3hearts.png",
            "../../VisualAssets/Hearts/4hearts.png",
            "../../VisualAssets/Hearts/5hearts.png"
        };
    }

}
        

