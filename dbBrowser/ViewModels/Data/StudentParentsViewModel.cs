using System.Collections.Generic;
using System.Data.Entity;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;

namespace dbBrowser.ViewModels.Data
{
	public class StudentParentsViewModel : DataBaseDataViewModel<StudentParent>
	{
		private readonly UniversityDataBaseContainer _Db;

		public StudentParentsViewModel(UniversityDataBaseContainer db)
		{
			_Db = db;
			Items = db.StudentParents.Local;
		}

		public override void LoadItems()
		{
			_Db.StudentParents.Load();
		}

		public override IEnumerable<StudentParent> Items { get; }
	}
}
