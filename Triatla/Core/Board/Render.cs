using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triatla.Core.Board.Data;
using Triatla.Core.Managers;
using static System.Console;

namespace Triatla.Core.Board
{
    public class Render
    {
        /// <summary>
        /// Initialize Triatla
        /// </summary>
        public static void Initialize()
        {
            TitleEditor.ChangeTitle();
            InitializeBoard(5, () =>
            {
                CursorVisible = false;
                RenderBoard();
            });
        }

        private static void InitializeBoard(int size, Action callback)
        {
            var dat = new BDat[size, size];
            for (var i = 0; i < dat.GetLength(0); i++)
            {
                for (var j = 0; j < dat.GetLength(1); j++)
                {
                    dat[i, j] = new BDat(i, j);
                }
            }

            BoardData.data = dat;
            callback?.Invoke();
        }

        /// <summary>
        /// Render board of Triatla
        /// </summary>
        private static void RenderBoard()
        {
            var data = BoardData.data;
            for (var i = 0; i < data.GetLength(0); i++)
            {
                for (var j = 0; j < data.GetLength(1); j++)
                {
                    var dat = data[i, j];
                    lock (Out)
                    {
                        ForegroundColor = ConsoleColor.Green;
                        var b = $"[{dat?.StateChar ?? '?'}]";
                        CursorTop = i;
                        CursorLeft = b.Length * j;
                        Write(b);
                    }
                }
            }

            Debug.WriteLine($"Found {(BoardData.Find(4, 4) is BDat d ? d.StateChar.ToString() : "Nothing!")}");
        }
    }
}
