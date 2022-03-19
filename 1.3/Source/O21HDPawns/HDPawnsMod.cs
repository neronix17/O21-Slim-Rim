using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using O21Toolbox;

using HarmonyLib;

namespace O21HDPawns
{
    public class HDPawnsMod : Mod
    {
        public static HDPawnsSettings settings;
        public static HDPawnsMod mod;

        public HDPawnsMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<HDPawnsSettings>();
            mod = this;

            new Harmony("neronix17.slimrim.rimworld").PatchAll();
        }

        public override string SettingsCategory() => "Slim Rim";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxEnhanced("Fat", "Slim the rim with a lack of fat.", ref settings.slimRim_fat);
            listing.CheckboxEnhanced("Hulk", "Hulk? More like bulk, get rid of it.", ref settings.slimRim_hulk);
            listing.CheckboxEnhanced("Thin", "Ok maybe that's a little too slim...", ref settings.slimRim_thin);
            listing.End();

            base.DoSettingsWindowContents(inRect);
        }
    }

    public class HDPawnsSettings : ModSettings
    {
        //public bool slimRim = true;
        public bool slimRim_fat = true;
        public bool slimRim_hulk = false;
        public bool slimRim_thin = false;


        public override void ExposeData()
        {
            base.ExposeData();

            //Scribe_Values.Look(ref slimRim, "slimRim", true);
            Scribe_Values.Look(ref slimRim_fat, "slimRim_fat", true);
            Scribe_Values.Look(ref slimRim_hulk, "slimRim_hulk", false);
            Scribe_Values.Look(ref slimRim_thin, "slimRim_thin", false);
        }

        public IEnumerable<string> GetEnabledSettings
        {
            get
            {
                return GetType().GetFields().Where(p => p.FieldType == typeof(bool) && (bool)p.GetValue(this)).Select(p => p.Name);
            }
        }
    }

    [HarmonyPatch(typeof(PawnGenerator), "GenerateBodyType")]
    public class PawnGenerator__GenerateBodyType
    {

        [HarmonyPostfix]
        public static void Postfix(Pawn pawn)
        {

            if ((pawn.story.bodyType == BodyTypeDefOf.Thin && HDPawnsMod.settings.slimRim_thin) ||
                (pawn.story.bodyType == BodyTypeDefOf.Fat && HDPawnsMod.settings.slimRim_fat) ||
                (pawn.story.bodyType == BodyTypeDefOf.Hulk && HDPawnsMod.settings.slimRim_hulk))
            {
                pawn.story.bodyType = (pawn.gender == Gender.Female) ? BodyTypeDefOf.Female : BodyTypeDefOf.Male;
            }
        }
    }
}
