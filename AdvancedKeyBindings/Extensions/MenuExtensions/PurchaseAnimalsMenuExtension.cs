using AdvancedKeyBindings.StaticHelpers;
using StardewValley;
using StardewValley.Menus;

namespace AdvancedKeyBindings.Extensions.MenuExtensions
{
    public static class PurchaseAnimalsMenuExtension
    {
        /// <summary>
        /// Returns the animal currently being purchased
        /// </summary>
        /// <param name="menu"></param>
        /// <returns>The animal</returns>
        public static FarmAnimal GetAnimalBeingPurchased(this PurchaseAnimalsMenu menu)
        {
            var reflector = StaticReflectionHelper.GetInstance().GetReflector();

            return reflector.GetField<FarmAnimal>(menu, "animalBeingPurchased").GetValue();
        }
    }
}