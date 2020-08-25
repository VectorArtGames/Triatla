using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Triatla.Core.Board;

namespace Triatla
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(Render.Initialize);
            Thread.Sleep(-1);
        }
    }
}
