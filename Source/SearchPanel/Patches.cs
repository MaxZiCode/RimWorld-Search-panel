using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

using Verse;
using Verse.Profile;
using RimWorld;
using HarmonyLib;
using UnityEngine;

namespace SearchPanel
{
	[StaticConstructorOnStartup]
	public static class Patches
	{
		private static bool s_hasModelInitialized;

		public static readonly SeekModel SeekModel = new SeekModel();

		public static readonly WindowController WindowController = new WindowController(SeekModel);

		static Patches()
		{
			Harmony harmony = new Harmony("rimworld.maxzicode.searchpanel.mainconstructor");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}

		#region Patches
		//[HarmonyPatch(typeof(Game), "CurrentMap", MethodType.Setter)]
		//class Patch_CurrentMap
		//{
		//	static void Postfix()
		//	{
		//		if (Find.WindowStack.IsOpen(typeof(ObjectSeeker_Window)) && Current.ProgramState == ProgramState.Playing)
		//		{
		//			((ObjectSeeker_Window)Find.WindowStack.Windows.First(w => w is ObjectSeeker_Window)).ODB.Update();
		//		}
		//	}
		//}

		[HarmonyPatch(typeof(PlaySettings), "DoPlaySettingsGlobalControls", MethodType.Normal)]
		class Patch_DoPlaySettingsGlobalControls
		{
			static void Postfix(WidgetRow row, bool worldView)
			{
				if (!worldView)
				{
					bool isSelected = WindowController.IsWindowOpened;
					row.ToggleableIcon(ref isSelected, ContentFinder<Texture2D>.Get("UI/Lupa(not Pupa)", true), "ZiT_ObjectsSeekerLabel".Translate(), SoundDefOf.Mouseover_ButtonToggle);
					if (isSelected != WindowController.IsWindowOpened)
					{
						if (!s_hasModelInitialized)
						{
							SeekModel.Initialize();
							s_hasModelInitialized = true;
						}
						WindowController.ToggleWindow();
					}
				}
			}
		}

		//[HarmonyPatch(typeof(MemoryUtility), "ClearAllMapsAndWorld", MethodType.Normal)]
		//class Patch_ClearAllMapsAndWorld
		//{
		//	static void Postfix()
		//	{
		//		ObjectsDatabase.ClearUpdateAction();
		//	}
		//}
		#endregion Patches
	}
}
