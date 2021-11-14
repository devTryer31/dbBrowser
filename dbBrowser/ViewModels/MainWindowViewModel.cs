using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using dbBrowser.Commands;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;

namespace dbBrowser.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private readonly UniversityDataBaseContainer _Db;

		private string _Title = "AccessBrowser";
		private string _SelectedTableName;

		public string Title {
			get => _Title;
			set => Set(ref _Title, value);
		}


		public Dictionary<string, DbSet> TablesFromNames { get; }

		public string SelectedTableName {
			get => _SelectedTableName;
			set {
				Set(ref _SelectedTableName, value);
				OnPropertyChanged(nameof(SelectedDbSet));
			}
		}

		public object SelectedDbSet => TablesFromNames[SelectedTableName].Local;

		public MainWindowViewModel()
		{
			_Db = new();
			TablesFromNames = new()
			{
				{ "Faculties", _Db.Faculties },
				{ "StudyGroups", _Db.StudyGroups },
				{ "Students", _Db.Students },
				{ "Privileges", _Db.Privileges },
				{ "FamilyRelations", _Db.FamilyRelations },
				{ "StudentParents", _Db.StudentParents },
			};

			#region Commands

			LoadFullTableDataCommand =
				new LambdaCommand(OnLoadFullTableDataCommandExecuted, CanLoadFullTableDataCommandExecute);

			SaveDbChangesCommand = 
				new LambdaCommand(OnSaveDbChangesCommandExecuted, CanSaveDbChangesCommandExecute);

			#endregion
		}


		#region Commands



		#region LoadFullTableDataCommand

		public ICommand LoadFullTableDataCommand { get; }

		private bool CanLoadFullTableDataCommandExecute(object p) => true;

		private void OnLoadFullTableDataCommandExecuted(object p)
		{
			TablesFromNames[SelectedTableName].Load();
			OnPropertyChanged(nameof(SelectedDbSet));
		}

		#endregion

		#region SaveDbChangesCommand

		public ICommand SaveDbChangesCommand { get; }

		private bool CanSaveDbChangesCommandExecute(object p) => true;

		private void OnSaveDbChangesCommandExecuted(object p)
		{
			_Db.SaveChanges();
		}

		#endregion

		#endregion

	}
}
