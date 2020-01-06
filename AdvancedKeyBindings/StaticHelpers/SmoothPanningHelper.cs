using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace AdvancedKeyBindings.StaticHelpers
{
    public class SmoothPanningHelper
    {
        private Vector2 _targetCoordinate;
        private Vector2 _sourceCoordinate;
        private bool _currentlyPanning = false;
        private float _panProgress;
        private static SmoothPanningHelper _smoothPanningHelperInstance;
        
        private SmoothPanningHelper(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += GameLoopOnUpdateTicked;
        }

        public void RelativePanTo(int x, int y)
        {
            _targetCoordinate = new Vector2(Game1.viewport.X + x,Game1.viewport.Y + y);
            _sourceCoordinate = new Vector2(Game1.viewport.X, Game1.viewport.Y);
            _panProgress = 0;
            _currentlyPanning = true;
        }
        
        public void AbsolutePanTo(int x, int y)
        {
            _targetCoordinate = new Vector2(x, y);
            _sourceCoordinate = new Vector2(Game1.viewport.X, Game1.viewport.Y);
            _panProgress = 0;
            _currentlyPanning = true;
        }
        
        
        private void GameLoopOnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (_currentlyPanning)
            {
                _panProgress += 0.1f;
                
                if (_panProgress >= 1)
                {
                    _panProgress = 1;
                    _currentlyPanning = false;
                }
                
                var foo = Vector2.SmoothStep(_sourceCoordinate, _targetCoordinate, _panProgress);
               
                
                PanScreen((int) foo.X, (int) foo.Y);

            }
        }

        public static void Initialize(IModHelper modHelper)
        {
            _smoothPanningHelperInstance = new SmoothPanningHelper(modHelper);
        }
        public static SmoothPanningHelper GetInstance()
        {
            if (_smoothPanningHelperInstance == null)
            {
                throw new Exception("The smooth panning class has not been initialized. Use Initialize() first.");
            }
            return _smoothPanningHelperInstance;
        }
        
        private static void PanScreen(int x, int y)
        {
            Game1.previousViewportPosition.X = (float) Game1.viewport.Location.X;
            Game1.previousViewportPosition.Y = (float) Game1.viewport.Location.Y;
            Game1.viewport.X = x;
            Game1.viewport.Y = y;
            Game1.clampViewportToGameMap();
            Game1.updateRaindropPosition();
        }
    }
}