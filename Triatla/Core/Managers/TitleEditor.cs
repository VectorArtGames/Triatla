using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triatla.Core.Board.Data;
using static System.Console;

namespace Triatla.Core.Managers
{
    public class TitleEditor
    {
	    public static void ChangeTitle()
        {
            Title = $"Triatla | Winning: N/A - Martin Magnusson";
        }

	    public static void RefreshTitle()
	    {
		    var dat = HighestScoreData.Data.OrderByDescending(x => x.Value.Score).FirstOrDefault().Value;

		    Title = $"Triatla | Winning: {(dat.IsAvailable ? $"'{dat.Character?.StateChar ?? '?'}' With {dat.Score}" : "N/A")} - Martin Magnusson";
        }
    }
}
