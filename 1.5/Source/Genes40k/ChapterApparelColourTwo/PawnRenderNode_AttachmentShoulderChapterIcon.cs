﻿using UnityEngine;
using Verse;

namespace Genes40k;

public class PawnRenderNode_AttachmentShoulderChapterIcon : PawnRenderNode
{
        
    private Genes40kModSettings modSettings = null;

    private Genes40kModSettings ModSettings => modSettings ??= LoadedModManager.GetMod<Genes40kMod>().GetSettings<Genes40kModSettings>();

    public bool Flipped = true;
    protected override bool FlipGraphic => Flipped;
    
    public PawnRenderNode_AttachmentShoulderChapterIcon(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree) : base(pawn, props, tree)
    {
    }

    public override Graphic GraphicFor(Pawn pawn)
    {
        var leftShoulderIcon = Props.texPath;
            
        var apparelColourTwo = (ChapterBodyDecorativeApparelColourTwo)apparel;
        var drawColour = apparelColourTwo.LeftShoulderIconColour;
            
        if (apparelColourTwo.LeftShoulderIcon != null)
        {
            leftShoulderIcon = apparelColourTwo.LeftShoulderIcon.drawnTextureIconPath;
        }
        else if (pawn.Faction != null && pawn.Faction.IsPlayer && ModSettings.CurrentlySelectedPreset.relatedChapterIcon != null)
        {
            leftShoulderIcon = ModSettings.CurrentlySelectedPreset.relatedChapterIcon.drawnTextureIconPath;
        }

        if (apparelColourTwo.FlipShoulderIcons)
        {
            Flipped = !Flipped;
        }
        
        return GraphicDatabase.Get<Graphic_Multi>(leftShoulderIcon, ShaderFor(pawn), Props.drawSize, drawColour, drawColour);
    }
}