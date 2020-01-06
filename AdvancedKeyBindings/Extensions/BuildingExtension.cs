using AdvancedKeyBindings.StaticHelpers;
using StardewValley;
using StardewValley.Buildings;

namespace AdvancedKeyBindings.Extensions
{
    public static class BuildingExtension
    {
        /// <summary>
        /// Pans the viewport to the building.
        /// </summary>
        /// <param name="building">The building</param>
        /// <param name="centerMouse">True to center the mouse at the building</param>
        /// <param name="playSound">True to play a selection sound</param>
        public static void PanToBuilding(this Building building, bool centerMouse = false, bool playSound = false)
        {
            var centerX = (building.tilesWide * 64) / 2;
            var centerY = (building.tilesHigh * 64) / 2;
            
            SmoothPanningHelper.GetInstance().AbsolutePanTo(
                building.tileX * 64 - (Game1.viewport.Width / 2) + centerX,
                building.tileY * 64 - (Game1.viewport.Height / 2) + centerY);

            if (centerMouse)
            {
                Game1.setMousePosition((int) ((float)Game1.viewport.Width / 2 * Game1.options.zoomLevel),
                    (int) ((float)Game1.viewport.Height / 2 * Game1.options.zoomLevel));
            }

            if (playSound)
            {
                Game1.playSound("smallSelect");
            }
        }
    }
}