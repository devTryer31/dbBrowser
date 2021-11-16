using System.Collections.Generic;
using System.Data.Entity;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;

namespace dbBrowser.ViewModels.Data
{
	public class FamilyRelationsViewMode : DataBaseDataViewModel<FamilyRelation>
	{
		private readonly UniversityDataBaseContainer _Db;

		public FamilyRelationsViewMode(UniversityDataBaseContainer db)
		{
			_Db = db;
			Items = db.FamilyRelations.Local;
		}

		public override void LoadItems()
		{
			_Db.FamilyRelations.Load();
		}

		public override IEnumerable<FamilyRelation> Items { get; }
	}
}
