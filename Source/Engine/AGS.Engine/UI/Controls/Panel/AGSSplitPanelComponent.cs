﻿using AGS.API;

namespace AGS.Engine
{
    public class AGSSplitPanelComponent : AGSComponent, ISplitPanelComponent
    {
        private readonly IInput _input;
        private readonly IGameState _state;
        private readonly IGameFactory _factory;

        private IPanel _topPanel, _bottomPanel;
        private bool _isHorizontal;
        private Vector2 _startPositionDragLine, _startPositionBottomPanel;
		private SizeF _startSizeTopPanel, _startSizeBottomPanel;

        public AGSSplitPanelComponent(IInput input, IGameState state, IGameFactory factory)
        {
            _input = input;
            _state = state;
            _factory = factory;
        }

        public IPanel TopPanel 
        { 
            get { return _topPanel; } 
            set 
            { 
                if (_topPanel == value) return; 
                _topPanel = value;
                if (value != null)
                {
                    _startSizeTopPanel = new SizeF(value.Width, value.Height);
                }
                createNewSplitLine(); 
            } 
        }
        public IPanel BottomPanel 
        { 
            get { return _bottomPanel; } 
            set 
            { 
                if (_bottomPanel == value) return; 
                _bottomPanel = value;
                if (value != null)
                {
                    _startSizeBottomPanel = new SizeF(value.Width, value.Height);
                    _startPositionBottomPanel = new Vector2(value.X, value.Y);
                }
                createNewSplitLine(); 
            } 
        }
        public bool IsHorizontal { get { return _isHorizontal; } set { if (_isHorizontal == value) return; _isHorizontal = value; createNewSplitLine(); } }
        public IObject DragLine { get; private set; }

        private void disposeExistingSplitLine()
        {
            var existing = DragLine;
            if (existing != null)
            {
                existing.Visible = false;
                _state.UI.Remove(existing);
                _state.Room.Objects.Remove(existing);
                existing.OnLocationChanged.Unsubscribe(onSplitLineMoved);
            }
        }

        private void createNewSplitLine()
        {
            disposeExistingSplitLine();
			var topPanel = TopPanel;
			if (topPanel == null) return;

            const float lineWidth = 10f;
            var splitLine = _factory.Object.GetObject(string.Format("{0}_SplitLine", topPanel.ID));
            splitLine.RenderLayer = topPanel.RenderLayer;
			var crop = topPanel.GetComponent<ICropChildrenComponent>();
			if (crop != null) crop.EntitiesToSkipCrop.Add(splitLine.ID);
            HoverEffect.Add(splitLine, Colors.Transparent, Colors.Yellow.WithAlpha(100));
			splitLine.Anchor = new PointF(0f, 0f);
            positionSplitLine(splitLine, topPanel, lineWidth);
            splitLine.Z = -1;
            topPanel.OnBoundingBoxesChanged.Subscribe(() => 
            {
                positionSplitLine(splitLine, topPanel, lineWidth);    
            });
            _startPositionDragLine = new Vector2(splitLine.X, splitLine.Y);
            splitLine.AddComponent<IUIEvents>();
            var draggable = splitLine.AddComponent<IDraggableComponent>();
            draggable.DragMinX = IsHorizontal ? (float?)null : splitLine.X;
            draggable.DragMaxX = IsHorizontal ? (float?)null : splitLine.X;
            draggable.DragMinY = IsHorizontal ? splitLine.Y : (float?)null;
            draggable.DragMaxY = IsHorizontal ? splitLine.Y : (float?)null;

            var room = topPanel.Room;
            if (room != null) room.Objects.Add(splitLine);
            else _state.UI.Add(splitLine);
            splitLine.OnLocationChanged.Subscribe(onSplitLineMoved);
            DragLine = splitLine;
        }

        private void positionSplitLine(IObject splitLine, IPanel topPanel, float lineWidth)
        {
			var boundingBoxes = topPanel.GetBoundingBoxes(_state.Viewport);
			var box = boundingBoxes.RenderBox;
			float width = IsHorizontal ? lineWidth : box.Width;
			float height = IsHorizontal ? box.Height : lineWidth;
			splitLine.Image = new EmptyImage(width, height);
			float anchorX = -topPanel.Width * topPanel.Anchor.X;
			splitLine.X = IsHorizontal ? anchorX + topPanel.X + box.Width - lineWidth / 2f : anchorX;
			splitLine.Y = box.MinY;
        }

        private void onSplitLineMoved()
        {
			var topPanel = TopPanel;
            if (topPanel == null) return;
			var bottomPanel = BottomPanel;
            var dragLine = DragLine;
            if (dragLine == null) return;

            if (IsHorizontal)
            {
                float delta = _startPositionDragLine.X - dragLine.X;
                topPanel.Image = new EmptyImage(_startSizeTopPanel.Width - delta, topPanel.Height);
                if (bottomPanel != null)
                {
                    bottomPanel.Image = new EmptyImage(_startSizeBottomPanel.Height + delta, bottomPanel.Height);
                    bottomPanel.X = _startPositionBottomPanel.X + delta;
                }
            }
            else
            {
                float delta = dragLine.Y - _startPositionDragLine.Y;
                topPanel.Image = new EmptyImage(topPanel.Width, _startSizeTopPanel.Height - delta);
                topPanel.Y = _startPositionBottomPanel.Y + delta;
                if (bottomPanel != null)
                {
                    bottomPanel.Image = new EmptyImage(bottomPanel.Width, _startSizeBottomPanel.Height + delta);
                    bottomPanel.Y = _startPositionBottomPanel.Y + delta;
                }
            }
        }
	}
}
