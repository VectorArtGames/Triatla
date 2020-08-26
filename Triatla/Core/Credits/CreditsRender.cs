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

		private static Credit[] Names = {
			new Credit
			{
				Text = "Programmer: Martin Magnusson",
				Color = ConsoleColor.Green
			},
			new Credit
			{
				Text = "The rest ..",
				Color = ConsoleColor.Yellow
            },
			new Credit
            {
				Text = "Ett skolprojekt för Ädelfors Folkhögskola",
				Color = ConsoleColor.Magenta
            }
		};

        public static async Task<bool> RollCredits()
		{
			foreach (var name in Names)
			{
				CursorTop = 0;
				MoveBufferArea(0, 0, BufferWidth, BufferHeight, 0, 1);
				foreach (var c in name.Text)
				{
					lock (Out)
                    {
						ForegroundColor = name.Color;
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
		public string Text;
		public ConsoleColor Color;
    }
}
