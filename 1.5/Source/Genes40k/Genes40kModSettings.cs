﻿using UnityEngine;
using Verse;

namespace Genes40k
{
    public class Genes40kModSettings : ModSettings
    {
        public bool psychicPhenomena = true;
        public bool psykerPariahBirth = true;
        public int psykerPariahBirthChance = 10;
        
        public bool perpetualBirth = true;
        public int perpetualBirthChance = 3;

        public int livingSaintBigThreat = 65;
        public int livingSaintSmallThreat = 35;
        
        public int implantationSuccessOffset = 0;
        public int implantationCapOffset = 0;
        
        private ChapterColourDef currentlySelectedPreset = null;
        
        public ChapterColourDef CurrentlySelectedPreset
        {
            get => currentlySelectedPreset ?? (currentlySelectedPreset = CustomPreset);
            set => currentlySelectedPreset = value;
        }

        private ChapterColourDef customPreset = null;
        public ChapterColourDef CustomPreset =>
            customPreset ?? (customPreset = new ChapterColourDef
            {
                defName = "BEWH_CustomChapterDef",
                label = "Custom",
                primaryColour = chapterColorOne,
                secondaryColour = chapterColorTwo,
                relatedChapterIcon = chapterShoulderIcon,
            });

        public Color chapterColorOne = Color.black;
        public Color chapterColorTwo = Color.red;
        public ShoulderIconDef chapterShoulderIcon = Genes40kDefOf.BEWH_ShoulderNone;
        
        public bool useChaosVersion = false;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref psychicPhenomena, "psychicPhenomena", true);
            Scribe_Values.Look(ref psykerPariahBirth, "psykerPariahBirth", true);
            Scribe_Values.Look(ref psykerPariahBirthChance, "psykerPariahBirthChance", 10);
            
            Scribe_Values.Look(ref perpetualBirth, "perpetualBirth", true);
            Scribe_Values.Look(ref perpetualBirthChance, "perpetualBirthChance", 3);
            
            Scribe_Values.Look(ref livingSaintBigThreat, "livingSaintBigThreat", 65);
            Scribe_Values.Look(ref livingSaintSmallThreat, "livingSaintSmallThreat", 35);
            
            Scribe_Values.Look(ref implantationSuccessOffset, "implantationSuccessOffset", 0);
            Scribe_Values.Look(ref implantationCapOffset, "implantationCapOffset", 0);
            
            Scribe_Values.Look(ref chapterColorOne, "chapterColorOne", Color.black);
            Scribe_Values.Look(ref chapterColorTwo, "chapterColorTwo", Color.red);
            Scribe_Defs.Look(ref chapterShoulderIcon, "chapterShoulderIcon");
            
            Scribe_Values.Look(ref useChaosVersion, "useChaosVersion", false);
            
            if (Scribe.mode == LoadSaveMode.Saving)
            {
                if (currentlySelectedPreset == CustomPreset)
                {
                    currentlySelectedPreset = null;
                }
            }
            
            Scribe_Defs.Look(ref currentlySelectedPreset, "currentlySelectedPreset");
            
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                if (currentlySelectedPreset != null)
                {
                    return;
                }
                currentlySelectedPreset = CustomPreset;
            }
        }
    }
}