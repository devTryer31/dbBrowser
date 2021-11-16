using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;

namespace dbBrowser.ViewModels.Data
{
	public class StudyGroupsViewModel : DataBaseDataViewModel<StudyGroup>
	{
		private readonly UniversityDataBaseContainer _Db;
		private StudyGroup _CurrentStudyGroup;
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

		public StudyGroup CurrentStudyGroup {
			get => _CurrentStudyGroup;
			set => Set(ref _CurrentStudyGroup, value);
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
