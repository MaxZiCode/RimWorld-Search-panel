﻿using System;
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
		public static SearchItemPackFactory SearchItemFactory { get; set; } = new MapSearcher(ThingFactory, TerrainFactory);
		public static CategoryFactory CategoryFactory { get; set; } = new CategoryFactory(SearchItemFactory);
		public static ISeekModel SeekModel { get; set; } = new SeekModel(CategoryFactory);
		public static ISeekController SeekController { get; set; } = new SeekController(SeekModel);
		public static Window MainWindow { get; set; } = new MainWindow(SeekController, SeekModel);
		public static WindowController MainWindowController { get; set; } = new WindowController(MainWindow);
	}
}