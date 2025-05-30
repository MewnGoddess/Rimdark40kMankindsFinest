﻿using RimWorld;
using Verse;

namespace Genes40k;

public class Gene_LivingSaint : Gene
{
    public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
    {
        base.Notify_PawnDied(dinfo, culprit);
        if (pawn.Faction != Faction.OfPlayer)
        {
            return;
        }
            
        pawn.Corpse.DeSpawn();
    }
}