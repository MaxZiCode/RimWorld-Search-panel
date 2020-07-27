using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    [StaticConstructorOnStartup]
    public static class MainMVC
    {
        public static ThingFactory ThingFactory { get; set; } = new ThingFactory();
        public static TerrainFactory TerrainFactory { get; set; } = new TerrainFactory();
        public static SearchItemFactory SearchItemFactory { get; set; } = new MapSearcher(ThingFactory, TerrainFactory);
        public static CategoryFactory CategoryFactory { get; set; } = new CategoryFactory();
        public static SeekModel SeekModel { get; set; } = new SeekModel(SearchItemFactory, CategoryFactory);
        public static WindowController WindowController { get; set; } = new WindowController(SeekModel);
    }
}