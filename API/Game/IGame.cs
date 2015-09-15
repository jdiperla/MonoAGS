﻿using System;
using System.Drawing;

namespace AGS.API
{
	public interface IGame
	{
		IGameFactory Factory { get; }
		IGameState State { get; }

		IGameLoop GameLoop { get; }
		ISaveLoad SaveLoad { get; }

		IInputEvents Input { get; }

		IGameEvents Events { get; }

		Size VirtualResolution { get; }

		void Start(string title, int width, int height);
	}
}

