﻿using System;
using AGS.API;
using AGS.Engine;
using System.Drawing;

namespace DemoGame
{
	public class TopBar
	{
		private const string _baseFolder = "../../Assets/Gui/Buttons/";
		private IPanel _panel;
		private readonly RotatingCursorScheme _scheme;
		private InventoryPanel _invPanel;
		private OptionsPanel _optionsPanel;

		private IInventoryItem _lastInventoryItem;
		private IObject _inventoryItemIcon;
		private IPlayer _player;
		private IGame _game;

		public TopBar(RotatingCursorScheme scheme, InventoryPanel invPanel, OptionsPanel optionsPanel)
		{
			_scheme = scheme;
			_invPanel = invPanel;
			_optionsPanel = optionsPanel;
		}

		public void Load(IGame game)
		{
			_game = game;
			_game.Events.OnSavedGameLoad.Subscribe((sender, e) => onSaveGameLoaded());
			IGameFactory factory = game.Factory;
			_player = game.State.Player;
			_panel = factory.UI.GetPanel("Toolbar", "../../Assets/Gui/DialogBox/toolbar.bmp", 0f, 180f);

			loadButton("Walk Button", factory, "walk/", 0f, RotatingCursorScheme.WALK_MODE);
			loadButton("Interact Button",factory, "hand/", 20f, RotatingCursorScheme.INTERACT_MODE);
			loadButton("Look Button",factory, "eye/", 40f, RotatingCursorScheme.LOOK_MODE);
			loadButton("Talk Button", factory, "talk/", 60f, MouseCursors.TALK_MODE);
			IButton invButton = loadButton("Inventory Button", factory, "inventory/", 80f);
			IButton activeInvButton = loadButton("Active Inventory Button", factory, "activeInventory/", 100f, RotatingCursorScheme.INVENTORY_MODE);
			activeInvButton.Z = 1f;
			loadButton("Help Button", factory, "help/", 280f);
			IButton optionsButton = loadButton("Settings Button", factory, "settings/", 300f);

			invButton.OnMouseClick(() => _invPanel.Show(), _game);
			optionsButton.OnMouseClick(() => _optionsPanel.Show(), _game);

			game.Events.OnRepeatedlyExecute.Subscribe(onRepeatedlyExecute);
			_inventoryItemIcon = factory.Object.GetObject("Inventory Item Icon");
			_inventoryItemIcon.X = 10f;
			_inventoryItemIcon.Y = 10f;
			_inventoryItemIcon.Anchor = new AGS.API.PointF (0f, 0f);
			_inventoryItemIcon.TreeNode.SetParent(activeInvButton.TreeNode);
			_inventoryItemIcon.RenderLayer = _panel.RenderLayer;
			game.State.UI.Add(_inventoryItemIcon);
		}

		private void onSaveGameLoaded()
		{
			_panel = _game.Find<IPanel>(_panel.ID);
			_inventoryItemIcon = _game.Find<IObject>(_inventoryItemIcon.ID);
			_player = _game.State.Player;
			_lastInventoryItem = null;
		}

		private IButton loadButton(string id, IGameFactory factory, string folder, float x, string mode = null)
		{
			folder = _baseFolder + folder;
			IButton button = factory.UI.GetButton(id, folder + "normal.bmp", folder + "hovered.bmp", folder + "pushed.bmp", x, 0f);
			button.TreeNode.SetParent(_panel.TreeNode);
			if (mode != null)
			{
				button.OnMouseClick(() => _scheme.CurrentMode = mode, _game);
			}
			return button;
		}

		private void onRepeatedlyExecute(object sender, AGSEventArgs args)
		{
			if (_lastInventoryItem == _player.Character.Inventory.ActiveItem) return;

			_lastInventoryItem = _player.Character.Inventory.ActiveItem;

			if (_lastInventoryItem != null)
			{
				_inventoryItemIcon.Image = _lastInventoryItem.CursorGraphics.Image;
				_inventoryItemIcon.Animation.Sprite.Anchor = new AGS.API.PointF (0.5f, 0.5f);
				_inventoryItemIcon.ScaleTo(15f, 15f);
			}
			_inventoryItemIcon.Visible = (_lastInventoryItem != null);
		}
	}
}

