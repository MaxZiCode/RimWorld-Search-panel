using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public class ThingSearchItem : SearchItem<Thing>
    {
        private int count;

        public override int Count => count;
        public override Texture2D Texture { get; }
        public override Color Color { get; }
        public override BuildableDef Def { get; }
        public override ThingDef Stuff { get; }

        public ThingSearchItem(Thing thing, string label) : base(thing, label)
        {
            var innerThing = thing is MinifiedThing mThing ? mThing.InnerThing : thing;

            Stuff = innerThing.Stuff;
            Def = innerThing.def;
            Texture = Def.uiIcon;
            Color = Stuff != null ? Def.GetColorForStuff(Stuff) : Def.uiIconColor;
        }

        public override IEnumerable<IntVec3> GetCells() => items.SelectMany(t => GenAdj.OccupiedRect(t.PositionHeld, t.Rotation, t.def.size));

        public override void Update()
        {
            count = items.Sum(i => i.stackCount);
        }
    }
}