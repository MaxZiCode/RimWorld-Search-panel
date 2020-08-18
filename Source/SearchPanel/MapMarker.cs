using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public class MapMarker : ISearchableObserver
    {
        protected ISeekModel model;
        protected readonly List<IntVec3> markingLocations = new List<IntVec3>();

        public MapMarker(ISeekModel model)
        {
            model.RegisterObserver(this);
            this.model = model;
        }

        public virtual void Draw()
        {
            if (markingLocations.Count() != 0)
                GenDraw.DrawFieldEdges(markingLocations, Color.magenta);
        }

        public virtual void AfterUpdateSearchItem()
        {
            markingLocations.Clear();
            markingLocations.AddRange(model.ActiveSearchable.GetCells());
        }
    }
}