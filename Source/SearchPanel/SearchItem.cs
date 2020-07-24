using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public struct SearchItem
    {
        public string Label { get; set; }

        public Texture2D Texture { get; set; }

        public Color Color { get; set; }

        public int Count { get; set; }

        public BuildableDef Def { get; set; }

        public IReadOnlyCollection<ObjectWithCells> ObjectsWithCells { get; set; }

        public List<IntVec3> Cells { get; set; }

        public SearchItem(BuildableDef def, IEnumerable<ObjectWithCells> objectsWithCells, int count, ThingDef stuff = null)
        {
            Label = GenLabel.ThingLabel(def, stuff);
            Texture = def.uiIcon;
            Color = stuff != null ? def.GetColorForStuff(stuff) : def.uiIconColor;
            Count = count;
            Def = def;

            ObjectsWithCells = objectsWithCells.ToList();
            Cells = ObjectsWithCells.SelectMany(owc => owc.Cells).ToList();
        }
    }
}