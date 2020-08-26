using System;

namespace Triatla.Core.Board.Data
{
	public class BDat
	{
		public event EventHandler OnStateChanged;

		public BDat(int x, int y)
		{
			X = x;
			Y = y;
		}

		public bool IsAvailable =>
			State == PlayerState.None;

		public bool IsSelected =>
			BoardData.Selection.Matches(X, Y);

		public char StateChar =>
			State == PlayerState.None ? ' ' : (State == PlayerState.Circle ? 'O' : 'X');

		public void SetState(PlayerState state)
		{
			State = state;
			CallStateChanged();
		}

		public PlayerState GetState =>
			State;

		private PlayerState State { get; set; } 
			= PlayerState.None;

		public int X { get; }

		public int Y { get; }

		protected void CallStateChanged() =>
			OnStateChanged?.Invoke(this, EventArgs.Empty);
	}
}