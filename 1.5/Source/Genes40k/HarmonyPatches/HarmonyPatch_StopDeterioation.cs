﻿using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Genes40k
{
    [HarmonyPatch(typeof(SteadyEnvironmentEffects), "FinalDeteriorationRate", new Type[]
        {
            typeof(Thing),
            typeof(bool),
            typeof(bool),
            typeof(TerrainDef),
            typeof(List<string>)
        }, new ArgumentType[]
        {
            ArgumentType.Normal,
            ArgumentType.Normal,
            ArgumentType.Normal,
            ArgumentType.Normal,
            ArgumentType.Normal
        })]
    public class StopDeterioationPatch
    {
        public static void Postfix(ref float __result, Thing t, List<string> reasons)
        {
            Comp_DeteriorateOutsideBuilding comp = t.TryGetComp<Comp_DeteriorateOutsideBuilding>();
            if (comp != null && comp.ShouldDeteriorate)
            {
                __result = 0;
                if (!reasons.NullOrEmpty())
                {
                    reasons.Add("BEWH.ItemDeterioratingNotInContainer".Translate());
                }
            }
        }    
    }
}