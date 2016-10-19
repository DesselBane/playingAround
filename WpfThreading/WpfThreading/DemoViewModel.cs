using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfThreading
{
    public class DemoViewModel : Base
    {
        private ObservableCollection<DemoTreeViewItem> _items = new ObservableCollection<DemoTreeViewItem>();
        private bool _isInitialized;
        public ObservableCollection<DemoTreeViewItem> Items => _items;

        public bool IsInitialized
        {
            get { return _isInitialized; }
            private set
            {
                _isInitialized = value;
                FirePropertyChanged();
                FirePropertyChanged(nameof(CanInitialize));
            }
        }

        public bool CanInitialize => !IsInitialized;

        public DemoViewModel()
        {
            
        }

        public async void Initialize()
        {
            if(IsInitialized)
                return;

            IsInitialized = true;



            var itm = await Task.Run(() => new DemoTreeViewItem($"Item {0}", 0));

            _items.Add(itm);

            itm.Initialize();
        }
    }
}
