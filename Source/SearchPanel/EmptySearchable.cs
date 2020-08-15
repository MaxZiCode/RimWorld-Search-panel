using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public class EmptySearchable : ISearchable
    {
        private static readonly EmptySearchable instance = new EmptySearchable();

        private readonly List<IntVec3> cellsChache = new List<IntVec3>();

        public string Label { get; } = string.Empty;
        public string LabelWithStuff { get; } = string.Empty;
        public int Count { get; } = 0;
        public Texture2D Texture { get; } = BaseContent.ClearTex;
        public Color Color { get; } = Color.white;
        public BuildableDef Def { get; }
        public ThingDef Stuff { get; }

        public IEnumerable<IntVec3> GetCells() => cellsChache;

        private EmptySearchable()
        {
        }

        public static EmptySearchable Get() => instance;
    }
}