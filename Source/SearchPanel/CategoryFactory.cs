using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SearchPanel
{
    public class CategoryFactory
    {
        private Filter<Thing> filterThing;
        private Filter<Terrain> filterTerrain;
        private Searcher<Thing> thingSearhcer;
        private Searcher<Terrain> terrainSearhcer;

        protected readonly Searcher<Thing> defaultThingSearhcer;
        protected readonly Searcher<Terrain> defaultTerrainSearhcer;
        protected readonly Filter<Thing> ignore;
        protected readonly Filter<Thing> everything;
        protected readonly Filter<Thing> nothing;
        protected readonly Filter<Thing> corpsesIgnore;
        protected readonly Filter<Thing> notFoggedThing;
        protected readonly Filter<Terrain> notFoggedTerrain;
        protected readonly Filter<Terrain> noTerrain;

        public CategoryFactory(Searcher<Thing> thingSearhcer, Searcher<Terrain> terrainSearhcer)
        {
            defaultThingSearhcer = thingSearhcer;
            defaultTerrainSearhcer = terrainSearhcer;

            ignore = new FilterRequestGroupIgnore(ThingRequestGroup.Construction, // Blueprint + BuildingFrame
                             new FilterRequestGroupIgnore(ThingRequestGroup.Filth,
                             new FilterThingCategoryIgnore(ThingCategory.Mote,
                             new FilterThingCategoryIgnore(ThingCategory.Ethereal))));
            everything = new FilterRequestGroup(ThingRequestGroup.Everything, ignore);
            nothing = new FilterRequestGroupIgnore(ThingRequestGroup.Everything);
            corpsesIgnore = new FilterRequestGroupIgnore(ThingRequestGroup.Corpse, everything);
            notFoggedThing = new FilterThingNotFogged(everything);
            notFoggedTerrain = new FilterTerrainNotFogged();
            noTerrain = new FilterTerrainNone();
        }

        public virtual IEnumerable<Category> GetCategories()
        {
            filterThing = notFoggedThing;
            filterTerrain = notFoggedTerrain;
            thingSearhcer = defaultThingSearhcer;
            terrainSearhcer = defaultTerrainSearhcer;

            // TODO: Favourites
            yield return GetCategory();

            filterTerrain = noTerrain;
            yield return GetCategory(ThingCategory.Building);

            filterThing = nothing;
            filterTerrain = notFoggedTerrain;
            yield return GetCategory();

            filterThing = notFoggedThing;
            filterTerrain = noTerrain;
            yield return GetCategory(ThingRequestGroup.Plant);
            yield return GetCategory(ThingRequestGroup.HarvestablePlant);

            filterThing = corpsesIgnore;
            yield return GetCategory(ThingRequestGroup.FoodSourceNotPlantOrTree);

            filterThing = notFoggedThing;
            yield return GetCategory(ThingRequestGroup.Pawn);
            yield return GetCategory(ThingRequestGroup.Corpse);
            yield return GetCategory(ThingRequestGroup.Apparel);
            yield return GetCategory(ThingRequestGroup.Weapon);
            yield return GetCategory(ThingRequestGroup.Medicine);
            yield return GetCategory(ThingRequestGroup.Drug);
        }

        protected Category GetCategory(ThingRequestGroup thingRequestGroup)
        {
            var anotherFilterThing = new FilterRequestGroup(thingRequestGroup, filterThing);
            return GetCategory(anotherFilterThing);
        }

        protected Category GetCategory(ThingCategory thingCategory)
        {
            var anotherFilterThing = new FilterThingCategory(thingCategory, filterThing);
            return GetCategory(anotherFilterThing);
        }

        protected Category GetCategory(Filter<Thing> filterThingAnother) => new Category(filterThingAnother, filterTerrain, thingSearhcer, terrainSearhcer);

        protected Category GetCategory() => new Category(filterThing, filterTerrain, thingSearhcer, terrainSearhcer);
    }
}