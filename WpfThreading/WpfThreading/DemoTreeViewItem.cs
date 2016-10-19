using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace WpfThreading
{
    public class DemoTreeViewItem : Base
    {
        #region Vars

        private ObservableCollection<DemoTreeViewItem> _children = new ObservableCollection<DemoTreeViewItem>();
        private bool _isExpanded;
        private bool _isInitializing;
        private int _level;
        private string _name;
        private bool _isSelected;
        private bool _isInitialized;

        #endregion

        #region Properties

        public string Name => _name;

        public bool IsInitializing
        {
            get { return _isInitializing; }
            private set
            {
                _isInitializing = value;
                FirePropertyChanged();
            }
        }

        public ObservableCollection<DemoTreeViewItem> Children => _children;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;

                FirePropertyChanged();
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                FirePropertyChanged();
            }
        }

        public bool IsInitialized
        {
            get { return _isInitialized; }
            private set
            {
                _isInitialized = value;
                FirePropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public DemoTreeViewItem(string name, int level)
        {
            _name = name;
            _level = level;
            Thread.Sleep(300);
        }

        #endregion

        public async void Initialize()
        {
            if (IsInitialized || IsInitializing)
                return;

            IsExpanded = true;
            IsInitializing = true;
            var childCol = await CreateChildren();

            foreach (DemoTreeViewItem demoTreeViewItem in childCol)
            {
                _children.Add(demoTreeViewItem);
                demoTreeViewItem.Initialize();
            }

            IsInitializing = false;
            IsInitialized = true;
        }


        private Task<ICollection<DemoTreeViewItem>> CreateChildren()
        {
            return Task.Run(() =>
            {
                ICollection<DemoTreeViewItem> child = new Collection<DemoTreeViewItem>();
                if (_level < 4)
                    for (int i = 0; i < 2; i++)
                    {
                        child.Add(new DemoTreeViewItem($"{Name}.{i}", _level + 1));
                    }
                return child;
            });
        }
    }
}