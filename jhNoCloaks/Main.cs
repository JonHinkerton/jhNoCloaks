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

            HarmonyInstance = new Harmony(modEntry.Info.Id);
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            return true;
        }

        [HarmonyPatch(typeof(Character))]
        internal static class Character_Patch
        {
            [HarmonyPatch(nameof(Character.AddEquipmentEntity), [typeof(EquipmentEntity), typeof(bool)])]
            [HarmonyPrefix]
            private static bool AddEquipmentEntity(Character __instance, EquipmentEntity ee)
            {
                if (ee.name.ToString().ToLower().Contains("cape"))
                {
                    log.Log("jhnocloak: removing " + ee.name.ToString());
                    return false;
                }

                return true;
            }

        }

    }
}
