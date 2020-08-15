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
        protected readonly Filter<Thing> Ignore;
        protected readonly Filter<Thing> Everything;
        protected readonly Filter<Thing> Nothing;
        protected readonly Filter<Thing> CorpsesIgnore;
        protected readonly Filter<Terrain> NotFoggedTerrain;
        protected readonly Filter<Terrain> NoTerrain;

        public CategoryFactory(Searcher<Thing> thingSearhcer, Searcher<Terrain> terrainSearhcer)
        {
            defaultThingSearhcer = thingSearhcer;
            defaultTerrainSearhcer = terrainSearhcer;

            Ignore = new FilterRequestGroupIgnore(ThingRequestGroup.Construction, // Blueprint + BuildingFrame
                             new FilterRequestGroupIgnore(ThingRequestGroup.Filth,
                             new FilterThingCategoryIgnore(ThingCategory.Mote,
                             new FilterThingCategoryIgnore(ThingCategory.Ethereal))));
            Everything = new FilterRequestGroup(ThingRequestGroup.Everything, Ignore);
            Nothing = new FilterRequestGroupIgnore(ThingRequestGroup.Everything);
            CorpsesIgnore = new FilterRequestGroupIgnore(ThingRequestGroup.Corpse, Everything);
            NotFoggedTerrain = new FilterTerrainNotFogged();
            NoTerrain = new FilterTerrainNone();
        }

        public virtual IEnumerable<Category> GetCategories()
        {
            filterThing = Everything;
            filterTerrain = NotFoggedTerrain;
            thingSearhcer = defaultThingSearhcer;
            terrainSearhcer = defaultTerrainSearhcer;

            // TODO: Favourites
            yield return GetCategory();

            filterTerrain = NoTerrain;
            yield return GetCategory(ThingCategory.Building);

            filterThing = Nothing;
            filterTerrain = NotFoggedTerrain;
            yield return GetCategory();

            filterThing = Everything;
            filterTerrain = NoTerrain;
            yield return GetCategory(ThingRequestGroup.Plant);
            yield return GetCategory(ThingRequestGroup.HarvestablePlant);

            filterThing = CorpsesIgnore;
            yield return GetCategory(ThingRequestGroup.FoodSourceNotPlantOrTree);

            filterThing = Everything;
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