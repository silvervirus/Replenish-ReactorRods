using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using UnityEngine;
using PrefabUtils = Nautilus.Utility.PrefabUtils;

namespace Replenish_ReactorRods
{
    
    public class RRR
    {
       public static TechType techType;
        public static void Patch()
        {
            RRRod.Register();
        }
    }

    public static class RRRod
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("ReplenishReactorRod", "Recharged Reactor Rod", "Takes Depleted Reactor Rod and makes them new again.")
            // set the icon to that of the vanilla locker:
            .WithIcon(RamuneLib.Utils.ImageUtils.GetSprite("reactorrod"));

        public static void Register()
        {
            // create prefab:
            CustomPrefab prefab = new CustomPrefab(Info);

            // copy the model of a vanilla wreck piece (which looks like a taller locker):
            CloneTemplate lockerClone = new CloneTemplate(Info, TechType.ReactorRod);

            // modify the cloned model:
            lockerClone.ModifyPrefab += obj =>
            {
                
                
            };
            prefab.SetUnlock(TechType.ReactorRod);
            // assign the created clone model to the prefab itself:
            prefab.SetGameObject(lockerClone);

            // assign it to the correct tab in the builder tool:
            prefab.SetPdaGroupCategory(TechGroup.Resources, TechCategory.AdvancedMaterials);

            // set recipe:
            prefab.SetRecipeFromJson(RamuneLib.Utils.JsonUtils.GetJsonRecipe("RRRod"))
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Resources","Electronics");

            // finally, register it into the game:
            prefab.Register();

            TechType techType = Info.TechType;
            
        }
    }
}
