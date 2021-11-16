using System;
using System.Collections.Generic;
using System.Data.Entity;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;

namespace dbBrowser.ViewModels.Data
{
	public class StudentsViewModel : DataBaseDataViewModel<Student>
	{
		private readonly UniversityDataBaseContainer _Db;

		public StudentsViewModel(UniversityDataBaseContainer db)
		{
			_Db = db;
			Items = db.Students.Local;
		}

		public override void LoadItems()
		{
			_Db.Students.Load();
		}

		public override IEnumerable<Student> Items { get; }
	}
}
