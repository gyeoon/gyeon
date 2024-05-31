using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GameInfo
{

    class Player
    {
        public string Name;
        public int chance = 3;
        public int Score;
        public List<int> ScoreList;  // 플레이 기록 최대 5개 저장

        public Player(string name)
        {
            Name = name;
            ScoreList = new List<int>();
            Score = 0;

        }

        public void ShowScore()
        {
            Console.SetCursorPosition(45, 18);
            Console.WriteLine("역대 매출을 확인합니다! ");
            for (int i = 0; i < ScoreList.Count; i++)
            {
                Console.SetCursorPosition(52, 20 + i);
                Console.WriteLine($"▶  {i + 1}. {ScoreList[i]}");
            }
        }
        public void ShowChance()
        {
            Console.SetCursorPosition(110, 2);
            Console.ForegroundColor = ConsoleColor.Red;
            if (chance == 3)
            {
                Console.WriteLine("●●●");
            }
            else if (chance == 2)
            {
                Console.WriteLine("●●○");
            }
            else if (chance == 1)
            {
                Console.WriteLine("●○○");
            }
            Console.ResetColor();
        }
        public void ArrangeScore(List<int> ScoreList, int newScore)
        {
            ScoreList.Add(newScore); // 새로운 점수를 리스트에 추가
            ScoreList.Sort((a, b) => b.CompareTo(a)); // 내림차순 정렬
            if (ScoreList.Count > 5)
            {
                ScoreList.RemoveRange(5, ScoreList.Count - 5); // 상위 5개 점수만 유지
            }
        }

    }
}
