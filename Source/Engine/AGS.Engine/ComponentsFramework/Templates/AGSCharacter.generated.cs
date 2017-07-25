﻿

//Generation Time: 8/16/2016 10:16:11 PM
//This class was automatically generated by a T4 template.
//Making manual changes in this class might be overridden if the template will be processed again.
//If you want to add functionality you might be able to do this via another partial class definition for this class.

using System;
using AGS.API;
using AGS.Engine;
using System.Threading.Tasks;
using System.Collections.Generic;
using Autofac;

namespace AGS.Engine
{
    public partial class AGSCharacter : AGSEntity, ICharacter
    {
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
        private ISayBehavior _sayBehavior;
        private IWalkBehavior _walkBehavior;
        private IFaceDirectionBehavior _faceDirectionBehavior;
        private IHasOutfit _hasOutfit;
        private IHasInventory _hasInventory;
        private IFollowBehavior _followBehavior;
        private IModelMatrixComponent _modelMatrixComponent;

        public AGSCharacter(string id, Resolver resolver, IOutfit outfit) : base(id, resolver)
        {            
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
            _faceDirectionBehavior = AddComponent<IFaceDirectionBehavior>();            
            _hasOutfit = AddComponent<IHasOutfit>();            
            _hasInventory = AddComponent<IHasInventory>();            
            _followBehavior = AddComponent<IFollowBehavior>();
            _modelMatrixComponent = AddComponent<IModelMatrixComponent>();
			beforeInitComponents(resolver, outfit);            
			InitComponents();
            afterInitComponents(resolver, outfit);            
        }

        public string Name { get { return ID; } }
        public bool AllowMultiple { get { return false; } }
        public void Init(IEntity entity) {}

        public override string ToString()
        {
            return string.Format("{0} ({1})", ID ?? "", GetType().Name);
        }

        partial void beforeInitComponents(Resolver resolver, IOutfit outfit);
		partial void afterInitComponents(Resolver resolver, IOutfit outfit);

        #region IHasRoom implementation

        public IRoom Room 
        {  
            get { return _hasRoom.Room; } 
        }

        public IRoom PreviousRoom 
        {  
            get { return _hasRoom.PreviousRoom; } 
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

        #region ITransformComponent implementation

        #endregion

        #region ITransform implementation

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

        #region ISayBehavior implementation

        public ISayConfig SpeechConfig 
        {  
            get { return _sayBehavior.SpeechConfig; } 
        }

        public IBlockingEvent<BeforeSayEventArgs> OnBeforeSay 
        {  
            get { return _sayBehavior.OnBeforeSay; } 
        }

        public void Say(String text)
        {
            _sayBehavior.Say(text);
        }

        public Task SayAsync(String text)
        {
            return _sayBehavior.SayAsync(text);
        }

        #endregion

        #region IWalkBehavior implementation

        public PointF WalkStep 
        {  
            get { return _walkBehavior.WalkStep; }  
            set { _walkBehavior.WalkStep = value; } 
        }

        public Boolean AdjustWalkSpeedToScaleArea 
        {  
            get { return _walkBehavior.AdjustWalkSpeedToScaleArea; }  
            set { _walkBehavior.AdjustWalkSpeedToScaleArea = value; } 
        }

        public Boolean MovementLinkedToAnimation
        {
            get { return _walkBehavior.MovementLinkedToAnimation; }
            set { _walkBehavior.MovementLinkedToAnimation = value; }
        }

        public Boolean IsWalking 
        {  
            get { return _walkBehavior.IsWalking; } 
        }

        public ILocation WalkDestination
        { 
            get { return _walkBehavior.WalkDestination; }
        }

        public Boolean DebugDrawWalkPath 
        {  
            get { return _walkBehavior.DebugDrawWalkPath; }  
            set { _walkBehavior.DebugDrawWalkPath = value; } 
        }

        public Task<Boolean> WalkAsync(ILocation location)
        {
            return _walkBehavior.WalkAsync(location);
        }

        public void StopWalking()
        {
            _walkBehavior.StopWalking();
        }

        public Task StopWalkingAsync()
        {
            return _walkBehavior.StopWalkingAsync();
        }

        public void PlaceOnWalkableArea()
        {
            _walkBehavior.PlaceOnWalkableArea();
        }

        #endregion

        #region IFaceDirectionBehavior implementation

        public Direction Direction 
        {  
            get { return _faceDirectionBehavior.Direction; } 
        }

        public IDirectionalAnimation CurrentDirectionalAnimation 
        {  
            get { return _faceDirectionBehavior.CurrentDirectionalAnimation; }  
            set { _faceDirectionBehavior.CurrentDirectionalAnimation = value; } 
        }

        public void FaceDirection(Direction direction)
        {
            _faceDirectionBehavior.FaceDirection(direction);
        }

        public Task FaceDirectionAsync(Direction direction)
        {
            return _faceDirectionBehavior.FaceDirectionAsync(direction);
        }

        public void FaceDirection(IObject obj)
        {
            _faceDirectionBehavior.FaceDirection(obj);
        }

        public Task FaceDirectionAsync(IObject obj)
        {
            return _faceDirectionBehavior.FaceDirectionAsync(obj);
        }

        public void FaceDirection(Single x, Single y)
        {
            _faceDirectionBehavior.FaceDirection(x, y);
        }

        public Task FaceDirectionAsync(Single x, Single y)
        {
            return _faceDirectionBehavior.FaceDirectionAsync(x, y);
        }

        public void FaceDirection(Single fromX, Single fromY, Single toX, Single toY)
        {
            _faceDirectionBehavior.FaceDirection(fromX, fromY, toX, toY);
        }

        public Task FaceDirectionAsync(Single fromX, Single fromY, Single toX, Single toY)
        {
            return _faceDirectionBehavior.FaceDirectionAsync(fromX, fromY, toX, toY);
        }

        #endregion

        #region IHasOutfit implementation

        public IOutfit Outfit 
        {  
            get { return _hasOutfit.Outfit; }  
            set { _hasOutfit.Outfit = value; } 
        }

        #endregion

        #region IHasInventory implementation

        public IInventory Inventory 
        {  
            get { return _hasInventory.Inventory; }  
            set { _hasInventory.Inventory = value; } 
        }

        #endregion

        #region IFollowBehavior implementation

        public void Follow(IObject obj, IFollowSettings settings)
        {
            _followBehavior.Follow(obj, settings);
        }

        public IObject TargetBeingFollowed
        {
            get { return _followBehavior.TargetBeingFollowed; }
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

