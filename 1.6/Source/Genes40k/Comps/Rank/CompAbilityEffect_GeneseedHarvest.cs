﻿using System;
using System.Linq;
using Core40k;
using RimWorld;
using Verse;

namespace Genes40k;

public class CompAbilityEffect_GeneseedHarvest : CompAbilityEffect
{
    public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
    {
        base.Apply(target, dest);
            
        var corpse = target.Thing as Corpse;
        var pawn = corpse.InnerPawn;

        var progenoidGlands = (Gene_ProgenoidGlands)pawn.genes.GetGene(Genes40kDefOf.BEWH_ProgenoidGlands);
            
        if (!progenoidGlands.HarvestSecondProgenoidGland())
        {
            return;
        }
            
        var random = new Random();

        var caster = parent.pawn;

        var chance = 0;

        if (caster.HasComp<CompRankInfo>())
        {
            chance += caster.GetComp<CompRankInfo>().UnlockedRanks.Where(rank => rank.HasModExtension<DefModExtension_GeneseedHarvest>()).Sum(rank => rank.GetModExtension<DefModExtension_GeneseedHarvest>().chanceOffset);
        }

        chance += caster.equipment.AllEquipmentListForReading.Where(equipment => equipment.def.HasModExtension<DefModExtension_GeneseedHarvest>()).Sum(equipment => equipment.def.GetModExtension<DefModExtension_GeneseedHarvest>().chanceOffset);


        if (random.Next(0, 100) > chance)
        {
            var failLetter = LetterMaker.MakeLetter("BEWH.MankindsFinest.SpaceMarine.GeneseedExtractionFail".Translate(), "BEWH.MankindsFinest.SpaceMarine.GeneseedExtractionFailDesc".Translate(corpse.InnerPawn), LetterDefOf.NegativeEvent, pawn);
            Find.LetterStack.ReceiveLetter(failLetter);
            return;
        }

        Genes40kUtils.MakeGeneseedVial(pawn, pawn.IsPrimaris());
        var successLetter = LetterMaker.MakeLetter("BEWH.MankindsFinest.SpaceMarine.GeneseedExtractionSuccess".Translate(), "BEWH.MankindsFinest.SpaceMarine.GeneseedExtractionSuccessDesc".Translate(corpse.InnerPawn), LetterDefOf.PositiveEvent, pawn);
        Find.LetterStack.ReceiveLetter(successLetter);
    }

    public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
    {
        base.Valid(target, throwMessages);
        if (target.Thing is not Corpse corpse)
        {
            return false;
        }
        if (corpse.InnerPawn.genes == null)
        {
            return false;
        }
        if (corpse.InnerPawn.IsFirstborn() && !Genes40kDefOf.BEWH_GeneseedExtractionFirstborn.IsFinished)
        {
            return false;
        }
        if (corpse.InnerPawn.IsPrimaris() && !Genes40kDefOf.BEWH_GeneseedExtractionPrimaris.IsFinished)
        {
            return false;
        }
        if (!corpse.InnerPawn.genes.HasActiveGene(Genes40kDefOf.BEWH_ProgenoidGlands))
        {
            return false;
        }

        return !((Gene_ProgenoidGlands)corpse.InnerPawn.genes.GetGene(Genes40kDefOf.BEWH_ProgenoidGlands)).SecondProgenoidGlandHarvested;
    }

    public override string ExtraLabelMouseAttachment(LocalTargetInfo target)
    {
        if (target.Thing is not Corpse corpse || corpse.InnerPawn == null)
        {
            return null;
        }
            
        if (corpse.InnerPawn.genes == null)
        {
            return null;
        }
        if (corpse.InnerPawn.IsFirstborn() && !Genes40kDefOf.BEWH_GeneseedExtractionFirstborn.IsFinished)
        {
            return "BEWH.MankindsFinest.SpaceMarine.SMGeneseedExtractionNotResearched".Translate();
        }
        if (corpse.InnerPawn.IsPrimaris() && !Genes40kDefOf.BEWH_GeneseedExtractionPrimaris.IsFinished)
        {
            return "BEWH.MankindsFinest.SpaceMarine.PMGeneseedExtractionNotResearched".Translate();
        }
        if (!corpse.InnerPawn.genes.HasActiveGene(Genes40kDefOf.BEWH_ProgenoidGlands))
        {
            return "BEWH.MankindsFinest.SpaceMarine.NoProgenoidGlands".Translate();
        }
        if (corpse.InnerPawn.genes.GetGene(Genes40kDefOf.BEWH_ProgenoidGlands) is Gene_ProgenoidGlands progenoidGlands && progenoidGlands.SecondProgenoidGlandHarvested)
        {
            return "BEWH.MankindsFinest.SpaceMarine.SecondaryGlandsAlreadyHarvested".Translate();
        }
        
        var caster = parent.pawn;
        var chance = 0;

        if (caster.HasComp<CompRankInfo>())
        {
            chance += caster.GetComp<CompRankInfo>().UnlockedRanks.Where(rank => rank.HasModExtension<DefModExtension_GeneseedHarvest>()).Sum(rank => rank.GetModExtension<DefModExtension_GeneseedHarvest>().chanceOffset);
        }

        chance += caster.equipment.AllEquipmentListForReading.Where(equipment => equipment.def.HasModExtension<DefModExtension_GeneseedHarvest>()).Sum(equipment => equipment.def.GetModExtension<DefModExtension_GeneseedHarvest>().chanceOffset);
            
        return "BEWH.MankindsFinest.SpaceMarine.GeneseedExtractionChance".Translate(chance);
    }

}