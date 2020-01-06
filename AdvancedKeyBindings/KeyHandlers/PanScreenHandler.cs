using System.Collections.Generic;
using System.Linq;
using AdvancedKeyBindings.Extensions;
using AdvancedKeyBindings.Extensions.MenuExtensions;
using AdvancedKeyBindings.StaticHelpers;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Menus;

namespace AdvancedKeyBindings.KeyHandlers
{
    public class PanScreenHandler: IKeyHandler
    {
        public SButton[] PanScreenScrollLeft { get; }
        public SButton[] PanScreenScrollRight { get; }
        public SButton[] PanScreenScrollUp { get; }
        public SButton[] PanScreenScrollDown { get; }
        public SButton[] PanScreenPreviousBuilding { get; }
        public SButton[] PanScreenNextBuilding { get; }
        private Building _currentBuilding;
        private readonly IReflectionHelper _reflectionHelper;

        public PanScreenHandler(SButton[] panScreenScrollLeft, SButton[] panScreenScrollRight, SButton[] panScreenScrollUp, SButton[] panScreenScrollDown, SButton[] panScreenPreviousBuilding, SButton[] panScreenNextBuilding, IReflectionHelper reflectionHelper)
        {
            PanScreenScrollLeft = panScreenScrollLeft;
            PanScreenScrollRight = panScreenScrollRight;
            PanScreenScrollUp = panScreenScrollUp;
            PanScreenScrollDown = panScreenScrollDown;
            PanScreenPreviousBuilding = panScreenPreviousBuilding;
            PanScreenNextBuilding = panScreenNextBuilding;
            _reflectionHelper = reflectionHelper;
        }

        public bool ReceiveButtonPress(SButton input)
        {

            if (HasSupportedMenu())
            {
                if (HandlePanning(input))
                {
                    return true;
                }
            }

            if (Game1.activeClickableMenu is PurchaseAnimalsMenu menu)
            {
                if (HandleAnimalPlacement(input, menu.GetAnimalBeingPurchased()))

                {
                    return true;
                }
            }

            if (Game1.activeClickableMenu is AnimalQueryMenu animalQueryMenu)
            {
                if (animalQueryMenu.BeingPlaced() && HandleAnimalPlacement(input, animalQueryMenu.GetAnimal()))
                {
                    return true;
                }
            }
            return false;
        }

        private bool HandleAnimalPlacement(SButton input, FarmAnimal animal)
        {
            if (PanScreenPreviousBuilding.Contains(input))
            {
                PreviousBuilding(animal);
                
                _currentBuilding?.PanToBuilding(true, true);
                return true;
            }

            if (PanScreenNextBuilding.Contains(input))
            {
                NextBuilding(animal);
                _currentBuilding?.PanToBuilding(true, true);

                return true;
            }

            return false;
        }

        private bool HasSupportedMenu()
        {
            return Game1.activeClickableMenu is PurchaseAnimalsMenu ||
                Game1.activeClickableMenu is CarpenterMenu;
        }

        private bool HandlePanning(SButton input)
        {
            var panSize = Game1.pixelZoom * 16 * 10;
            if (PanScreenScrollLeft.Contains(input))
            {
                SmoothPanningHelper.GetInstance().RelativePanTo(-panSize, 0);
                return true;
            }
                
            if (PanScreenScrollRight.Contains(input))
            {
                SmoothPanningHelper.GetInstance().RelativePanTo(panSize, 0);
                return true;
            }
                
            if (PanScreenScrollUp.Contains(input))
            {
                SmoothPanningHelper.GetInstance().RelativePanTo(0, -panSize);
                return true;
            }
                
            if (PanScreenScrollDown.Contains(input))
            {
                SmoothPanningHelper.GetInstance().RelativePanTo(0, panSize);
                return true;
            }

            return false;
        }

        private void NextBuilding(FarmAnimal animal)
        {
            var possibleBuildings = animal.GetPossibleHomeBuildings();
            
            if (possibleBuildings.Count == 0)
            {
                _currentBuilding = null;
                return;
            }
            
            if (_currentBuilding == null)
            {
                _currentBuilding = possibleBuildings.First();
            }
            else
            {
                var buildingIndex = possibleBuildings.IndexOf(_currentBuilding);
                buildingIndex++;

                if (buildingIndex >= possibleBuildings.Count)
                {
                    buildingIndex = 0;
                }
                _currentBuilding = possibleBuildings[buildingIndex];
            }
        }
        
        private void PreviousBuilding(FarmAnimal animal)
        {
            var possibleBuildings = animal.GetPossibleHomeBuildings();

            if (possibleBuildings.Count == 0)
            {
                _currentBuilding = null;
                return;
            }
            
            if (_currentBuilding == null)
            {
                _currentBuilding = possibleBuildings.Last();
            }
            else
            {
                var buildingIndex = possibleBuildings.IndexOf(_currentBuilding);
                
                buildingIndex--;

                if (buildingIndex < 0)
                {
                    buildingIndex = possibleBuildings.Count -1;
                }
                _currentBuilding = possibleBuildings[buildingIndex];
            }
        }
    }
}