﻿using RimWorld;
using Verse;

namespace Genes40k;

[StaticConstructorOnStartup]
public class Comp_TwinDisable : AbilityComp
{
    private CompProperties_AbilityTwinDisable Props => (CompProperties_AbilityTwinDisable)props;

    public override bool GizmoDisabled(out string reason)
    {
        var caster = parent.pawn;
        if (caster.genes != null && caster.genes.HasActiveGene(Genes40kDefOf.BEWH_PrimarchSpecificGeneXX))
        {
            var gene = (Gene_TwinConnected)caster.genes.GetGene(Genes40kDefOf.BEWH_PrimarchSpecificGeneXX);
            if (gene.Twin == null)
            {
                reason = "BEWH.MankindsFinest.Ability.NoTwin".Translate();
                return true;
            }
            if (Props.disableIfDead && gene.Twin.Dead)
            {
                reason = "BEWH.MankindsFinest.Ability.TwinConnectedDead".Translate();
                return true;
            }

            if (Props.disableIfOnDifferentMap && caster.Map != gene.Twin.Map)
            {
                reason = "BEWH.MankindsFinest.Ability.TwinConnectedDifferentMap".Translate();
                return true;
            }
        }
            
        return base.GizmoDisabled(out reason);
    }
}