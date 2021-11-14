using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbBrowser.Data.Model;

namespace dbBrowser.Data
{
	static class DbInitializer
	{
		private static void InitializeDb(UniversityDataBaseContainer db)
		{
			Faculty faculty = new Faculty {
				Dean = "DeanFirst",
				Auditorium = "FirstAud154",
				Title = "СПИНТех"
			};
			faculty = db.Faculties.Add(faculty);

			StudyGroup studyGroup = new StudyGroup {
				StudentsAmount = 11,
				Title = "ПИН-11",
				Faculty = faculty
			};
			studyGroup = db.StudyGroups.Add(studyGroup);

			Student student = new Student {
				Name = "First Name",
				Surname = "Second Name",
				Patronymic = "Patronymic",
				Birthday = DateTime.Now,
				Address = "Not Found",
				StudyGroup = studyGroup
			};
			student = db.Students.Add(student);

			db.Privileges.Add(new Privilege {
				ReleaseDate = DateTime.Now,
				DocumenPath = "HomePage",
				Title = "FirstPrivilege",
				Student = student
			});

			db.SaveChanges();
		}
	}
}
