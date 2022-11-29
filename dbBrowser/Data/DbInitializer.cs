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
        public static void InitializeDb(UniversityDataBaseContainer db)
        {
            db.Database.CreateIfNotExists();

            Faculty faculty = new Faculty
            {
                Dean = "DeanFirst",
                Auditorium = "FirstAud154",
                Title = "СПИНТех"
            };
            faculty = db.Faculties.Add(faculty);

            var studyGroups = db.StudyGroups.AddRange(GetStudyGroupCollection(faculty));

            Student student = new Student
            {
                Name = "First Name",
                Surname = "Second Name",
                Patronymic = "Patronymic",
                Birthday = DateTime.Now,
                Address = "Not Found",
                StudyGroup = studyGroups.First()
            };

            student = db.Students.Add(student);

            db.Privileges.Add(new Privilege
            {
                ReleaseDate = DateTime.Now,
                DocumenPath = "HomePage",
                Title = "FirstPrivilege",
                Student = student
            });

            db.SaveChanges();
        }

        public static IEnumerable<StudyGroup> GetStudyGroupCollection(Faculty faculty)
            => new StudyGroup[]
            {
                        new StudyGroup {
                                StudentsAmount = 11,
                                Title = "ПИН-11",
                                Faculty = faculty
                            },
                        new StudyGroup {
                                StudentsAmount = 20,
                                Title = "П-41",
                                Faculty = faculty
                            }
            };
    }
}
