using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public interface ISearchable
    {
        string Label { get; }
        int Count { get; }
        Texture2D Texture { get; }
        Color Color { get; }
        BuildableDef Def { get; }
        ThingDef Stuff { get; }

        IEnumerable<IntVec3> GetCells();
    }
}