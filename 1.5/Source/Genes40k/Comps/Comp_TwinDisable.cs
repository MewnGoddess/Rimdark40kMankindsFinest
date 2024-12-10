﻿using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;


namespace Genes40k
{
    [StaticConstructorOnStartup]
    public class Comp_TwinDisable : AbilityComp
    {
        public new CompProperties_AbilityTwinDisable Props => (CompProperties_AbilityTwinDisable)props;

        public override bool GizmoDisabled(out string reason)
        {
            var caster = parent.pawn;
            if (caster.genes != null && caster.genes.HasActiveGene(Genes40kDefOf.BEWH_PrimarchSpecificGeneXX))
            {
                var gene = (Gene_TwinConnected)caster.genes.GetGene(Genes40kDefOf.BEWH_PrimarchSpecificGeneXX);
                if (Props.disableIfDead && gene.Twin.Dead)
                {
                    reason = "BEWH.TwinConnectedDead".Translate();
                    return true;
                }

                if (Props.disableIfOnDifferentMap && caster.Map != gene.Twin.Map)
                {
                    reason = "BEWH.TwinConnectedDifferentMap".Translate();
                    return true;
                }
            }
            
            return base.GizmoDisabled(out reason);
        }
    }
}