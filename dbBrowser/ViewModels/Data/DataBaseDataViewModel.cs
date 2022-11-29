using dbBrowser.ViewModels.Base;
using System.Collections.Generic;

namespace dbBrowser.ViewModels.Data
{
    public abstract class DataBaseDataViewModel<T> : BaseViewModel, IItemsLoaded
    {
        public abstract IEnumerable<T> Items { get; }

        public abstract void LoadItems();
    }

    public interface IItemsLoaded
    {
        void LoadItems();
    }
}
