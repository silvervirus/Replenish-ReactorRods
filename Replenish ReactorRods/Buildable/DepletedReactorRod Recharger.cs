using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using UnityEngine;

namespace Replenish_ReactorRods.Buildable;

public class DepletedReactorRod_Recharger
{
        public static TechType techType = BuildableDRRRC.Info.TechType;
        public static void Patch()
        {
            BuildableDRRRC.Register();
        }
    

    public static class BuildableDRRRC
    {
        public static Texture2D HorizontalWallLockersTexture = RamuneLib.Utils.ImageUtils.GetTexture("nukedoor");
        public static Texture2D HorizontalWallLockersspec = RamuneLib.Utils.ImageUtils.GetTexture("nukedoor");
       // public static Texture2D HorizontalWallLockersnorm = RamuneLib.Utils.ImageUtils.GetTexture("submarine_locker_01_normal");
        public static Texture2D HorizontalLockersTexture = RamuneLib.Utils.ImageUtils.GetTexture("nukeexport");
        public static Texture2D HorizontalLockersspec = RamuneLib.Utils.ImageUtils.GetTexture("nukeexport");
        public static Texture2D HorizontalLockersnorm = RamuneLib.Utils.ImageUtils.GetTexture("nukenormal");
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("RodRecharger", "Rod Recharger Locker", "Why Throw away DepletedReactorRods when you can Recharge them.", "English")
            .WithIcon(RamuneLib.Utils.ImageUtils.GetSprite("nukelocker"));

        public static void Register()
        {
            CustomPrefab prefab = new CustomPrefab(Info);
            CloneTemplate curtainsClone = new CloneTemplate(Info, TechType.SmallLocker);

            curtainsClone.ModifyPrefab += obj =>
            {   
                var triggerCull = obj.GetComponentInChildren<TriggerCull>();
                GameObject.DestroyImmediate(triggerCull);
                var label = obj.FindChild("Label");
                    GameObject.DestroyImmediate(label);
                var renderer1 = obj.FindChild("model").FindChild("submarine_locker_02").GetComponent<MeshRenderer>();
                foreach (var m in renderer1.materials)
                {
                    m.mainTexture = BuildableDRRRC.HorizontalLockersTexture;
                    m.SetTexture("_SpecTex", HorizontalLockersTexture);
                    m.SetTexture("_Illum", HorizontalLockersTexture);
                   // m.SetTexture("_BumpMap", HorizontalWallLockersnorm);

                }
                var renderer = obj.FindChild("model").FindChild("submarine_locker_02").FindChild("submarine_locker_02_door").GetComponent<MeshRenderer>();
                foreach (var m in renderer.materials)
                {
                    m.mainTexture = BuildableDRRRC.HorizontalWallLockersTexture;
                    m.SetTexture("_SpecTex", HorizontalWallLockersTexture);
                    m.SetTexture("_Illum", HorizontalWallLockersTexture);
                    m.SetTexture("_BumpMap", HorizontalLockersnorm);

                }
                var crafter = obj.AddComponent<ReactorRodCrafter>();
                crafter.lockerStorage = obj.GetComponentInChildren<StorageContainer>();

            };


            prefab.SetUnlock(TechType.ReactorRod);
            prefab.SetGameObject(curtainsClone);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule).SetBuildable();
            prefab.SetRecipe(new RecipeData()
            {
                craftAmount = 0,
                Ingredients = new List<CraftData.Ingredient>()
                {
                    new CraftData.Ingredient(TechType.TitaniumIngot, 3),
                    new CraftData.Ingredient(TechType.ReactorRod, 1)
                },
            });
                
            prefab.Register();

            
        }
    }
}


