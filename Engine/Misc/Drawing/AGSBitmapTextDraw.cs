﻿using System;
using AGS.API;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace AGS.Engine
{
	public class AGSBitmapTextDraw : IBitmapTextDraw
	{
		private ITextConfig _config;
		private StringFormat _wrapFormat = new StringFormat (StringFormatFlags.NoClip);
		private Bitmap _bitmap;
		private int _maxWidth, _height;
		private string _text;

		public AGSBitmapTextDraw(Bitmap bitmap)
		{
			_bitmap = bitmap;
		}

		public void DrawText(string text, ITextConfig config, AGS.API.SizeF textSize, AGS.API.SizeF baseSize, 
			int maxWidth, int height)
		{
			_height = height;
			_text = text;
			_config = config;
			_maxWidth = maxWidth;
			IFont font = _config.Font;
			IBrush outlineBrush = _config.OutlineBrush;

			using (Graphics gfx = Graphics.FromImage (_bitmap)) 
			{
				gfx.SmoothingMode = SmoothingMode.AntiAlias;
				gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
				gfx.TextRenderingHint = TextRenderingHint.AntiAlias;
				gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
				gfx.CompositingQuality = CompositingQuality.HighQuality;

				float left = alignX(textSize.Width, baseSize);
				float top = alignY(textSize.Height, baseSize);
				float centerX = left + _config.OutlineWidth / 2f;
				float centerY = top + _config.OutlineWidth / 2f;
				float right = left + _config.OutlineWidth;
				float bottom = top + _config.OutlineWidth;

				gfx.Clear(Color.Transparent);

				if (_config.OutlineWidth > 0f)
				{
					drawString(gfx, outlineBrush, left, top);
					drawString(gfx, outlineBrush, centerX, top);
					drawString(gfx, outlineBrush, right, top);

					drawString(gfx, outlineBrush, left, centerY);
					drawString(gfx, outlineBrush, right, centerY);

					drawString(gfx, outlineBrush, left, bottom);
					drawString(gfx, outlineBrush, centerX, bottom);
					drawString(gfx, outlineBrush, right, bottom);
				}
				if (_config.ShadowBrush != null)
				{
					drawString(gfx, _config.ShadowBrush, centerX + _config.ShadowOffsetX, 
						centerY + _config.ShadowOffsetY);
				}
				drawString(gfx, _config.Brush, centerX, centerY);

				//This should be a better way to render the outline (DrawPath renders the outline, and FillPath renders the text)
				//but for some reason some lines are missing when we render like that, at least on the mac
				/*if (_outlineWidth > 0f)
				{
					GraphicsPath path = new GraphicsPath ();
					Pen outlinePen = new Pen (_outlineBrush, _outlineWidth) { LineJoin = LineJoin.Round };
					path.AddString(_text, _font.FontFamily, (int)_font.Style, _font.Size, new Point (), new StringFormat ());
					//gfx.ScaleTransform(1.3f, 1.35f);
					gfx.DrawPath(outlinePen, path);
					gfx.FillPath(_brush, path);
				}
				else 
					gfx.DrawString (_text, _font, _brush, 0f, 0f);*/
			}
		}

		private void drawString(Graphics gfx, IBrush ibrush, float x, float y)
		{
			Brush brush = getBrush(ibrush);
			if (brush == null)
				return;
			Font font = getFont(_config.Font);
			if (_maxWidth == int.MaxValue)
			{
				gfx.DrawString(_text, font, brush, x, y);
			}
			else
			{
				alignWrap();
				gfx.DrawString(_text, font, brush, new RectangleF(x, y, _maxWidth, _height),
					_wrapFormat);
			}
		}

		private Brush getBrush(IBrush brush)
		{
			return ((AGSBrush)brush).InnerBrush; //todo: build brush based on BrushType
		}

		private Font getFont(IFont font)
		{
			return ((AGSFont)font).InnerFont;
		}

		private void alignWrap()
		{
			switch (_config.Alignment)
			{
				case Alignment.TopLeft:
				case Alignment.MiddleLeft:
				case Alignment.BottomLeft:
					_wrapFormat.Alignment = StringAlignment.Near;
					break;
				case Alignment.TopCenter:
				case Alignment.MiddleCenter:
				case Alignment.BottomCenter:
					_wrapFormat.Alignment = StringAlignment.Center;
					break;
				default:
					_wrapFormat.Alignment = StringAlignment.Far;
					break;
			}
		}

		private float alignX(float width, AGS.API.SizeF baseSize)
		{
			const float reducePadding = 2f;
			switch (_config.Alignment)
			{
				case Alignment.TopLeft:
				case Alignment.MiddleLeft:
				case Alignment.BottomLeft:
					return -reducePadding + _config.PaddingLeft;
				case Alignment.TopCenter:
				case Alignment.MiddleCenter:
				case Alignment.BottomCenter:
					return baseSize.Width / 2 - width / 2 - reducePadding / 2;
				default:
					return baseSize.Width - width - reducePadding - _config.PaddingRight;
			}
		}

		private float alignY(float height, AGS.API.SizeF baseSize)
		{
			const float reducePadding = 2f;
			switch (_config.Alignment)
			{
				case Alignment.TopLeft:
				case Alignment.TopCenter:
				case Alignment.TopRight:
					return _bitmap.Height - baseSize.Height - reducePadding + _config.PaddingTop;
				case Alignment.MiddleLeft:
				case Alignment.MiddleCenter:
				case Alignment.MiddleRight:
					return _bitmap.Height - baseSize.Height/2f - height/2f - reducePadding/2f;
				default:
					return _bitmap.Height - height - reducePadding - _config.PaddingBottom;
			}
		}
	}
}
