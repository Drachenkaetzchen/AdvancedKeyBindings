﻿using AdvancedKeyBindings.Config;
using AdvancedKeyBindings.KeyHandlers;
using AdvancedKeyBindings.StaticHelpers;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace AdvancedKeyBindings
{
    public class ModEntry: StardewModdingAPI.Mod
    {
        /// <summary>The mod configuration.</summary>
        private ModConfig Config;

        /// <summary>The configured key bindings.</summary>
        private ModConfigKeys Keys;

        private IKeyHandler[] _keyHandlers;

        private IInputHelper _inputHelper;

        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();
            Keys = Config.Controls.ParseControls(Monitor);
            
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            
            SmoothPanningHelper.Initialize(helper);
            StaticReflectionHelper.Initialize(helper);

            _inputHelper = helper.Input;
            _keyHandlers = new IKeyHandler[]
            {
                new ChestKeyHandler(Keys.AddToExistingStacks),
                new PanScreenHandler(Keys.PanScreenScrollLeft, Keys.PanScreenScrollRight, Keys.PanScreenScrollUp, Keys.PanScreenScrollDown, Keys.PanScreenPreviousBuilding,
                    Keys.PanScreenNextBuilding, helper.Reflection)
            };
        }

       

        /// <summary>The method invoked when the player presses an input button.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            foreach (var handler in _keyHandlers)
            {
                if (handler.ReceiveButtonPress(e.Button))
                {
                    _inputHelper.Suppress(e.Button);
                    break;
                }
            }
            
        }
        
        
    }
}