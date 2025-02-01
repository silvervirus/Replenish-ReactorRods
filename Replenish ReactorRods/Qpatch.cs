using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using Replenish_ReactorRods.Buildable;
using static CraftData;


using UnityEngine;

namespace Replenish_ReactorRods
{
	[BepInPlugin(Qpatch.PLUGIN_GUID, Qpatch.PLUGIN_NAME, Qpatch.PLUGIN_VERSION)]

	public class Qpatch : BaseUnityPlugin
	{
		public const String PLUGIN_GUID = "SN.ReplenishReactorRods";
		public const String PLUGIN_NAME = "SNReplenishReactorRods";
		public const String PLUGIN_VERSION = "1.0.1";
		
		public static string ModPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		public static Atlas.Sprite GetSprite(string name)
		{
			return ImageUtils.LoadSpriteFromFile(Path.Combine($"{ModPath}/Assets/{name}.png"));
		}
		public void Start()
		{
			Replenish_ReactorRods.RRR.Patch();
			DepletedReactorRod_Recharger.Patch();
			Console.WriteLine("[ReplenishReactorRods] Successfully patched.");
		}
	}
}
