﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AGS.API;

namespace AGS.Engine
{
    public class AGSBoundingBoxComponent : AGSComponent, IBoundingBoxComponent
    {
        private bool _isHitTestBoxDirty, _isCropDirty, _areViewportsDirty;
        private ConcurrentDictionary<IViewport, ViewportBoundingBoxes> _boundingBoxes;
        private AGSBoundingBox _hitTestBox, _intermediateBox;
        private IModelMatrixComponent _matrix;
        private ICropSelfComponent _crop;
        private IAnimationContainer _animation;
        private IDrawableInfo _drawable;
        private IGameSettings _settings;
        private readonly IGLViewportMatrixFactory _layerViewports;
        private readonly IBoundingBoxBuilder _boundingBoxBuilder;
        private readonly IGameState _state;
        private IEntity _entity;

        public AGSBoundingBoxComponent(IRuntimeSettings settings, IGLViewportMatrixFactory layerViewports,
                                       IBoundingBoxBuilder boundingBoxBuilder, IGameState state, IGameEvents events)
        {
            _boundingBoxes = new ConcurrentDictionary<IViewport, ViewportBoundingBoxes>(new IdentityEqualityComparer<IViewport>());
            _boundingBoxes.TryAdd(state.Viewport, new ViewportBoundingBoxes(state.Viewport));
            _settings = settings;
            _state = state;
            OnBoundingBoxesChanged = new AGSEvent();
            _layerViewports = layerViewports;
            _boundingBoxBuilder = boundingBoxBuilder;
            boundingBoxBuilder.OnNewBoxBuildRequired.Subscribe(onHitTextBoxShouldChange);
            events.OnRoomChanging.Subscribe(onHitTextBoxShouldChange);
            onHitTextBoxShouldChange();
        }

        public IEvent OnBoundingBoxesChanged { get; private set; }

        public override void Init(IEntity entity)
        {
            _entity = entity;
            base.Init(entity);
            entity.Bind<IModelMatrixComponent>(c => { c.OnMatrixChanged.Subscribe(onHitTextBoxShouldChange); _matrix = c; },
                                               c => { c.OnMatrixChanged.Unsubscribe(onHitTextBoxShouldChange); _matrix = null; });
            entity.Bind<ICropSelfComponent>(c => { c.OnCropAreaChanged.Subscribe(onCropShouldChange); _crop = c; },
                                            c => { c.OnCropAreaChanged.Unsubscribe(onCropShouldChange); _crop = null; });
            entity.Bind<IImageComponent>(c => c.OnImageChanged.Subscribe(onHitTextBoxShouldChange),
                                         c => c.OnImageChanged.Unsubscribe(onHitTextBoxShouldChange));
            entity.Bind<IAnimationContainer>(c => _animation = c, _animation => _animation = null);
            entity.Bind<IDrawableInfo>(c => { c.OnRenderLayerChanged.Subscribe(onHitTextBoxShouldChange); c.OnIgnoreViewportChanged.Subscribe(onAllViewportsShouldChange); _drawable = c; },
                                       c => { c.OnRenderLayerChanged.Unsubscribe(onHitTextBoxShouldChange); c.OnIgnoreViewportChanged.Unsubscribe(onAllViewportsShouldChange); _drawable = null; });
            
        }

