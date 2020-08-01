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
        static Patches()
        {
            Harmony harmony = new Harmony("rimworld.maxzicode.searchpanel.mainconstructor");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

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
        private class Patch_DoPlaySettingsGlobalControls
        {
            private static void Postfix(WidgetRow row, bool worldView)
            {
                var model = MainMVC.SeekModel;
                var controller = MainMVC.MainWindowController;

                if (!worldView)
                {
                    bool isSelected = controller.IsWindowOpened;
                    row.ToggleableIcon(ref isSelected, Textures.SearchToolIcon, "ZiT_ObjectsSeekerLabel".Translate(), SoundDefOf.Mouseover_ButtonToggle);
                    if (isSelected != controller.IsWindowOpened)
                    {
                        model.Initialize();
                        controller.ToggleWindow();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(SelectionDrawer), nameof(SelectionDrawer.DrawSelectionOverlays), MethodType.Normal)]
        private class Patch_DrawSelectionOverlays
        {
            private static void Postfix()
            {
                var locations = MainMVC.SeekModel.ActiveSearchItem.Cells;
                if (locations != null)
                    GenDraw.DrawFieldEdges(locations, Color.magenta);
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
    }
}