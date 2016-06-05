﻿using System;
using AGS.API;
using OpenTK;

namespace AGS.Engine
{
	public class QuadVectors
	{
		public QuadVectors(IGame game)
		{
			TopLeft = new Vector3 ();
			BottomLeft = new Vector3 (0f, game.VirtualResolution.Height, 0f);
			TopRight = new Vector3 (game.VirtualResolution.Width, 0f, 0f);
			BottomRight = new Vector3 (game.VirtualResolution.Width, game.VirtualResolution.Height, 0f);
		}

		public QuadVectors(float x, float y, float width, float height)
		{
			TopLeft = new Vector3 (x, y, 0f);
			BottomLeft = new Vector3 (x, y + height, 0f);
			TopRight = new Vector3 (x + width, y, 0f);
			BottomRight = new Vector3 (x + width, y + height, 0f);
		}

		public Vector3 BottomLeft { get; private set; }
		public Vector3 BottomRight { get; private set; }
		public Vector3 TopLeft { get; private set; }
		public Vector3 TopRight { get; private set; }

		public void Render(int texture, float r = 1f, float g = 1f, float b = 1f, float a = 1f)
		{
			GLUtils.DrawQuad(texture, BottomLeft, BottomRight, TopLeft, TopRight, r, g, b, a);
		}
	}
}
