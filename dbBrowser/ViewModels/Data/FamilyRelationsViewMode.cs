using System.Collections.Generic;
using System.Data.Entity;
using dbBrowser.Data.Model;

namespace dbBrowser.ViewModels.Data
{
	public class FamilyRelationsViewMode : DataBaseDataViewModel<FamilyRelation>
	{
		private readonly UniversityDataBaseContainer _Db;
		private IEnumerable<Student> _Students;
		private IEnumerable<StudentParent> _StudentParents;

		public FamilyRelationsViewMode(UniversityDataBaseContainer db)
		{
			_Db = db;
			Items = db.FamilyRelations.Local;
			Students = _Db.Students.Local;
			StudentParents = _Db.StudentParents.Local;
		}

		public override void LoadItems()
		{
			_Db.Students.Load();
			Students = _Db.Students.Local;

			_Db.StudentParents.Load();
			StudentParents = _Db.StudentParents.Local;

			_Db.FamilyRelations.Load();
			OnPropertyChanged(nameof(Items));
		}

		public IEnumerable<Student> Students {
			get => _Students;
			set => Set(ref _Students, value);
		}

		public IEnumerable<StudentParent> StudentParents {
			get => _StudentParents;
			set => Set(ref _StudentParents, value);
		}

		public override IEnumerable<FamilyRelation> Items { get; }
	}
}
