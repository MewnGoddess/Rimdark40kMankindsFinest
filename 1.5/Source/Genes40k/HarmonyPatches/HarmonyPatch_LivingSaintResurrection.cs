﻿using HarmonyLib;
using RimWorld;
using Verse;

namespace Genes40k
{
    [HarmonyPatch(typeof(IncidentWorker), "TryExecute")]
    public class LivingSaintResurrection
    {
        public static void Prefix(IncidentWorker __instance)
        {
            var gameComponent = Current.Game.GetComponent<GameComponent_LivingSaint>();
            gameComponent?.TrySpawnSaint(__instance.def.category);
        }    
    }
}