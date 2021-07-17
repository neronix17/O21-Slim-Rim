using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace O21HDPawns
{
    public class Comp_SlimRim : ThingComp
    {
        public CompProperties_SlimRim Props => (CompProperties_SlimRim)props;

        public bool completed = false;

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(ref completed, "completed", false);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            if (!completed)
            {
                CorrectBodyNow();
            }
        }

        public void CorrectBodyNow()
        {
            Pawn pawn = this.parent as Pawn;

            if(HDPawnsMod.settings.slimRim_fat && pawn.story.bodyType == BodyTypeDefOf.Fat)
            {
                MakeNormalBody(pawn);
            }
            if (HDPawnsMod.settings.slimRim_hulk && pawn.story.bodyType == BodyTypeDefOf.Hulk)
            {
                MakeNormalBody(pawn);
            }
            if (HDPawnsMod.settings.slimRim_thin && pawn.story.bodyType == BodyTypeDefOf.Thin)
            {
                MakeNormalBody(pawn);
            }

            completed = true;
        }

        public void MakeNormalBody(Pawn pawn)
        {
            if (pawn.gender == Gender.Male)
            {
                pawn.story.bodyType = BodyTypeDefOf.Male;
            }
            else if (pawn.gender == Gender.Female)
            {
                pawn.story.bodyType = BodyTypeDefOf.Female;
            }
        }
    }
}
