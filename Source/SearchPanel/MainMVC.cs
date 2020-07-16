using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPanel
{
    public static class MainMVC
    {
        public static readonly SeekModel SeekModel = new SeekModel(new MapSearcher());

        public static readonly WindowController WindowController = new WindowController(SeekModel);
    }
}