        public AGSBoundingBoxes GetBoundingBoxes(IViewport viewport)
		{
            var viewportBoxes = _boundingBoxes.GetOrAdd(viewport,_ => new ViewportBoundingBoxes(viewport));
            var boundingBoxes = viewportBoxes.BoundingBoxes;
            if (!_isHitTestBoxDirty && !_isCropDirty && !_areViewportsDirty && !viewportBoxes.IsDirty)
                return boundingBoxes;
            var animation = _animation;
			var drawable = _drawable;
			var matrix = _matrix;

			if (animation == null || animation.Animation == null || animation.Animation.Sprite == null ||
                animation.Animation.Sprite.Image == null || drawable == null || matrix == null) return boundingBoxes;

            if (_isHitTestBoxDirty)
            {
                _isCropDirty = true;
                updateHitTestBox(animation, drawable, matrix);
                _isHitTestBoxDirty = false;
            }
            var crop = _crop;

            var layerViewport = _layerViewports.GetViewport(drawable.RenderLayer.Z);
			Size resolution;
			PointF resolutionFactor;
			bool resolutionMatches = AGSModelMatrixComponent.GetVirtualResolution(false, _settings.VirtualResolution,
                                                 drawable, null, out resolutionFactor, out resolution);

            var viewportMatrix = drawable.IgnoreViewport ? Matrix4.Identity : layerViewport.GetMatrix(viewport, drawable.RenderLayer.ParallaxSpeed);
            AGSBoundingBox intermediateBox, hitTestBox;
            if (resolutionMatches)
            {
                intermediateBox = _intermediateBox;
                hitTestBox = _hitTestBox;
            }
            else
            {
				var sprite = animation.Animation.Sprite;
                float width = sprite.Image.Width;
                float height = sprite.Image.Height;
				var modelMatrices = matrix.GetModelMatrices();
                var modelMatrix = modelMatrices.InObjResolutionMatrix;
                intermediateBox = _boundingBoxBuilder.BuildIntermediateBox(width, height, modelMatrix);
                hitTestBox = _boundingBoxBuilder.BuildHitTestBox(intermediateBox);
            }

            PointF scale;
            var renderBox = _boundingBoxBuilder.BuildRenderBox(intermediateBox, viewportMatrix, out scale);

			var cropInfo = renderBox.Crop(BoundingBoxType.Render, crop, resolutionFactor, scale);
			boundingBoxes.PreCropRenderBox = renderBox;
			renderBox = cropInfo.BoundingBox;
            boundingBoxes.RenderBox = renderBox;
            boundingBoxes.TextureBox = cropInfo.TextureBox;
            if (cropInfo.Equals(default(AGSCropInfo)))
            {
                boundingBoxes.HitTestBox = default(AGSBoundingBox);
            }
            else
            {
                hitTestBox = hitTestBox.Crop(BoundingBoxType.HitTest, crop, AGSModelMatrixComponent.NoScaling, scale).BoundingBox;
                boundingBoxes.HitTestBox = hitTestBox;
            }
            _isCropDirty = false;
            _areViewportsDirty = false;
            viewportBoxes.IsDirty = false;

            OnBoundingBoxesChanged.Invoke();
            return boundingBoxes;
		}

        private void updateHitTestBox(IAnimationContainer animation, IDrawableInfo drawable, IModelMatrixComponent matrix)
        {
            var modelMatrices = matrix.GetModelMatrices();
            var modelMatrix = modelMatrices.InVirtualResolutionMatrix;

			Size resolution;
			PointF resolutionFactor;
			bool resolutionMatches = AGSModelMatrixComponent.GetVirtualResolution(false, _settings.VirtualResolution,
												 drawable, null, out resolutionFactor, out resolution);
            
			var sprite = animation.Animation.Sprite;
			float width = sprite.Image.Width / resolutionFactor.X;
			float height = sprite.Image.Height / resolutionFactor.Y;
            _intermediateBox = _boundingBoxBuilder.BuildIntermediateBox(width, height, modelMatrix);
            _hitTestBox = _boundingBoxBuilder.BuildHitTestBox(_intermediateBox);
		}

        private void onHitTextBoxShouldChange()
        {
            _isHitTestBoxDirty = true;
            onAllViewportsShouldChange();
        }

        private void onCropShouldChange()
        {
            _isCropDirty = true;
        }

        private void onAllViewportsShouldChange()
        {
            _areViewportsDirty = true;
        }

		//https://stackoverflow.com/questions/8946790/how-to-use-an-objects-identity-as-key-for-dictionaryk-v
		private class IdentityEqualityComparer<T> : IEqualityComparer<T> where T : class
        {
            public int GetHashCode(T value)
            {
                return RuntimeHelpers.GetHashCode(value);
            }

            public bool Equals(T left, T right)
            {
                return left == right; // Reference identity comparison
            }
        }

        private class ViewportBoundingBoxes
        {
            private IViewport _viewport;

            public ViewportBoundingBoxes(IViewport viewport)
            {
                IsDirty = true;
                _viewport = viewport;
                BoundingBoxes = new AGSBoundingBoxes();
				viewport.OnAngleChanged.Subscribe(onViewportChanged);
				viewport.OnScaleChanged.Subscribe(onViewportChanged);
				viewport.OnPositionChanged.Subscribe(onViewportChanged);
                viewport.OnProjectionBoxChanged.Subscribe(onViewportChanged);
                viewport.OnParentChanged.Subscribe(onViewportChanged);
            }

            public AGSBoundingBoxes BoundingBoxes { get; set; }
            public bool IsDirty { get; set; }

            private void onViewportChanged()
            {
                IsDirty = true;
            }
        }
	}
}