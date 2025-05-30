﻿using System;
using RimWorld;
using Verse;

namespace Genes40k;

public class CompAbilityEffect_StasisConfirmation : CompAbilityEffect
{
    public override Window ConfirmationDialog(LocalTargetInfo target, Action confirmAction)
    {
        var pawn = target.Pawn;
        if (pawn == null)
        {
            return null;
        }

        var stasisResearch = Genes40kDefOf.BEWH_StasisResurrection;

        return stasisResearch.IsFinished ? null : Dialog_MessageBox.CreateConfirmation("BEWH.MankindsFinest.Ability.StasisResearchNotCompleted".Translate(pawn.Named("PAWN"), stasisResearch.label), confirmAction, destructive: true);
    }
}