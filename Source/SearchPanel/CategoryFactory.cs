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
        public readonly Category IgnoreCategory;
        public readonly Category Everything;
        public readonly Category CorpsesIgnore;

        public CategoryFactory()
        {
            IgnoreCategory = new CategoryViaIgnoreRequestGroup(ThingRequestGroup.Construction, // Blueprint + BuildingFrame
                             new CategoryViaIgnoreRequestGroup(ThingRequestGroup.Filth,
                             new CategoryViaThingCategory(ThingCategory.Mote,
                             new CategoryViaThingCategory(ThingCategory.Ethereal))));
            Everything = new CategoryViaRequestGroup(ThingRequestGroup.Everything, IgnoreCategory);
            CorpsesIgnore = new CategoryViaIgnoreRequestGroup(ThingRequestGroup.Corpse);
        }

        public virtual IEnumerable<Category> GetCategories()
        {
#if DEBUG
            foreach (ThingRequestGroup trg in Enum.GetValues(typeof(ThingRequestGroup)))
            {
                yield return new CategoryViaRequestGroup(trg);
            }

            foreach (ThingCategory tc in Enum.GetValues(typeof(ThingCategory)))
            {
                yield return new CategoryViaThingCategory(tc);
            }
            yield break;
#endif

            yield return Everything;
            yield return new CategoryViaRequestGroup(ThingRequestGroup.FoodSourceNotPlantOrTree, CorpsesIgnore);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Corpse);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Pawn);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Medicine);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Weapon);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Drug);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.HarvestablePlant);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Plant);
            yield return new CategoryViaRequestGroup(ThingRequestGroup.Apparel);
        }
    }
}
