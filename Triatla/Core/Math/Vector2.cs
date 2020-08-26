using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triatla.Core.Math
{
	public class Vector2
	{
		public event EventHandler OnVectorChange;

		public Vector2(int x, int y)
		{
			X = x;
			Y = y;
		}

		#region Properties

		private int _x;
		public int X
		{
			get => _x;
			set
			{
				_x = value;
				CallVectorChange();
			}
		}

		private int _y;

		public int Y
		{
			get => _y;
			set
			{
				_y = value;
				CallVectorChange();
			}
		}

		#endregion

		#region EVENT_CALLS

		protected void CallVectorChange() =>
			OnVectorChange?.Invoke(this, EventArgs.Empty);

		#endregion

		#region Methods

		public bool Matches(int x, int y) =>
			X == x && Y == y;

		#endregion

		#region OPERATORS

		public static Vector2 operator -(Vector2 left, Vector2 right) =>
			new Vector2(left.X - right.X, left.Y - right.Y);

		public static Vector2 operator +(Vector2 left, Vector2 right) =>
			new Vector2(left.X + right.X, left.Y + right.Y);

		#endregion

		/// <summary>
		/// Default Value of Zero (0, 0)
		/// </summary>
		public static Vector2 Zero = new Vector2(0,0);
	}
}
