﻿using System;
using AGS.API;
using System.Drawing;
using System.Collections.Generic;
using AreaKey = System.Tuple<AGS.API.IArea, System.Drawing.Bitmap>;

namespace AGS.Engine
{
	public class AGSWalkBehindsMap
	{
		private readonly Dictionary<AreaKey, IImage> _images;
		private readonly Dictionary<AreaKey, IObject> _objects;
		private readonly WalkBehindBitmapCreator _bitmapCreator;
		private readonly IGraphicsFactory _factory;

		public AGSWalkBehindsMap(IGraphicsFactory factory)
		{
			_factory = factory;
			_images = new Dictionary<AreaKey, IImage> (100);
			_objects = new Dictionary<AreaKey, IObject> (100);
			_bitmapCreator = new WalkBehindBitmapCreator ();
		}

		public IObject GetDrawable(IWalkBehindArea area, Bitmap bg)
		{
			AreaKey key = new AreaKey (area, bg);
			IObject obj = _objects.GetOrAdd(key, () => new AGSObject (new AGSSprite ()) { Anchor = new AGSPoint() });
			obj.Z = area.Baseline == null ? area.Mask.MinY : area.Baseline.Value;
			obj.Image = _images.GetOrAdd(key, () => createImage(area, bg));
			return obj;
		}

		private IImage createImage(IWalkBehindArea area, Bitmap bg)
		{
			var bitmap = _bitmapCreator.Create(area, bg);
			return _factory.LoadImage(bitmap); 
		}
	}
}

