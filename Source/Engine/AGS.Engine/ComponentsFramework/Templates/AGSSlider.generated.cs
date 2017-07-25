﻿

//Generation Time: 8/21/2016 4:45:41 PM
//This class was automatically generated by a T4 template.
//Making manual changes in this class might be overridden if the template will be processed again.
//If you want to add functionality you might be able to do this via another partial class definition for this class.

using System;
using AGS.API;
using AGS.Engine;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AGS.Engine
{
    public partial class AGSSlider : AGSEntity, ISlider
    {
        private ISliderComponent _sliderComponent;
        private IUIEvents _uIEvents;
        private ISkinComponent _skinComponent;
        private IHasRoom _hasRoom;
        private IAnimationContainer _animationContainer;
        private IInObjectTree _inObjectTree;
        private ICollider _collider;
        private IVisibleComponent _visibleComponent;
        private IEnabledComponent _enabledComponent;
        private ICustomPropertiesComponent _customProperties;
        private IDrawableInfo _drawableInfo;
        private IHotspotComponent _hotspotComponent;
        private IShaderComponent _shaderComponent;
        private ITranslateComponent _translateComponent;
        private IImageComponent _imageComponent;
        private IScaleComponent _scaleComponent;
        private IRotateComponent _rotateComponent;
        private IPixelPerfectComponent _pixelPerfectComponent;
        private IModelMatrixComponent _modelMatrixComponent;

        public AGSSlider(string id, Resolver resolver, IImage image) : base(id, resolver)
        {            
            _sliderComponent = AddComponent<ISliderComponent>();            
            _uIEvents = AddComponent<IUIEvents>();            
            _skinComponent = AddComponent<ISkinComponent>();            
            _hasRoom = AddComponent<IHasRoom>();            
            _animationContainer = AddComponent<IAnimationContainer>();            
            _inObjectTree = AddComponent<IInObjectTree>();            
            _collider = AddComponent<ICollider>();            
            _visibleComponent = AddComponent<IVisibleComponent>();            
            _enabledComponent = AddComponent<IEnabledComponent>();            
            _customProperties = AddComponent<ICustomPropertiesComponent>();            
            _drawableInfo = AddComponent<IDrawableInfo>();            
            _hotspotComponent = AddComponent<IHotspotComponent>();            
            _shaderComponent = AddComponent<IShaderComponent>();            
            _translateComponent = AddComponent<ITranslateComponent>();            
            _imageComponent = AddComponent<IImageComponent>();            
            _scaleComponent = AddComponent<IScaleComponent>();            
            _rotateComponent = AddComponent<IRotateComponent>();            
            _pixelPerfectComponent = AddComponent<IPixelPerfectComponent>();
            _modelMatrixComponent = AddComponent<IModelMatrixComponent>();
			beforeInitComponents(resolver, image);            
			InitComponents();
            afterInitComponents(resolver, image);            
        }

        public string Name { get { return ID; } }
        public bool AllowMultiple { get { return false; } }
        public void Init(IEntity entity) {}

        public override string ToString()
        {
            return string.Format("{0} ({1})", ID ?? "", GetType().Name);
        }

        partial void beforeInitComponents(Resolver resolver, IImage image);
		partial void afterInitComponents(Resolver resolver, IImage image);

        #region ISliderComponent implementation

        public IObject Graphics 
        {  
            get { return _sliderComponent.Graphics; }  
            set { _sliderComponent.Graphics = value; } 
        }

        public IObject HandleGraphics 
        {  
            get { return _sliderComponent.HandleGraphics; }  
            set { _sliderComponent.HandleGraphics = value; } 
        }

        public ILabel Label 
        {  
            get { return _sliderComponent.Label; }  
            set { _sliderComponent.Label = value; } 
        }

        public Single MinValue 
        {  
            get { return _sliderComponent.MinValue; }  
            set { _sliderComponent.MinValue = value; } 
        }

        public Single MaxValue 
        {  
            get { return _sliderComponent.MaxValue; }  
            set { _sliderComponent.MaxValue = value; } 
        }

        public Single Value 
        {  
            get { return _sliderComponent.Value; }  
            set { _sliderComponent.Value = value; } 
        }

        public Boolean IsHorizontal 
        {  
            get { return _sliderComponent.IsHorizontal; }  
            set { _sliderComponent.IsHorizontal = value; } 
        }

        public IEvent<SliderValueEventArgs> OnValueChanged 
        {  
            get { return _sliderComponent.OnValueChanged; } 
        }

        #endregion

        #region IUIEvents implementation

        public IEvent<MousePositionEventArgs> MouseEnter 
        {  
            get { return _uIEvents.MouseEnter; } 
        }

        public IEvent<MousePositionEventArgs> MouseLeave 
        {  
            get { return _uIEvents.MouseLeave; } 
        }

        public IEvent<MousePositionEventArgs> MouseMove 
        {  
            get { return _uIEvents.MouseMove; } 
        }

        public IEvent<MouseButtonEventArgs> MouseClicked 
        {  
            get { return _uIEvents.MouseClicked; } 
        }

        public IEvent<MouseButtonEventArgs> MouseDoubleClicked 
        {  
            get { return _uIEvents.MouseDoubleClicked; } 
        }

        public IEvent<MouseButtonEventArgs> MouseDown 
        {  
            get { return _uIEvents.MouseDown; } 
        }

        public IEvent<MouseButtonEventArgs> MouseUp 
        {  
            get { return _uIEvents.MouseUp; } 
        }

        public IEvent<MouseButtonEventArgs> LostFocus 
        {  
            get { return _uIEvents.LostFocus; } 
        }

        public Boolean IsMouseIn 
        {  
            get { return _uIEvents.IsMouseIn; } 
        }

        #endregion

        #region ISkinComponent implementation

        public ISkin Skin 
        {  
            get { return _skinComponent.Skin; }  
            set { _skinComponent.Skin = value; } 
        }

        public IConcurrentHashSet<String> SkinTags 
        {  
            get { return _skinComponent.SkinTags; } 
        }

        #endregion

        #region IHasRoom implementation

        public IRoom Room 
        {  
            get { return _hasRoom.Room; } 
        }

        public IRoom PreviousRoom 
        {  
            get { return _hasRoom.PreviousRoom; } 
        }

        public Task ChangeRoomAsync(IRoom room, Nullable<Single> x, Nullable<Single> y)
        {
            return _hasRoom.ChangeRoomAsync(room, x, y);
        }

        #endregion

        #region IAnimationContainer implementation

        public IAnimation Animation 
        {  
            get { return _animationContainer.Animation; } 
        }

        public Boolean DebugDrawAnchor 
        {  
            get { return _animationContainer.DebugDrawAnchor; }  
            set { _animationContainer.DebugDrawAnchor = value; } 
        }

        public IEvent<object> OnAnimationStarted 
        {  
            get { return _animationContainer.OnAnimationStarted; } 
        }

        public IBorderStyle Border 
        {  
            get { return _animationContainer.Border; }  
            set { _animationContainer.Border = value; } 
        }

        public void StartAnimation(IAnimation animation)
        {
            _animationContainer.StartAnimation(animation);
        }

        public AnimationCompletedEventArgs Animate(IAnimation animation)
        {
            return _animationContainer.Animate(animation);
        }

        public Task<AnimationCompletedEventArgs> AnimateAsync(IAnimation animation)
        {
            return _animationContainer.AnimateAsync(animation);
        }

        #endregion

        #region IInObjectTree implementation

        #endregion

        #region IInTree<IObject> implementation

        public ITreeNode<IObject> TreeNode 
        {  
            get { return _inObjectTree.TreeNode; } 
        }

		#endregion

		#region ICollider implementation

		public AGSBoundingBoxes BoundingBoxes
		{
			get { return _collider.BoundingBoxes; }
			set { _collider.BoundingBoxes = value; }
		}

        public Nullable<PointF> CenterPoint 
        {  
            get { return _collider.CenterPoint; } 
        }

        public Boolean CollidesWith(Single x, Single y)
        {
            return _collider.CollidesWith(x, y);
        }

        #endregion

        #region IVisibleComponent implementation

        public Boolean Visible 
        {  
            get { return _visibleComponent.Visible; }  
            set { _visibleComponent.Visible = value; } 
        }

        public Boolean UnderlyingVisible 
        {  
            get { return _visibleComponent.UnderlyingVisible; } 
        }

        public IEvent<object> OnUnderlyingVisibleChanged
        {
            get { return _visibleComponent.OnUnderlyingVisibleChanged; }
        }

        #endregion

        #region IEnabledComponent implementation

        public Boolean Enabled 
        {  
            get { return _enabledComponent.Enabled; }  
            set { _enabledComponent.Enabled = value; } 
        }

        public Boolean UnderlyingEnabled 
        {  
            get { return _enabledComponent.UnderlyingEnabled; } 
        }

        #endregion

        #region ICustomProperties implementation

        public ICustomProperties Properties
        {
            get { return _customProperties.Properties; }
        }

        #endregion

        #region IDrawableInfo implementation

        public IRenderLayer RenderLayer 
        {  
            get { return _drawableInfo.RenderLayer; }  
            set { _drawableInfo.RenderLayer = value; } 
        }

        public Boolean IgnoreViewport 
        {  
            get { return _drawableInfo.IgnoreViewport; }  
            set { _drawableInfo.IgnoreViewport = value; } 
        }

        public Boolean IgnoreScalingArea 
        {  
            get { return _drawableInfo.IgnoreScalingArea; }  
            set { _drawableInfo.IgnoreScalingArea = value; } 
        }

        #endregion

        #region IHotspotComponent implementation

        public IInteractions Interactions 
        {  
            get { return _hotspotComponent.Interactions; } 
        }

        public Nullable<PointF> WalkPoint 
        {  
            get { return _hotspotComponent.WalkPoint; }  
            set { _hotspotComponent.WalkPoint = value; } 
        }

        public String Hotspot 
        {  
            get { return _hotspotComponent.Hotspot; }  
            set { _hotspotComponent.Hotspot = value; } 
        }

        #endregion

        #region IShaderComponent implementation

        public IShader Shader 
        {  
            get { return _shaderComponent.Shader; }  
            set { _shaderComponent.Shader = value; } 
        }

        #endregion

        #region ITranslateComponent implementation

        #endregion

        #region ITranslate implementation

        public ILocation Location 
        {  
            get { return _translateComponent.Location; }  
            set { _translateComponent.Location = value; } 
        }

        public Single X 
        {  
            get { return _translateComponent.X; }  
            set { _translateComponent.X = value; } 
        }

        public Single Y 
        {  
            get { return _translateComponent.Y; }  
            set { _translateComponent.Y = value; } 
        }

        public Single Z 
        {  
            get { return _translateComponent.Z; }  
            set { _translateComponent.Z = value; } 
        }

        #endregion

        #region IImageComponent implementation

        #endregion

        #region IHasImage implementation

        public Byte Opacity 
        {  
            get { return _imageComponent.Opacity; }  
            set { _imageComponent.Opacity = value; } 
        }

        public Color Tint 
        {  
            get { return _imageComponent.Tint; }  
            set { _imageComponent.Tint = value; } 
        }

        public PointF Anchor 
        {  
            get { return _imageComponent.Anchor; }  
            set { _imageComponent.Anchor = value; } 
        }

        public IImage Image 
        {  
            get { return _imageComponent.Image; }  
            set { _imageComponent.Image = value; } 
        }

        public IImageRenderer CustomRenderer 
        {  
            get { return _imageComponent.CustomRenderer; }  
            set { _imageComponent.CustomRenderer = value; } 
        }

        public IEvent<object> OnImageChanged 
        {  
            get { return _imageComponent.OnImageChanged; } 
        }

        #endregion

        #region IScaleComponent implementation

        #endregion

        #region IScale implementation

        public Single Height 
        {  
            get { return _scaleComponent.Height; } 
        }

        public Single Width 
        {  
            get { return _scaleComponent.Width; } 
        }

        public Single ScaleX 
        {  
            get { return _scaleComponent.ScaleX; } 
        }

        public Single ScaleY 
        {  
            get { return _scaleComponent.ScaleY; } 
        }

        public SizeF BaseSize
        {
            get { return _scaleComponent.BaseSize; }
        }

        public void ResetBaseSize(Single initialWidth, Single initialHeight)
        {
            _scaleComponent.ResetBaseSize(initialWidth, initialHeight);
        }

        public void ResetScale()
        {
            _scaleComponent.ResetScale();
        }

        public void ResetScale(Single initialWidth, Single initialHeight)
        {
            _scaleComponent.ResetScale(initialWidth, initialHeight);
        }

        public void ScaleBy(Single scaleX, Single scaleY)
        {
            _scaleComponent.ScaleBy(scaleX, scaleY);
        }

        public void ScaleTo(Single width, Single height)
        {
            _scaleComponent.ScaleTo(width, height);
        }

        public void FlipHorizontally()
        {
            _scaleComponent.FlipHorizontally();
        }

        public void FlipVertically()
        {
            _scaleComponent.FlipVertically();
        }

        #endregion

        #region IRotateComponent implementation

        #endregion

        #region IRotate implementation

        public Single Angle 
        {  
            get { return _rotateComponent.Angle; }  
            set { _rotateComponent.Angle = value; } 
        }

        #endregion

        #region IPixelPerfectComponent implementation

        #endregion

        #region IPixelPerfectCollidable implementation

        public IArea PixelPerfectHitTestArea 
        {  
            get { return _pixelPerfectComponent.PixelPerfectHitTestArea; } 
        }

        public void PixelPerfect(Boolean pixelPerfect)
        {
            _pixelPerfectComponent.PixelPerfect(pixelPerfect);
        }

        #endregion

        public ModelMatrices GetModelMatrices() { return _modelMatrixComponent.GetModelMatrices(); }

        public IEvent<object> OnMatrixChanged { get { return _modelMatrixComponent.OnMatrixChanged; } }
        public IEvent<object> OnIgnoreScalingAreaChanged { get { return _drawableInfo.OnIgnoreScalingAreaChanged; } }
        public IEvent<object> OnRenderLayerChanged { get { return _drawableInfo.OnRenderLayerChanged; } }
        public IEvent<object> OnLocationChanged { get { return _translateComponent.OnLocationChanged; } }
        public IEvent<object> OnScaleChanged { get { return _scaleComponent.OnScaleChanged; } }
        public IEvent<object> OnAnchorChanged { get { return _imageComponent.OnAnchorChanged; } }
        public IEvent<object> OnTintChanged { get { return _imageComponent.OnTintChanged; } }
        public IEvent<object> OnAngleChanged { get { return _rotateComponent.OnAngleChanged; } }
    }
}

