using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using O21Toolbox;

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
        }

        public override string SettingsCategory()
        {
            return "Slim Rim";
        }

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
        public bool slimRim = true;
        public bool slimRim_fat = true;
        public bool slimRim_hulk = false;
        public bool slimRim_thin = false;


        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref slimRim, "slimRim", true);
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
}
