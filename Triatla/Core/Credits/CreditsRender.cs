using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Triatla.Core.Credits
{
	public struct CreditsRender
	{

		private static bool IsAnimating { get; set; }
		private static bool HasPlayedCredits { get; set; }

		public static async Task<bool> CallCreditsRoll()
		{
			return !IsAnimating && !HasPlayedCredits && await RollCredits();
		}

		private static Credit[] Names = new[]
		{
			new Credit
			{
				text = "Programmer: Martin Magnusson",
				color = ConsoleColor.Green
			},
			new Credit
			{
				text = "The rest ..",
				color = ConsoleColor.Yellow
            },
			new Credit
            {
				text = "Ett skolprojekt för Ädelfors Folkhögskola",
				color = ConsoleColor.Magenta
            }
		};

        public static async Task<bool> RollCredits()
		{
			foreach (var name in Names)
			{
				CursorTop = 0;
				MoveBufferArea(0, 0, BufferWidth, BufferHeight, 0, 1);
				foreach (var c in name.text)
				{
					lock (Out)
                    {
						ForegroundColor = name.color;
						Write(c);
						ResetColor();
					}
					await Task.Delay(50);
				}
				WriteLine();
			}

			// Make some kind of scroll 'animation'
			for (var i = 0; i < BufferHeight; i++)
            {
				MoveBufferArea(0, 0, BufferWidth, BufferHeight, 0, 1);
				await Task.Delay(500);
            }
			return true;
		}

		public static void ResetCredits() =>
			IsAnimating = HasPlayedCredits = false;
	}

	public struct Credit
    {
		public string text;
		public ConsoleColor color;
    }
}
