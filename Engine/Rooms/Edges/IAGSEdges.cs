﻿using System;
using AGS.API;

namespace AGS.Engine
{
	public interface IAGSEdges : IEdges
	{
		void OnRepeatedlyExecute(ICharacter character);
	}
}

