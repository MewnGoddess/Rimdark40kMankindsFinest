﻿using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Genes40k;

public class WorkGiver_FillPrimarchVatWithNutrition : WorkGiver_Scanner
{

    private const float NutritionBuffer = 2.5f;

    public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(Genes40kDefOf.BEWH_PrimarchGrowthVat);

    public override PathEndMode PathEndMode => PathEndMode.Touch;

    public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t is not Building_PrimarchGrowthVat building_GrowthVat)
        {
            return false;
        }
        if (t.IsForbidden(pawn) || !pawn.CanReserve(t, 1, -1, null, forced))
        {
            return false;
        }
        if (pawn.Map.designationManager.DesignationOn(t, DesignationDefOf.Deconstruct) != null)
        {
            return false;
        }
        if (t.IsBurning())
        {
            return false;
        }
        if (building_GrowthVat.NutritionNeeded > NutritionBuffer)
        {
            if (FindNutrition(pawn, building_GrowthVat).Thing != null)
            {
                return true;
            }
                
            JobFailReason.Is("BEWH.MankindsFinest.PrimarchGrowthVat.NoSlurry".Translate());
            return false;
        }

        return false;
    }

    public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t is not Building_PrimarchGrowthVat building_GrowthVat)
        {
            return null;
        }
        if (building_GrowthVat.NutritionNeeded > NutritionBuffer)
        {
            var thingCount = FindNutrition(pawn, building_GrowthVat);
            if (thingCount.Thing != null)
            {
                var job = HaulAIUtility.HaulToContainerJob(pawn, thingCount.Thing, t);
                job.count = Mathf.Min(job.count, thingCount.Count);
                return job;
            }
        }

        return null;
    }

    private static ThingCount FindNutrition(Pawn pawn, Building_PrimarchGrowthVat vat)
    {
        var thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForDef(Genes40kDefOf.BEWH_RawGestationalSlurry), PathEndMode.ClosestTouch, TraverseParms.For(pawn), 9999f, Validator);
        if (thing == null)
        {
            return default;
        }
        var b = Mathf.CeilToInt(vat.NutritionNeeded / thing.GetStatValue(StatDefOf.Nutrition));
        
        return new ThingCount(thing, Mathf.Min(thing.stackCount, b));
        bool Validator(Thing x)
        {
            if (x.IsForbidden(pawn) || !pawn.CanReserve(x))
            {
                return false;
            }
            if (!vat.CanAcceptNutrition(x))
            {
                return false;
            }
                
            return !(x.def.GetStatValueAbstract(StatDefOf.Nutrition) > vat.NutritionNeeded);
        }
    }
}