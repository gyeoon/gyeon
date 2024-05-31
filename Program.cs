using System;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Timers;
using GameInfo;
using BurgerInfo;




namespace _240524_2
{
    class Program
    {
       

        private static System.Timers.Timer timer;
        private static bool TimeOver = false; // 타임오버됐는가?
        private static int remainingTime = 20;
        private static int totalTime = 20;


        private static void SetTimer()
        {
            if (timer != null) // 타이머가 이미 실행중이면
            {
                timer.Stop(); //타이머 멈추고
                timer.Dispose(); // 자원해제
            }
            timer = new System.Timers.Timer(1000); // 1초마다 이벤트 발생 -> 
            timer.Elapsed += OnTimedEvent; // 타이머 이벤트 핸들러
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        // 시간 감소하고 멈추는거
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            remainingTime--; // 남은 시간 감소

            if (remainingTime <= 0)
            {
                TimeOver = true;
                timer.Stop();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(17, 1); // 시간을 표시할 위치로 이동
                Console.WriteLine($"남은 시간: {remainingTime}  ");
                Console.ResetColor();
            }
        }
        // 텍스트 반짝임
        static void BlinkingText(string text, int top)
        {
            int delay = 700; // 주기, 밀리초
            int cursorLeft = (Console.WindowWidth - text.Length) / 2;

            for (int i = 0; i < 10; i++) // 최대 5번
            {
                if (Console.KeyAvailable) // 키 입력이 있으면 중단
                    break;

                Console.SetCursorPosition(cursorLeft, top); // 위치 조정
                Console.Write(i % 2 == 0 ? text : new string(' ', text.Length)); // 조건이 참이면(짝수) 텍스트를, 아니면(홀수) 공백문자 출력
                Thread.Sleep(delay);// 출력하고 좀 기다림
            }
        }

        // 게임시작화면
        static void StartImage()
        {
            Console.OutputEncoding = Encoding.UTF8;

            string asciiArt = @"


           █████                                                     
          ░░███                                                      
           ░███████  █████ ████ ████████   ███████  ██████  ████████ 
           ░███░░███░░███ ░███ ░░███░░███ ███░░███ ███░░███░░███░░███
           ░███ ░███ ░███ ░███  ░███ ░░░ ░███ ░███ ███████  ░███ ░░░ 
           ░███ ░███ ░███ ░███  ░███     ░███ ░███ ███░░░   ░███     
           ████████  ░░████████ █████    ░░███████ ░██████  █████    
          ░░░░░░░░    ░░░░░░░░ ░░░░░      ░░░░░███ ░░░░░░  ░░░░░                                                                                                  ███ ░███                   
                                         ░░██████                    
                                          ░░░░░░        
                                                   
                                                                  █████   ████  ███                     
                                                                 ░░███   ███░  ░░░                      
                                                                  ░███  ███    ████  ████████    ███████
                                                                  ░███████    ░░███ ░░███░░███  ███░░███
                                                                  ░███░░███    ░███  ░███ ░███ ░███ ░███
                                                                  ░███ ░░███   ░███  ░███ ░███ ░███ ░███
                                                                  █████ ░░████ █████ ████ █████░░███████
                                                                 ░░░░░   ░░░░ ░░░░░ ░░░░ ░░░░░  ░░░░░███
                                                                                                ███ ░███
                                                                                               ░░██████ 
                                                                                                ░░░░░░  
";

            Console.Clear(); // 일단 콘솔창 비우고
            Console.WriteLine(asciiArt);

            // 메시지 반짝이게 하기
            string startGameMessage = "[Press any key to Start.]";
            int messageTop = Console.WindowHeight - 5; // 하단 위치 조정
            BlinkingText(startGameMessage, messageTop); // 적절한 위치에 텍스트 배치

            if (!Console.KeyAvailable) // 키 입력이 없으면 마지막에 메시지 출력된 상태로 유지
            {
                Console.SetCursorPosition((Console.WindowWidth - startGameMessage.Length) / 2, messageTop);
                Console.Write(startGameMessage);
                while (!Console.KeyAvailable)
                {
                    // 키 입력 대기
                }
            }
            Console.ReadKey(true); // 키 입력 처리
        }

        public static class NativeMethods
        {
            private const int MF_BYCOMMAND = 0x00000000;
            private const int SC_SIZE = 0xF000;
            private const int SC_MAXIMIZE = 0xF030;
            private const int SC_MINIMIZE = 0xF020;

