using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triatla.Core.Math;

namespace Triatla.Core.Board.Data
{

    /// <summary>
    /// General Board Data
    /// </summary>
    public struct BoardData
    {
	    public static WinnerData Winner;

	    public static event EventHandler<BDat> OnPlaceChanged;
	    public static event EventHandler<BDat> OnWinnerSelected;


        public static BDat[,] Data;

        public static object Find(int x, int y)
        {
            return (Data?.Cast<BDat>())?.FirstOrDefault(dat => dat.X == x && dat.Y == y);
        }

        public static Vector2 Selection = Vector2.Zero;

        public static PlayerState CurrentState { get; set; }
	        = PlayerState.Cross;

        public static void Place()
        {
	        var s = Selection;
	        var d = Data[s.Y, s.X];
	        if (!d.IsAvailable) return;
	        d.SetState(CurrentState);

	        CurrentState = CurrentState != PlayerState.None && CurrentState == PlayerState.Circle
		        ? PlayerState.Cross
		        : PlayerState.Circle;

            CallPlaceChanged(d);
        }

        public static void CallPlaceChanged(BDat state) =>
			OnPlaceChanged?.Invoke(null, state);

        public static void CallWinnerSelected(BDat state) =>
	        OnWinnerSelected?.Invoke(null, state);
    }

    public static class BoardDataExtensions
    {
	    public static bool IsFilled(this BDat[,] data) =>
		    data.Cast<BDat>().All(x => !x.IsAvailable);


		/// <summary>
		/// Check how many neighbors and callback max neighbors
		/// </summary>
		/// <param name="data">Board Data</param>
		/// <param name="start">Start base</param>
		/// <param name="length">length of scan</param>
		/// <param name="callback">callback of max</param>
		/// <returns>Max neighbor scan</returns>
	    public static int CheckNeighbors(this BDat[,] data, BDat start, int length, Action<int, BDat> callback)
        {
	        var dirs = new List<int>();
	        for (var i = 0; i < typeof(ScanDirection).GetEnumNames().Length; i++)
	        {
		        var dir = (ScanDirection)i;
		        var amount = data.CheckDirection(start, dir, length);
				dirs.Add(amount);
	        }

	        var max = dirs.Max();
            callback?.Invoke(max, start);

	        return max;
        }

        private static int CheckDirection(this BDat[,] data, BDat start, ScanDirection dir, int length)
        {
	        var maxSizeX = data.GetLength(1);
	        var maxSizeY = data.GetLength(0);

	        var total = 0;
	        var s = new Vector2(start.X, start.Y);

	        switch (dir)
	        {
                case ScanDirection.Up:
	                for (var i = 0; i < length; i++)
	                {
		                // Take bounds into consideration
		                if (0 > s.Y - i) continue;


		                var next = data[s.Y - i, s.X];
		                if (next.StateChar != start.StateChar) break;
		                total++;
	                }
                    break;
                case ScanDirection.Down:
	                for (var i = 0; i < length; i++)
	                {
		                // Take bounds into consideration
		                if (maxSizeY <= s.Y + i) continue;


		                var next = data[s.Y + i, s.X];
		                if (next.StateChar != start.StateChar) break;
						total++;
	                }
                    break;
                case ScanDirection.Left:
	                for (var i = 0; i < length; i++)
	                {
		                // Take bounds into consideration
		                if (0 > s.X - i) continue;


		                var next = data[s.Y, s.X - i];
		                if (next.StateChar != start.StateChar) break;
						total++;
	                }
                    break;
                case ScanDirection.Right:
	                for (var i = 0; i < length; i++)
	                {
		                // Take bounds into consideration
		                if (maxSizeX <= s.X + i) continue;


		                var next = data[s.Y, s.X + i];
		                if (next.StateChar != start.StateChar) break;
						total++;
	                }
                    break;
	        }

	        return total;
        }

        public enum ScanDirection
        {
            Right,
            Left,
            Up,
            Down,
        }
    }

    public enum PlayerState
    {
        None,
        Cross,
        Circle
    }

    public static class PlayerStateExtension
    {
	    public static char GetChar(this PlayerState state) =>
		    state == PlayerState.None ? '?' : (state == PlayerState.Circle ? 'O' : 'X');
    }
}
