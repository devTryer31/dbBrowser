using System.Collections.Generic;
using System.Data.Entity;
using dbBrowser.Data.Model;

namespace dbBrowser.ViewModels.Data
{
	public class PrivilegesViewModel : DataBaseDataViewModel<Privilege>
	{
		private readonly UniversityDataBaseContainer _Db;
		private IEnumerable<Student> _Students;

		public PrivilegesViewModel(UniversityDataBaseContainer db)
		{
			_Db = db;
			Items = db.Privileges.Local;
			Students = _Db.Students.Local;
		}

		public override void LoadItems()
		{
			_Db.Students.Load();
			Students = _Db.Students.Local;
			_Db.Privileges.Load();
			OnPropertyChanged(nameof(Items));
		}

		public IEnumerable<Student> Students {
			get => _Students;
			set => Set(ref _Students, value);
		}

		public override IEnumerable<Privilege> Items { get; }
	}
}
