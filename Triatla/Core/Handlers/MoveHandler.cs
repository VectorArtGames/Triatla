using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Triatla.Core.Board.Data;

namespace Triatla.Core.Handlers
{
	public class MoveHandler
	{
		public static bool MoveKeysEnabled { get; set; } 
			= true;

		/// <summary>
		/// Starts checking key presses in a new thread
		/// </summary>
		public static void StartKeyPress()
		{
			new Thread(CheckKeyPress)
			{
				IsBackground = true,
			}.Start();
		}

		/// <summary>
		/// Check key presses
		/// </summary>
		private static void CheckKeyPress()
		{
			// Infinite loop
			while (true)
			{
				// Check for key press and get info
				var inf = Console.ReadKey(true);
				if (!MoveKeysEnabled) return;

				// Check controls / inputs
				switch (inf.Key)
				{
					// ARROWS
					case ConsoleKey.UpArrow:
						if (BoardData.Selection.Y - 1 < 0) break;
						BoardData.Selection.Y--;
						break;
					case ConsoleKey.DownArrow:
						if (BoardData.Selection.Y + 1 >= BoardData.Data?.GetLength(0)) break;
						BoardData.Selection.Y++;
						break;
					case ConsoleKey.LeftArrow:
						if (BoardData.Selection.X - 1 < 0) break;
						BoardData.Selection.X--;
						break;
					case ConsoleKey.RightArrow:
						if (BoardData.Selection.X + 1 >= BoardData.Data?.GetLength(1)) break;
						BoardData.Selection.X++;
						break;

					// WASD
					case ConsoleKey.W:
						if (BoardData.Selection.Y - 1 < 0) break;
						BoardData.Selection.Y--;
						break;
					case ConsoleKey.S:
						if (BoardData.Selection.Y + 1 >= BoardData.Data?.GetLength(0)) break;
						BoardData.Selection.Y++;
						break;
					case ConsoleKey.A:
						if (BoardData.Selection.X - 1 < 0) break;
						BoardData.Selection.X--;
						break;
					case ConsoleKey.D:
						if (BoardData.Selection.X + 1 >= BoardData.Data?.GetLength(1)) break;
						BoardData.Selection.X++;
						break;

					// Place state
					case ConsoleKey.Spacebar:
						BoardData.Place();
						break;
				}
			}
		}
	}
}
