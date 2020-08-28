using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triatla.Core.Board.Data;
using Triatla.Core.Credits;
using Triatla.Core.Handlers;
using Triatla.Core.Managers;
using static System.Console;

namespace Triatla.Core.Board
{
    public class Render
    {
	    private const int AddedHeight = 3;

        /// <summary>
        /// Initialize Triatla
        /// </summary>
        public static void Initialize()
        {
            TitleEditor.ChangeTitle();

            InitializeBoard(12, () =>
            {

	            // Check for key presses
                Task.Run(MoveHandler.StartKeyPress);

                // Render board - Graphics
                RenderBoard();
            });
        }

        private static void SetSize(int size)
        {
	        WindowHeight = size + AddedHeight;
	        BufferHeight = WindowHeight;
	        BufferWidth = WindowWidth = size * 4;
        }

        private static void InitializeBoard(int size, Action callback)
        {
	        SetSize(size); // Sets Size of the console, etc.

            // Initializes the 2D Array
            var dat = new BDat[size, size];
            for (var i = 0; i < dat.GetLength(0); i++)
            {
                for (var j = 0; j < dat.GetLength(1); j++)
                {
                   var d = new BDat(j, i);

                   d.OnStateChanged += (s, g) =>
                   {
                       RenderBoard();
                   };

                   dat[i, j] = d;
                }
            }

            // Subscribe to selection changed event
            BoardData.Selection.OnVectorChange += (s, g) =>
            {
                RenderBoard();
            };

            // Set board data to dat (temp data)
            BoardData.Data = dat;

            BoardData.OnWinnerSelected += (s, g) =>
            {
                var win = new WinnerData
                {
	                Winner = g, 
	                WinTime = DateTime.Now
                };

                BoardData.Winner = win;

                //RenderBoard();
            };

            BoardData.OnPlaceChanged += (s, g) =>
            {
                // Check neighbors and check winner
                BoardData.Data.CheckNeighbors(g, 5, (i, state) =>
                {

	                var c = state.StateChar;
	                if (HighestScoreData.Data.ContainsKey(c) && HighestScoreData.Data[c] is HighestScoreData score)
	                {
                        if (score.Score > i) return;

                        var tempD = new HighestScoreData
		                {
                            Score = i,
                            Character = state
		                };
		                HighestScoreData.Data[c] = tempD;
	                }


                    if (i >= 5) // Check if any winner
						BoardData.CallWinnerSelected(state);


                    TitleEditor.RefreshTitle();
                });

                
                if (!BoardData.Data.IsFilled())
					RenderBoard();
            };

            // Invoke callback
            callback?.Invoke();
        }

        /// <summary>
        /// Render board of Triatla
        /// </summary>
        private static async void RenderBoard()
        {
	        // Disable cursor - Selection
	        CursorVisible = false;

            var data = BoardData.Data;
            if (BoardData.Winner != null)
            {
	            MoveHandler.MoveKeysEnabled = false;
	            var win = BoardData.Winner;

	            Clear();
	            ForegroundColor = ConsoleColor.Green;
	            WriteLine($"Player: '{win.Winner.StateChar}' Wins!\nCongratulations!");
                await Task.Delay(2000);
                await CreditsRender.CallCreditsRoll();
                return;
            }

            if (BoardData.Winner == null && data.IsFilled())
            {
	            MoveHandler.MoveKeysEnabled = false;
	            Clear();
	            WriteLine("Nobody Wins!");
	            await Task.Delay(2000);
				Debug.WriteLine("CALL 2");

				await CreditsRender.CallCreditsRoll();
                return;
            }

            for (var i = 0; i < data.GetLength(0); i++)
            {
                for (var j = 0; j < data.GetLength(1); j++)
                {
                    var dat = data[i, j];
                    lock (Out)
                    {
                        ForegroundColor = dat != null ? (dat.IsSelected ? (BoardData.CurrentState == PlayerState.Circle ? ConsoleColor.Green : ConsoleColor.Yellow) : (dat.GetState != PlayerState.None ? (dat.GetState == PlayerState.Circle ? ColorData.Team1Color : ColorData.Team2Color) : ConsoleColor.Red)) : ConsoleColor.Yellow;
                        
                        var b = $"[{dat?.StateChar ?? '?'}]";
                        CursorTop = i;
                        CursorLeft = b.Length * j;
                        Write(b);
                        ResetColor();
                    }
                }
            }

            lock (Out)
            {
	            BackgroundColor = ConsoleColor.Blue;
	            var status = $"Position X: {BoardData.Selection.X}, Y: {BoardData.Selection.Y} - Turn '{BoardData.CurrentState.GetChar()}'";
	            
	            var fCalc = BufferWidth / 2 - status.Length / 2;
	            var first = new string(' ', fCalc <= 0 ? 0 : fCalc);

	            var eCalc = BufferWidth - first.Length - status.Length;
	            var end = new string(' ', eCalc <= 0 ? 0 : eCalc);

                var lines = new string('\n', WindowHeight - data.GetLength(0) - 1);
                Write($"{lines}{first}{status}{end}");
                ResetColor();
            }
        }
    }
}
