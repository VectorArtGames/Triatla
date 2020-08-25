using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triatla.Core.Board.Data
{
    public struct BoardData
    {
        public static BDat[,] data;

        public static object Find(int x, int y)
        {
            return data?.Cast<BDat>()?.Where(dat => dat.X == x && dat.Y == y)?.FirstOrDefault() ?? null;
        }
    }

    public class BDat
    {
        public BDat(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsAvailable()
        {
            return true;
        }

        public bool IsSelected()
        {
            return true;
        }

        public char StateChar =>
            State == PlayerState.Cross ? 'X' : 'O';

        private PlayerState State { get; } 
            = PlayerState.Cross;

        public int X { get; }

        public int Y { get; }
    }

    public enum PlayerState
    {
        Cross,
        Circle
    }
}