            [DllImport("user32.dll")]
            private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

            [DllImport("user32.dll")]
            private static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

            [DllImport("kernel32.dll", ExactSpelling = true)]
            private static extern IntPtr GetConsoleWindow();

            public static void DisableConsoleResize()
            {
                IntPtr hWnd = GetConsoleWindow();
                IntPtr hMenu = GetSystemMenu(hWnd, false);
                RemoveMenu(hMenu, SC_SIZE, MF_BYCOMMAND);
                RemoveMenu(hMenu, SC_MAXIMIZE, MF_BYCOMMAND);
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(120, 30);
            Console.SetBufferSize(120, 30);
            NativeMethods.DisableConsoleResize();
            StartImage();

            // 이후 동작
            Console.Clear();
            #region(게임출력);
            Console.SetCursorPosition(15, 5);
            Console.WriteLine("버거짱에 오신 것을 환영합니다!");
            Console.SetCursorPosition(3, 10);
            Console.WriteLine("당신을 천호옹역 버거짱의 새로운 사장으로 임명합니다.");
            Console.SetCursorPosition(3, 12);
            Console.WriteLine("경일 게임 아카데미의 학생들을 사로잡아");
            Console.SetCursorPosition(3, 13);
            Console.WriteLine("최고의 햄버거 가게로 자리 잡으세요");
            Console.SetCursorPosition(3, 15);
            Console.WriteLine("부디 YES브랜드의 매출을 뛰어넘는 가게가 되길!");
            Console.SetCursorPosition(3, 17);
            Console.WriteLine("행운을 빕니다!");
            Console.SetCursorPosition(15, 20);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ■■■");
            Console.SetCursorPosition(15, 21);
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(15, 22);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(15, 23);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("〓〓〓〓〓");
            Console.SetCursorPosition(15, 24);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==========");
            Console.SetCursorPosition(15, 25);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(15, 26);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("■■■■■");

            Console.SetCursorPosition(35, 20);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ■■■");
            Console.SetCursorPosition(35, 21);
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(35, 22);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("〓〓〓〓〓");
            Console.SetCursorPosition(35, 23);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(35, 24);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==========");
            Console.SetCursorPosition(35, 25);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(35, 26);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("■■■■■");


            Console.ResetColor();
            Console.SetCursorPosition(80, 5);
            Console.WriteLine("게임방법");
            Console.SetCursorPosition(65, 10);
            Console.WriteLine("제한시간 안에 방향키를 사용하여 손님이 원하는 햄버거를");
            Console.SetCursorPosition(65, 11);
            Console.WriteLine("만드세요. ");
            Console.SetCursorPosition(65, 13);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("←: 상추    (〓〓〓〓〓)");
            Console.SetCursorPosition(65, 15);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("↑: 패티    (■■■■■)");
            Console.SetCursorPosition(65, 17);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("↓: 토마토  (■■■■■)");
            Console.SetCursorPosition(65, 19);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("→: 치즈    (==========)");
            Console.SetCursorPosition(65, 21);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SPACE: 빵   (■■■■■)");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(100, 15);
            Console.WriteLine("  ■■■");
            Console.SetCursorPosition(100, 16);
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(95, 18);
            Console.WriteLine("ENTER: 빵 얹고 제출");
            Console.ResetColor();
            Console.SetCursorPosition(65, 23);
            Console.WriteLine("스테이지를 클리어할수록 제한시간은 점점 줄어듭니다!");
            Console.ReadKey(true);
            #endregion
            Console.Clear();
            HowtoHidden();  // 게임방법
            Console.SetCursorPosition(45, 8);
            Console.WriteLine("당신의 이름을 알려주세요: ");
            Console.SetCursorPosition(54, 10);
            string name = Console.ReadLine();
            Player player = new Player(name);
            Thread.Sleep(600);

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(43, 12);
                Console.WriteLine($"{name} 사장님! 가게를 오픈할게요!");
                player.chance = 3; // 남은 기회 초기화
                player.Score = 0; // 점수 초기화

                Console.ResetColor();
                Console.SetCursorPosition(47, 14);
                Console.WriteLine("무엇을 하시겠습니까? ");
                Console.SetCursorPosition(41, 16);
                Console.WriteLine(" 1) 장사 시작  2) 역대 매출 확인");

                while (Console.KeyAvailable) // 입력 버퍼 지우기
                {
                    Console.ReadKey(true);
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        break;
                    case ConsoleKey.D2:
                        player.ShowScore();
                        Thread.Sleep(1800);
                        Console.Clear();
                        continue;
                    default:
                        Console.SetCursorPosition(47, 17);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("올바르지 않은 키여요");
                        Console.ResetColor();
                        Thread.Sleep(1800);
                        Console.Clear();
                        continue;

                }

                Console.Clear();
                // --------------------------------------------게임 실행------------------------------------
                Random rand = new Random();
                int RoundCount = 1;
                while (player.chance > 0)
                {
                    if (RoundCount % 4 != 0)  // 일반 스테이지
                    {

                        Console.Clear();
                        SetTimer();//타이머 시작

                        GameUI(); // 게임UI
                        player.ShowChance();  // 남은 기회
                        Console.SetCursorPosition(12, 1);
                        remainingTime = totalTime;  // 시간초기화
                        Console.SetCursorPosition(3, 1);
                        Console.WriteLine($"{RoundCount}번째 손님 ");
                        Console.SetCursorPosition(52, 1);
                        Console.WriteLine($"현재 매출: {player.Score}원");
                        List<Burger> CustomerBurger = Randomburger(); // 랜덤버거 생성하고
                        Show_Burger(CustomerBurger);  // 출력해
                        List<Burger> PlayerBurger = new List<Burger>();  // 플레이어가 만들 버거 리스트
                        bool isCorrect = true;                    // 손님 버거랑 내 버거랑 같은지
                                                                  // 사용자가 입력하는 로직
                        while (player.chance > 0)
                        {
                            while (Console.KeyAvailable) // 입력 버퍼 지우기
                            {
                                Console.ReadKey(true);
                            }
                            Console.SetCursorPosition(20, 18);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("■■■■■");

                            var key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.Enter) // 엔터 누르면 맨위 빵 얹고 제출
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.SetCursorPosition(20, 17 - PlayerBurger.Count - 1);
                                Console.WriteLine("  ■■■");
                                Console.SetCursorPosition(20, 17 - PlayerBurger.Count);
                                Console.WriteLine("■■■■■");
                                Thread.Sleep(100);
                                break;
                            }
                            if (TimeOver)
                            {

                                break;
                            }

                            else if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow // 지정된 키 누르면 요소 쌓기
                                || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow || key == ConsoleKey.Spacebar)
                            {
                                Burger Userinsert = new Burger(); // 리스트에 넣을 객체 생성
                                Userinsert.ArrowtoMat(key);  // 키 입력값 받아
                                PlayerBurger.Add(Userinsert); // 객체 리스트에 넣어

                                for (int i = 0; i < PlayerBurger.Count; i++) // 실시간으로 값 비교해
                                {
                                    if (PlayerBurger[i].material != CustomerBurger[i].material ||
                                        PlayerBurger.Count > CustomerBurger.Count)  // 내가 손님거보다 더 쌓았을 경우 or 다른걸 쌓았을 경우
                                    {
                                        isCorrect = false;
                                        break;
                                    }
                                }


                                if (!isCorrect) // 손님 버거랑 같지 않을 때
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition(19, 25);
                                    Console.WriteLine("이게 아냐!");
                                    Console.SetCursorPosition(80, 25);
                                    Console.WriteLine("난 이런걸 주문한 적이 없는데...");
                                    Thread.Sleep(900);
                                    Console.SetCursorPosition(80, 25);
                                    Console.WriteLine("                             ");
                                    Console.SetCursorPosition(15, 25);
                                    Console.WriteLine("                             ");
                                    Console.ResetColor();
                                    player.chance--;
                                    break;
                                }
                                else // 손님 버거랑 요소가 같으면 출력
                                {
                                    for (int i = 0; i < PlayerBurger.Count; i++)
                                    {
                                        Console.SetCursorPosition(20, 17 - i); // 콘솔 창에서 버거의 위치를 설정
                                        PlayerBurger[i].View_Mat();
                                    }

                                }
                            }

