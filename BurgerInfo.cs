using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerInfo
{

    enum Material
    {
        VEGT = 0,
        PATTY,
        TOMATO,
        CHEESE,
        BREAD

    }

    class Burger
    {
        public Material material;
        private static Random rand = new Random();
        public Burger()
        {
            material = BurgerNum();
        }

        public void View_Mat()
        {
            string H_Mat = string.Empty;
            switch (material)
            {
                case Material.VEGT:
                    Console.ForegroundColor = ConsoleColor.Green;
                    H_Mat = "〓〓〓〓〓";
                    break;

                case Material.PATTY:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    H_Mat = "■■■■■";
                    break;

                case Material.TOMATO:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    H_Mat = "■■■■■";
                    break;

                case Material.CHEESE:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    H_Mat = "==========";
                    break;

                case Material.BREAD:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    H_Mat = "■■■■■";
                    break;
            }
            Console.WriteLine(H_Mat);
            Console.ResetColor();

        }

        // 렌덤으로 재료 출력
        public Material BurgerNum()
        {
            int NumtoMa = rand.Next(0, 5);

            switch (NumtoMa)
            {
                case 0:
                    return Material.VEGT;
                case 1:
                    return Material.PATTY;
                case 2:
                    return Material.TOMATO;
                case 3:
                    return Material.CHEESE;
                case 4:
                    return Material.BREAD;
                default:
                    return Material.BREAD;
            }
        }

        // 키 입력에 따라 바뀌는거
        public void ArrowtoMat(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    material = Material.VEGT;
                    break;

                case ConsoleKey.UpArrow:
                    material = Material.PATTY;
                    break;
                case ConsoleKey.DownArrow:
                    material = Material.TOMATO;
                    break;
                case ConsoleKey.RightArrow:
                    material = Material.CHEESE;
                    break;
                case ConsoleKey.Spacebar:
                    material = Material.BREAD;
                    break;
                default:
                    break;
            }

        }



    }

}
