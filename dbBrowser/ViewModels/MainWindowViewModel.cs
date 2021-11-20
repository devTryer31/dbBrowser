using System.Collections.Generic;
using System.Data.Entity;
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

		private string _Title = "dbBrowser";
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

			LoadAllTablesDataCommand =
				new LambdaCommand(OnLoadAllTablesDataCommandExecuted, CanLoadAllTablesDataCommandExecute);

			RemoveAllDataTablesChangesCommand =
				new LambdaCommand(OnRemoveAllDataTablesChangesCommandExecuted, CanRemoveAllDataTablesChangesCommandExecute);

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

		#region LoadAllTablesDataCommand

		public ICommand LoadAllTablesDataCommand { get; }

		private bool CanLoadAllTablesDataCommandExecute(object p) => true;

		private void OnLoadAllTablesDataCommandExecuted(object p)
		{
			_Db.Faculties.Load();
			_Db.StudyGroups.Load();
			_Db.Students.Load();
			_Db.StudentParents.Load();
			_Db.FamilyRelations.Load();
			_Db.Privileges.Load();
			OnPropertyChanged(nameof(SelectedViewModel));
		}

		#endregion

		#region RemoveAllDataTablesChangesCommand

		public ICommand RemoveAllDataTablesChangesCommand { get; }

		private bool CanRemoveAllDataTablesChangesCommandExecute(object p) => true;

		private void OnRemoveAllDataTablesChangesCommandExecuted(object p)
		{
			foreach (var entry in _Db.ChangeTracker.Entries()) {
				switch (entry.State) {
					case EntityState.Modified:
						entry.State = EntityState.Unchanged;
						break;
					case EntityState.Deleted:
						entry.Reload();
						break;
					case EntityState.Added:
						entry.State = EntityState.Detached;
						break;
				}
			}
		}

		#endregion

		#endregion

	}
}
