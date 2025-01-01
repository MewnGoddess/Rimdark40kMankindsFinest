﻿using Core40k;
using RimWorld;
using UnityEngine;
using Verse;

namespace Genes40k
{
    public class PawnRenderNodeWorker_AttachmentShoulderPad : PawnRenderNodeWorker_AttachmentBody
    {
        public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
        {
            var pawn = parms.pawn;

            if (parms.Portrait)
            {
                if (parms.facing == Rot4.South || parms.facing == Rot4.North)
                {
                    return false;
                }
            }
            else
            {
                if (pawn.Rotation == Rot4.North || pawn.Rotation == Rot4.South)
                {
                    return false;
                }
                
                if (parms.posture == PawnPosture.Standing)
                {
                    return true;
                }
            
                var mindState = pawn.mindState;
                if (mindState != null && mindState.duty?.def?.drawBodyOverride.HasValue == true)
                {
                    return pawn.mindState.duty.def.drawBodyOverride.Value;
                }
                if (parms.bed != null && parms.pawn.RaceProps.Humanlike)
                {
                    return parms.bed.def.building.bed_showSleeperBody;
                }
            }
            
            return true;
        }

        protected override Graphic GetGraphic(PawnRenderNode node, PawnDrawParms parms)
        {
            var def = node.apparel.def;
            var apparelColourTwo = (ChapterApparelColourTwo)node.apparel;

            return GraphicDatabase.Get<Graphic_Multi>(node.Props.texPath, node.Props.shaderTypeDef.Shader, def.graphicData.drawSize, apparelColourTwo.DrawColor, apparelColourTwo.DrawColorTwo, def.graphicData);
        }
    }
}