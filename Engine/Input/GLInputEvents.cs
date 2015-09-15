﻿using System;
using AGS.API;
using OpenTK;
using OpenTK.Input;

namespace AGS.Engine
{
	public class GLInputEvents : IInputEvents
	{
		private GameWindow _game;
		private int _virtualWidth, _virtualHeight;
		private IGameState _state;

		public GLInputEvents (GameWindow game, IGameState state)
		{
			this._virtualWidth = game.Bounds.Width;
			this._virtualHeight = game.Bounds.Height;
			this._state = state;
				
			this._game = game;

			MouseDown = new AGSEvent<AGS.API.MouseButtonEventArgs> ();
			MouseUp = new AGSEvent<AGS.API.MouseButtonEventArgs> ();
			MouseMove = new AGSEvent<MousePositionEventArgs> ();
			KeyDown = new AGSEvent<KeyboardEventArgs> ();
			KeyUp = new AGSEvent<KeyboardEventArgs> ();

			game.MouseDown += async (sender, e) => await MouseDown.InvokeAsync(sender, new AGS.API.MouseButtonEventArgs(convert(e.Button), convertX(e.X), convertY(e.Y)));
			game.MouseUp += async (sender, e) => await MouseUp.InvokeAsync(sender, new AGS.API.MouseButtonEventArgs(convert(e.Button), convertX(e.X), convertY(e.Y)));
			game.MouseMove += async (sender, e) => await MouseMove.InvokeAsync(sender, new MousePositionEventArgs(convertX(e.X), convertY(e.Y)));
			game.KeyDown += async (sender, e) => await KeyDown.InvokeAsync(sender, new KeyboardEventArgs(convert(e.Key)));
			game.KeyUp += async (sender, e) => await KeyUp.InvokeAsync(sender, new KeyboardEventArgs(convert(e.Key)));
		}

		#region IInputEvents implementation

		public IEvent<AGS.API.MouseButtonEventArgs> MouseDown { get; private set; }

		public IEvent<AGS.API.MouseButtonEventArgs> MouseUp { get; private set; }

		public IEvent<MousePositionEventArgs> MouseMove { get; private set; }

		public IEvent<KeyboardEventArgs> KeyDown { get; private set; }

		public IEvent<KeyboardEventArgs> KeyUp { get; private set; }

		#endregion

		private AGS.API.MouseButton convert(OpenTK.Input.MouseButton button)
		{
			switch (button) {
			case OpenTK.Input.MouseButton.Left:
				return AGS.API.MouseButton.Left;
			case OpenTK.Input.MouseButton.Right:
				return AGS.API.MouseButton.Right;
			case OpenTK.Input.MouseButton.Middle:
				return AGS.API.MouseButton.Middle;
			default:
				throw new NotSupportedException ();
			}
		}

		private AGS.API.Key convert(OpenTK.Input.Key key)
		{
			return (AGS.API.Key)(int)key;
		}

		private float convertX(float x)
		{
			x = MathUtils.Lerp (0f, 0f, _game.ClientSize.Width, _virtualWidth, x);
			return x + getViewport().X;
		}

		private float convertY(float y)
		{
			y = MathUtils.Lerp (0f, _virtualHeight, _game.ClientSize.Height, 0f, y);
			return y + getViewport().Y;
		}

		private IViewport getViewport()
		{
			return _state.Player.Character.Room.Viewport;
		}
	}
}

