using AdvancedKeyBindings.StaticHelpers;
using StardewValley;
using StardewValley.Menus;

namespace AdvancedKeyBindings.Extensions.MenuExtensions
{
    public static class CarpenterMenuExtension
    {
       
        public static bool IsUpgradingPlacementMode(this CarpenterMenu menu)
        {
            var reflector = StaticReflectionHelper.GetInstance().GetReflector();

            return reflector.GetField<bool>(menu, "upgrading").GetValue() && menu.InPlacementMode();
        }
        
        public static bool IsDemolishingPlacementMode(this CarpenterMenu menu)
        {
            var reflector = StaticReflectionHelper.GetInstance().GetReflector();

            return reflector.GetField<bool>(menu, "demolishing").GetValue() && menu.InPlacementMode();
        }
        
        public static bool IsMovingPlacementMode(this CarpenterMenu menu)
        {
            var reflector = StaticReflectionHelper.GetInstance().GetReflector();

            return reflector.GetField<bool>(menu, "moving").GetValue() && menu.InPlacementMode();
        }

        public static bool InPlacementMode(this CarpenterMenu menu)
        {
            var reflector = StaticReflectionHelper.GetInstance().GetReflector();

            return reflector.GetField<bool>(menu, "onFarm").GetValue();
        }
    }
}