﻿using Core40k;
using RimWorld;
using UnityEngine;
using Verse;

namespace Genes40k
{
    public class PawnRenderNodeWorker_AttachmentShoulderPad : PawnRenderNodeWorker_AttachmentBody
    {
        private Graphic cachedShoulderGraphic = null;

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
            var apparelColourTwo = (ApparelColourTwo)node.apparel;
            
            const string shoulderPath = "Things/Armor/Imperium/PowerArmor/Shoulder/BEWH_ImperiumPowerArmor_Shoulder";
            var shader = ShaderDatabase.CutoutComplex;
                    
            if (def.graphicData.shaderType != null)
            {
                shader = def.graphicData.shaderType.Shader;
            }
            cachedShoulderGraphic = GraphicDatabase.Get<Graphic_Multi>(shoulderPath, shader, def.graphicData.drawSize, apparelColourTwo.DrawColor, apparelColourTwo.DrawColorTwo, def.graphicData);
            
            return cachedShoulderGraphic;
        }
    }
}