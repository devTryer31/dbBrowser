using System.Collections.Generic;
using System.Data.Entity;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;

namespace dbBrowser.ViewModels.Data
{
	public class PrivilegesViewModel : DataBaseDataViewModel<Privilege>
	{
		private readonly UniversityDataBaseContainer _Db;

		public PrivilegesViewModel(UniversityDataBaseContainer db)
		{
			_Db = db;
			Items = db.Privileges.Local;
		}

		public override void LoadItems()
		{
			_Db.Privileges.Load();
		}

		public override IEnumerable<Privilege> Items { get; }
	}
}