                            else  // 지정된 키 말고 다른걸 눌렀을 때
                            {
                                Console.ResetColor();
                                Console.SetCursorPosition(15, 25);
                                Console.WriteLine(" 그런 재료는 없어!! ");
                                Thread.Sleep(300);
                                Console.SetCursorPosition(15, 25);
                                Console.WriteLine("                             ");
                                Console.ResetColor();
                            }

                        } // 플레이어 입력받는 while문 끝남

                        //결과 비교
                        if (TimeOver)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(15, 25);
                            Console.WriteLine("죄송합니다...");
                            Console.SetCursorPosition(80, 25);
                            Console.WriteLine("제거 언제나와요!!!!!!!!?");
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(80, 25);
                            Console.WriteLine("                             ");
                            Console.SetCursorPosition(15, 25);
                            Console.WriteLine("                             ");
                            Console.ResetColor();
                            player.chance--;
                            TimeOver = false;
                            continue;
                        }

                        if (PlayerBurger.Count == CustomerBurger.Count && isCorrect)
                        {
                            RoundCount++;
                            player.Score += 5000;
                            totalTime--;
                            if (remainingTime < 6)
                            {
                                remainingTime = 5;  // 계속 줄다가 5초로 고정
                            }

                            Console.SetCursorPosition(15, 25);
                            Console.WriteLine("버거 나갑니다옹~");
                            Console.SetCursorPosition(90, 25);
                            Console.WriteLine("잘 먹겠습니다~");
                            Thread.Sleep(1000);


                            continue; // 올바르게 만든 경우 반복문 탈출
                        }


