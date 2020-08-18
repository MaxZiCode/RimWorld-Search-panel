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
        public static Searcher<Thing> ThingSearcher { get; set; } = new ThingSearcher();
        public static Searcher<Terrain> TerrainSearcher { get; set; } = new TerrainSearcher();
        public static CategoryFactory CategoryFactory { get; set; } = new CategoryFactory(ThingSearcher, TerrainSearcher);
        public static ISeekModel SeekModel { get; set; } = new SeekModel(CategoryFactory);
        public static ISeekController SeekController { get; set; } = new SeekController(SeekModel);
        public static Window MainWindow { get; set; } = new MainWindow(SeekController, SeekModel);
        public static WindowController MainWindowController { get; set; } = new WindowController(MainWindow);
        public static MapMarker MapMarker { get; set; } = new MapMarker(SeekModel);
    }
}