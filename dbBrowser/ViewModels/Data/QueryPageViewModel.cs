using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using dbBrowser.Commands;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;

namespace dbBrowser.ViewModels.Data
{
	public class QueryPageViewModel : BaseViewModel
	{
		private readonly UniversityDataBaseContainer _Db;
		private string _Query;
		private IEnumerable _ResultItems;

		public QueryPageViewModel()
		{
			_Db = new();

			#region Commands

			GetTheOldestStudentsCommand =
				new LambdaCommand(OnGetTheOldestStudentsCommandExecuted, o => true);

			GetTwoParentsStudentsCommand =
				new LambdaCommand(OnGetTwoParentsStudentsCommandExecuted, o => true);

			FindGroupsWithOrphanCommand =
				new LambdaCommand(OnFindGroupsWithOrphanCommandExecuted, o => true);

			#endregion

		}

		public string Query {
			get => _Query;
			set => Set(ref _Query, value);
		}

		public IEnumerable ResultItems {
			get => _ResultItems;
			set => Set(ref _ResultItems, value);
		}


		#region Commands

		public ICommand GetTheOldestStudentsCommand { get; set; }

		private void OnGetTheOldestStudentsCommandExecuted(object p)
		{
			var q =
				_Db.Students.OrderByDescending(s => s.Birthday).Take(3)
					.Select(s => new { s.Id, s.Surname, s.Name, s.StudyGroup.Title });
			Query = q.ToString();
			ResultItems = q.ToList();
		}

		public ICommand GetTwoParentsStudentsCommand { get; set; }

		private void OnGetTwoParentsStudentsCommandExecuted(object p)
		{
			var q =
				_Db.FamilyRelations.GroupBy(fr => fr.Student).Where(g => g.Count() == 2)
					.Select(g => g.Key)
					.Select(s => new { s.Id, s.Surname, s.Name, s.StudyGroup.Title });
			Query = q.ToString();
			ResultItems = q.ToList();
		}

		public ICommand FindGroupsWithOrphanCommand { get; set; }

		private void OnFindGroupsWithOrphanCommandExecuted(object p)
		{
			var q =
				_Db.StudyGroups.Include("Students")
					.Select(g => new {Group = g.Title, Count = g.Students.Count(s => s.Privileges.Any(p => p.Title == "Сирота"))})
					.Where(at => at.Count > 0);
			Query = q.ToString();
			ResultItems = q.ToList();
		}

		//public ICommand GetTheOldestStudentsCommand { get; set; }

		//private void OnGetTheOldestStudentsCommandExecuted(object p)
		//{
		//	var q =
		//		_Db.Students.OrderByDescending(s => s.Birthday).Take(3)
		//			.Select(s => new { s.Id, s.Surname, s.Name, s.StudyGroup.Title });
		//	Query = q.ToString();
		//	ResultItems = q.ToList();
		//}
		#endregion

	}
}
