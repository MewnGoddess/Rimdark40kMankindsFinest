﻿using RimWorld;
using Verse;

namespace Genes40k;

public class ThoughtWorker_Pariah : ThoughtWorker
{
    protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
    {
        if (!other.RaceProps.Humanlike)
        {
            return false;
        }
        if (pawn.story.traits.HasTrait(TraitDefOf.Kind))
        {
            return false;
        }

        if (other.genes == null)
        {
            return false;
        }
            
        foreach (var gene in other.genes.GenesListForReading)
        {
            if (!gene.def.HasModExtension<DefModExtension_Pariah>() || !def.HasModExtension<DefModExtension_Pariah>())
            {
                continue;
            }
                
            if (gene.def.GetModExtension<DefModExtension_Pariah>().pariahGene == def.GetModExtension<DefModExtension_Pariah>().pariahGene)
            {
                return !pawn.IsPariah() && !pawn.genes.HasActiveGene(Genes40kDefOf.BEWH_MnemosyneMindshield);
            }
        }
        return false;
    }
}