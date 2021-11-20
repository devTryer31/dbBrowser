using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using dbBrowser.Commands;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;
using dbBrowser.Views.Windows;

namespace dbBrowser.ViewModels.Data
{
	public class QueryPageViewModel : BaseViewModel
	{
		private readonly UniversityDataBaseContainer _Db;
		private string _Query;
		private IEnumerable _ResultItems;
		private string _QueryStringResult;

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

			StudentsByPatronymicCommand =
				new LambdaCommand(OnStudentsByPatronymicCommandExecuted, o => true);

			GetFacultyByGroupNameCommand =
				new LambdaCommand(OnGetFacultyByGroupNameCommandExecuted, o => true);

			GetParentsByStudentIdCommand =
				new LambdaCommand(OnGetParentsByStudentIdCommandExecuted, o => true);

			ExecuteSqlQueryCommand =
				new LambdaCommand(OnExecuteSqlQueryCommandExecuted, o => true);

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

		public string QueryStringResult {
			get => _QueryStringResult;
			set => Set(ref _QueryStringResult, value);
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
					.Select(g => new { Group = g.Title, Count = g.Students.Count(s => s.Privileges.Any(p => p.Title == "Сирота")) })
					.Where(at => at.Count > 0);
			Query = q.ToString();
			ResultItems = q.ToList();
		}

		//Massage boxes

		public ICommand StudentsByPatronymicCommand { get; set; }

		private void OnStudentsByPatronymicCommandExecuted(object p)
		{
			var qRes = ShowQueryDialog("Введите отчество:");
			if (string.IsNullOrWhiteSpace(qRes))
				return;

			var q =
				_Db.Students.Where(s => s.Patronymic == qRes);
			Query = q.ToString();
			ResultItems = q.ToList();
		}

		public ICommand GetParentsByStudentIdCommand { get; set; }

		private void OnGetParentsByStudentIdCommandExecuted(object p)
		{
			var qRes = ShowQueryDialog("Введите Id студента:");
			if (string.IsNullOrWhiteSpace(qRes))
				return;

			var qId = int.Parse(qRes);

			var q =
				_Db.FamilyRelations.Include("Student").Include("StudentParent")
					.Where(fr => fr.Student.Id == qId)
					.Select(fr => new { fr.StudentParent.Id, fr.StudentParent.Surname, fr.StudentParent.Name });
			Query = q.ToString();
			ResultItems = q.ToList();
		}

		public ICommand GetFacultyByGroupNameCommand { get; set; }

		private void OnGetFacultyByGroupNameCommandExecuted(object p)
		{
			var qRes = ShowQueryDialog("Введите название группы:");
			if (string.IsNullOrWhiteSpace(qRes))
				return;

			var q =
				_Db.StudyGroups.Include("Faculty").Where(sg => sg.Title == qRes)
					.Select(sg => new { sg.Faculty.Id, sg.Faculty.Title });
			Query = q.ToString();
			ResultItems = q.ToList();
		}

		private string ShowQueryDialog(string inputTitle)
		{
			MainDialogWindow qWindow = new();
			qWindow.InputText.Text = inputTitle;
			qWindow.ShowDialog();
			return qWindow.ResultText.Text;
		}

		public ICommand ExecuteSqlQueryCommand { get; set; }

		private void OnExecuteSqlQueryCommandExecuted(object p)
		{
			string firstWord = Query.Split(' ')[0].ToLower();
			if (firstWord is "insert" or "update" or "delete") {
				int RowsAffected = _Db.Database.ExecuteSqlCommand(Query);
				ResultItems = new List<object> { new { RowsAffected } };
				return;
			}

			//ResultItems = _Db.Database.SqlQuery<object>(Query).ToList();
			var objs = new object[10];
			var resList = new List<object>(10);
			using (SqlConnection connection = new SqlConnection(_Db.Database.Connection.ConnectionString)) {
				SqlCommand command = new SqlCommand(Query, connection);
				try {
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						QueryStringResult += reader.GetValues(objs);
						resList.Add(
							new
							{
								col0 = objs[0],
								col1 = objs[1],
								col2 = objs[2],
								col3 = objs[3],
								col4 = objs[4],
								col5 = objs[5],
								col6 = objs[6],
								col7 = objs[7],
								col8 = objs[8],
								col9 = objs[9]
							});
					}
					reader.Close();
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message);
				}
				finally {
					connection.Close();
				}
			}

			ResultItems = resList;
		}

		#endregion

	}
}
