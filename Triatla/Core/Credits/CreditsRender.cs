using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triatla.Core.Managers;
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
			// Make some kind of scroll 'animation'
			for (var i = 0; i < BufferHeight; i++)
			{
				MoveBufferArea(0, 0, BufferWidth, BufferHeight, 0, 1);
				await Task.Delay(350);
			}

			// Typing animation credits scene
			foreach (var name in Names)
			{
				CursorTop = 0;
				MoveBufferArea(0, 0, BufferWidth, BufferHeight, 0, 1);
				await Task.Delay(500);
				foreach (var c in name.Text)
				{
					lock (Out)
                    {
						ForegroundColor = name.Color;
						Write(c);
						ResetColor();
					}
					await Task.Delay(Rand.Next(100));
				}
				WriteLine();
			}

			// Make some kind of scroll 'animation'
			for (var i = 0; i < BufferHeight; i++)
            {
				MoveBufferArea(0, 0, BufferWidth, BufferHeight, 0, 1);
				await Task.Delay(500);
            }

			// Wait for a second
			await Task.Delay(1000);

			// Say one last message - Thank you for playing
			const string text = "Thank you for playing ..";
			CursorTop = BufferHeight / 2;
			CursorLeft = BufferWidth / 2 - text.Length / 2;
			WriteLine(text);
			TitleEditor.EndTitle();


			return true;
		}

		public static void ResetCredits() =>
			IsAnimating = HasPlayedCredits = false;



		public static Random Rand { get; } = new Random();
	}

	public struct Credit
    {
		public string Text;
		public ConsoleColor Color;
    }
}
