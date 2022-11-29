using dbBrowser.Data.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace dbBrowser.ViewModels.Data
{
    public class StudentsViewModel : DataBaseDataViewModel<Student>
    {
        private readonly UniversityDataBaseContainer _Db;
        private IEnumerable<StudyGroup> _Faculties;

        public StudentsViewModel(UniversityDataBaseContainer db)
        {
            _Db = db;
            Items = db.Students.Local;
            StudyGroups = _Db.StudyGroups.Local;
        }

        public override void LoadItems()
        {
            _Db.StudyGroups.Load();
            StudyGroups = _Db.StudyGroups.Local;
            _Db.Students.Load();
            OnPropertyChanged(nameof(Items));
        }

        public IEnumerable<StudyGroup> StudyGroups
        {
            get => _Faculties ?? Enumerable.Empty<StudyGroup>();
            set => Set(ref _Faculties, value);
        }

        public override IEnumerable<Student> Items { get; }
    }
}
