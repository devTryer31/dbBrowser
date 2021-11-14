using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace dbBrowser.ViewModels.Base
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected virtual bool Set<T>(ref T source, T value, string propertyName = null)
		{
			if (ReferenceEquals(value, source))
				return false;

			source = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}
