using HarmonyLib;
using Kingmaker.ResourceLinks;
using Kingmaker.View.Equipment;
using Kingmaker.Visual.CharacterSystem;
using System.Collections.Generic;
using System.Reflection;
using UnityModManagerNet;

namespace jhNoCloaks
{
    public static class Main
    {
        internal static Harmony HarmonyInstance;
        internal static UnityModManager.ModEntry.ModLogger log;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            log = modEntry.Logger;

            modEntry.OnGUI = OnGUI;
            HarmonyInstance = new Harmony(modEntry.Info.Id);
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            return true;
        }

        public static void OnGUI(UnityModManager.ModEntry modEntry)
        {

        }

        [HarmonyPatch(typeof(Character))]
        internal static class Character_Patch
        {
            [HarmonyPatch(nameof(Character.AddEquipmentEntity), [typeof(EquipmentEntity), typeof(bool)])]
            [HarmonyPrefix]
            private static bool AddEquipmentEntity(Character __instance, EquipmentEntity ee)
            {
                if (ee.name.ToString().ToLower().Contains("cape") || ee.name.ToString().ToLower().Contains("medi"))
                {
                    log.Log("jhnocloak: removing " + ee.name.ToString());
                    return false;
                }

                return true;
            }

            [HarmonyPatch(nameof(Character.AddEquipmentEntities), [typeof(IEnumerable<EquipmentEntityLink>), typeof(bool)])]
            [HarmonyPrefix]
            private static bool AddEquipmentEntities(Character __instance, IEnumerable<EquipmentEntityLink> ees, bool saved = false)
            {
                foreach (var e in ees)
                {
                    var eid = e.AssetId;


                    
                    //if (e..name.ToString().ToLower().Contains("medi"))
                    //{
                    //    log.Log("jhnocloak: removing " + ee.name.ToString());
                    //    return false;
                    //}
                }

                return true;
            }

        }

    }
}
