using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using dbBrowser.Data.Model;

namespace dbBrowser.ViewModels.Data
{
	public class StudyGroupsViewModel : DataBaseDataViewModel<StudyGroup>
	{
		private readonly UniversityDataBaseContainer _Db;
		private IEnumerable<Faculty> _Faculties;

		public StudyGroupsViewModel(UniversityDataBaseContainer db)
		{
			_Db = db;
			Items = db.StudyGroups.Local;
		}

		public override IEnumerable<StudyGroup> Items { get; }

		public IEnumerable<Faculty> Faculties {
			get => _Faculties ?? Enumerable.Empty<Faculty>();
			set => Set(ref _Faculties, value);
		}

		public override void LoadItems()
		{
			_Db.Faculties.Load();
			Faculties = _Db.Faculties.Local;
			_Db.StudyGroups.Load();
			OnPropertyChanged(nameof(Items));
		}
	}
}
