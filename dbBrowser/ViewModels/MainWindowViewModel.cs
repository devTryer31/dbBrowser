using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using dbBrowser.Commands;
using dbBrowser.Data.Model;
using dbBrowser.ViewModels.Base;
using dbBrowser.ViewModels.Data;

namespace dbBrowser.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private readonly UniversityDataBaseContainer _Db;

		private string _Title = "AccessBrowser";
		private string _SelectedTableName;
		private IItemsLoaded _SelectedViewModel;

		public string Title {
			get => _Title;
			set => Set(ref _Title, value);
		}

		public IItemsLoaded SelectedViewModel {
			get => _SelectedViewModel;
			set => Set(ref _SelectedViewModel, value);
		}

		public Dictionary<string, IItemsLoaded> TablesViewModelsFromNames { get; }

		public string SelectedTableName {
			get => _SelectedTableName;
			set {
				SelectedViewModel = TablesViewModelsFromNames[value];
				Set(ref _SelectedTableName, value);
			}
		}

		public MainWindowViewModel()
		{
			_Db = new();
			TablesViewModelsFromNames = new()
			{
				{ "Faculties", new FacultiesViewModel(_Db) },
				{ "StudyGroups", new StudyGroupsViewModel(_Db) },
				{ "Students", new StudentsViewModel(_Db) },
				{ "Privileges", new PrivilegesViewModel(_Db) },
				{ "FamilyRelations", new FamilyRelationsViewMode(_Db) },
				{ "StudentParents", new StudentParentsViewModel(_Db) },
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
			TablesViewModelsFromNames[SelectedTableName].LoadItems();
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