                        else if (!isCorrect) // 이미 전에 if문에서 카운트 깠으니까
                            continue;
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(19, 25);
                            Console.WriteLine("이게 아냐!");
                            Console.SetCursorPosition(80, 25);
                            Console.WriteLine("난 이런걸 주문한 적이 없는데...");
                            Thread.Sleep(900);
                            Console.SetCursorPosition(80, 25);
                            Console.WriteLine("                             ");
                            Console.SetCursorPosition(15, 25);
                            Console.WriteLine("                             ");
                            Console.ResetColor();
                            player.chance--;
                            continue;
                        }


                    }

                    else // 히든스테이지 발동 조건
                    {
                        Console.Clear();
                        SetTimer();//타이머 시작
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(47, 23);
                        Console.WriteLine("별난 손님 등장!!!!!!!!!!!!!!");
                        Console.SetCursorPosition(49, 24);
                        Console.WriteLine("버거를 반대로 쌓으세요!!!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(37, 27);
                        Console.WriteLine("마지막 빵(■■■■■)은 ENTER를 누르셔야합니다! ");
                        Console.ResetColor();
                        GameUI(); // 게임UI
                        player.ShowChance();  // 남은 기회
                        Console.SetCursorPosition(12, 1);
                        remainingTime = totalTime;  // 시간초기화
                        Console.SetCursorPosition(3, 1);
                        Console.WriteLine($"{RoundCount}번째 손님 ");
                        Console.SetCursorPosition(52, 1);
                        Console.WriteLine($"현재 매출: {player.Score}원");
                        List<Burger> CustomerBurger = Randomburger(); // 랜덤버거 생성하고
                        Show_Burger(CustomerBurger);  // 출력해
                        List<Burger> PlayerBurger = new List<Burger>();  // 플레이어가 만들 버거 리스트
                        bool isCorrect = true;                    // 손님 버거랑 내 버거랑 같은지

                        while (true)

                        {

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(20, 17 - CustomerBurger.Count - 1);
                            Console.WriteLine("  ■■■");
                            Console.SetCursorPosition(20, 17 - CustomerBurger.Count);
                            Console.WriteLine("■■■■■");

                            while (Console.KeyAvailable) // 입력 버퍼 지우기
                            {
                                Console.ReadKey(true);
                            }

                            var key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.Enter) // 엔터 누르면 맨위 빵 얹고 제출
                            {
                                Console.SetCursorPosition(20, 18 - CustomerBurger.Count + PlayerBurger.Count);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("■■■■■");
                                Thread.Sleep(100);
                                break;
                            }
                            if (TimeOver)
                            {

                                break;
                            }

                            else if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow // 지정된 키 누르면 요소 쌓기
                                || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow || key == ConsoleKey.Spacebar)
                            {
                                Burger Userinsert = new Burger(); // 리스트에 넣을 객체 생성
                                Userinsert.ArrowtoMat(key);  // 키 입력값 받아
                                PlayerBurger.Add(Userinsert); // 객체 리스트에 넣어
                                isCorrect = true;

                                for (int i = 0; i < PlayerBurger.Count; i++)
                                {
                                    if (i >= CustomerBurger.Count ||
                                        PlayerBurger[i].material != CustomerBurger[CustomerBurger.Count - 1 - i].material)
                                    {
                                        isCorrect = false;
                                        break;
                                    }
                                }


                                if (!isCorrect) // 손님 버거랑 같지 않을 때
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition(19, 25);
                                    Console.WriteLine("이게 아냐!");
                                    Console.SetCursorPosition(80, 25);
                                    Console.WriteLine("난 이런걸 주문한 적이 없는데...");
                                    Thread.Sleep(1000);
                                    Console.SetCursorPosition(80, 25);
                                    Console.WriteLine("                             ");
                                    Console.SetCursorPosition(15, 25);
                                    Console.WriteLine("                             ");
                                    Console.ResetColor();
                                    player.chance--;
                                    break;
                                }
                                else // 손님 버거랑 요소가 같으면 출력
                                {
                                    for (int j = 0; j < PlayerBurger.Count; j++)
                                    {
                                        Console.SetCursorPosition(20, 18 - CustomerBurger.Count + j); // 콘솔 창에서 버거의 위치를 설정
                                        PlayerBurger[j].View_Mat(); // 리스트의 각 요소인 Burger 객체의 View_Mat 메서드 호출
                                    }

                                }
                            }

                            else  // 지정된 키 말고 다른걸 눌렀을 때
                            {
                                Console.ResetColor();
                                Console.SetCursorPosition(15, 25);
                                Console.WriteLine(" 그런 재료는 없어!! ");
                                Thread.Sleep(500);
                                Console.SetCursorPosition(15, 25);
                                Console.WriteLine("                             ");
                            }

                        }
                        // 플레이어 입력받는 while문 끝남

                        //결과 비교
                        if (TimeOver)
                        {
                            Console.SetCursorPosition(15, 25);
                            Console.WriteLine("죄송합니다...");
                            Console.SetCursorPosition(80, 25);
                            Console.WriteLine("제거 언제나와요!!!!!!!!?");
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(80, 25);
                            Console.WriteLine("                             ");
                            Console.SetCursorPosition(15, 25);
                            Console.WriteLine("                             ");
                            Console.ResetColor();
                            player.chance--;
                            TimeOver = false;
                            continue;
                        }

                        if (PlayerBurger.Count == CustomerBurger.Count && isCorrect)
                        {
                            RoundCount++;
                            player.Score += 5000;
                            totalTime--;
                            if (remainingTime < 6)
                            {
                                remainingTime = 5;  // 계속 줄다가 5초로 고정
                            }
                            Console.SetCursorPosition(15, 25);
                            Console.WriteLine("버거 나갑니다옹~");
                            Console.SetCursorPosition(90, 25);
                            Console.WriteLine("잘 먹겠습니다~");
                            Thread.Sleep(1000);

                            continue; // 올바르게 만든 경우 반복문 탈출
                        }


                        else if (!isCorrect) // 이미 전에 if문에서 카운트 깠으니까
                            continue;
                        else
                        {
                            Console.SetCursorPosition(19, 25);
                            Console.WriteLine("이게 아냐!");
                            Console.SetCursorPosition(80, 25);
                            Console.WriteLine("난 이런걸 주문한 적이 없는데...");
                            Thread.Sleep(900);
                            Console.SetCursorPosition(80, 25);
                            Console.WriteLine("                             ");
                            Console.SetCursorPosition(15, 25);
                            Console.WriteLine("                             ");
                            player.chance--;
                        }

                    }

                }
                while (Console.KeyAvailable) // 입력 버퍼 지우기
                {
                    Console.ReadKey(true);
                }
                Console.Clear();
                timer.Stop();
                Console.SetCursorPosition(12, 1);
                Console.WriteLine("                "); // 타이머 메시지를 지움
                Console.SetCursorPosition(35, 10);
                Console.WriteLine("컴플레인이 너무 많이 들어와 잠시 영업을 중단할게요...");
                player.ArrangeScore(player.ScoreList, player.Score);
                remainingTime = 20;
                totalTime = 20; //  남은 시간 초기화
                Console.SetCursorPosition(50, 11);
                Console.WriteLine($"이번 매출액: {player.Score}");
                Console.SetCursorPosition(46, 13);
                Console.WriteLine("게임을 중단하시려면 esc를, ");
                Console.SetCursorPosition(38, 14);
                Console.WriteLine("로비로 돌아가시려면 그 외의 키를 눌러주셔요");
                var key_1 = Console.ReadKey(true).Key;
                if (key_1 == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    continue;
                }

                Console.Clear();

            }
            
        }

        // 버거를 랜덤하게 뽑는 매서드
        static List<Burger> Randomburger()
        {
            Random rand = new Random();
            int burgersize = rand.Next(2, 9);

            // 일단 마지막에 결과값 비교하삼 -> Bread 카운트 제어
            List<Burger> burgers = new List<Burger>();    // 크기가 정해져있지 않음!!             


            // 각 버거를 생성하고 리스트에 추가
            for (int i = 0; i < burgersize; i++)
            {
                Burger burger = new Burger();// 각 버거의 크기를 랜덤으로 설정
                burgers.Add(new Burger());
            }
            return burgers;
        }


        // 랜덤 버거를 출력
        static void Show_Burger(List<Burger> burgers)
        {
            Console.SetCursorPosition(92, 18);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("■■■■■");
            Console.ResetColor();

            for (int i = 0; i < burgers.Count; i++)
            {
                Console.SetCursorPosition(92, 17 - i); // 콘솔 창에서 버거의 위치를 설정
                burgers[i].View_Mat();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(92, 17 - burgers.Count - 1);
            Console.WriteLine("  ■■■");
            Console.SetCursorPosition(92, 17 - burgers.Count);
            Console.WriteLine("■■■■■");


        }

        // 게임 UI
        static void GameUI()
        {
            Console.SetCursorPosition(49, 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("←: 상추    〓〓〓〓〓");
            Console.SetCursorPosition(49, 7);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("↑: 패티");
            Console.SetCursorPosition(61, 7);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("■■■■■");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(49, 9);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("↓: 토마토  ■■■■■");
            Console.SetCursorPosition(49, 11);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("→: 치즈    ==========");
            Console.SetCursorPosition(49, 13);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SPACE: 빵   ■■■■■");
            Console.ResetColor();
            Console.SetCursorPosition(49, 15);
            Console.WriteLine("ENTER: 버거 나가요~");

            Console.SetCursorPosition(3, 3);
            Console.Write("┌" + new string('─', 40) + "┐");
            Console.SetCursorPosition(3, 20);
            Console.Write("└" + new string('─', 40) + "┘");
            Console.SetCursorPosition(75, 3);
            Console.Write("┌" + new string('─', 40) + "┐");
            Console.SetCursorPosition(75, 20);
            Console.Write("└" + new string('─', 40) + "┘");
            Console.SetCursorPosition(90, 5);
            Console.WriteLine("주문이요!!!!");
        }
        // 히든 스테이지 설명 
        static void HowtoHidden()
        {
            Console.SetCursorPosition(40, 5);
            Console.WriteLine("여기서 잠깐!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.SetCursorPosition(40, 10);
            Console.WriteLine("당신이 4명의 손님을 받을 때마다");
            Console.SetCursorPosition(38, 11);
            Console.WriteLine("특이하고 별난 손님이 찾아올겁니다!!!!");
            Console.SetCursorPosition(30, 13);
            Console.WriteLine("그때는 햄버거를 쌓는 방향을 !!반대로!! 해야합니다...ㄷㄷ");
            Console.SetCursorPosition(7, 19);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ■■■");
            Console.SetCursorPosition(7, 20);
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(7, 21);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(7, 22);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("〓〓〓〓〓");
            Console.SetCursorPosition(7, 23);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==========");
            Console.SetCursorPosition(7, 24);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(7, 25);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(58, 17);
            Console.ResetColor();

            Console.WriteLine("원래 순서 ");
            Console.SetCursorPosition(25, 19);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(40, 19);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(55, 19);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==========");
            Console.SetCursorPosition(70, 19);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("〓〓〓〓〓");
            Console.SetCursorPosition(85, 19);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("■■■■■");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(100, 18);
            Console.WriteLine("  ■■■");
            Console.SetCursorPosition(100, 19);
            Console.WriteLine("■■■■■");

            Console.SetCursorPosition(56, 23);
            Console.ResetColor();
            Console.WriteLine("!!별난 손님!!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(25, 25);
            Console.WriteLine("  ■■■");
            Console.SetCursorPosition(25, 26);
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(40, 26);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(55, 26);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("〓〓〓〓〓");
            Console.SetCursorPosition(70, 26);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==========");
            Console.SetCursorPosition(85, 26);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(100, 26);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("■■■■■");
            Console.SetCursorPosition(2, 1);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("게임을 시작하시려면 아무 키나 눌러주세요^,^");


            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}



