using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triatla.Core.Board.Data
{
	public struct HighestScoreData
	{
		public static Dictionary<char, HighestScoreData> Data = new Dictionary<char, HighestScoreData>()
		{
			{ 'X', new HighestScoreData() },
			{ 'O', new HighestScoreData() }
		};

		public int Score { get; set; }
		public BDat Character { get; set; }

		public bool IsAvailable =>
			Character != null;
	}
}
