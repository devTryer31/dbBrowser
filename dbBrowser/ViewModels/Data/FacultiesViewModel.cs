using System.Collections.Generic;
using System.Data.Entity;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;

namespace dbBrowser.ViewModels.Data
{
	public class FacultiesViewModel : DataBaseDataViewModel<Faculty>
	{
		private readonly UniversityDataBaseContainer _Db;

		public FacultiesViewModel(UniversityDataBaseContainer db)
		{
			_Db = db;
			Items = db.Faculties.Local;
		}

		public override void LoadItems()
		{
			_Db.Faculties.Load();
		}

		public override IEnumerable<Faculty> Items { get; }
	}
}
