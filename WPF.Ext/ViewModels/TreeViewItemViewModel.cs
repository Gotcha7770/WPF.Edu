using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WPF.Ext.ViewModels
{
    public interface ICheckable
    {
        bool? IsChecked { get; set; }

        ICommand CheckedCommand { get; }
    }

    public interface ITreeViewItemModel : ICheckable
    {
        string Value { get; set; }

        ITreeViewItemModel Parent { get; }

        ObservableCollection<ITreeViewItemModel> Children { get; }
    }

    public class TreeViewItemViewModelBase : ReactiveObject, ITreeViewItemModel
    {
        private bool? _isChecked;

        public TreeViewItemViewModelBase(string value, ITreeViewItemModel parent, IEnumerable<ITreeViewItemModel> children = null)
        {
            Value = value;
            Parent = parent;
            Children = new ObservableCollection<ITreeViewItemModel>();

            if(children != null && children.Any())
                Children.AddRange(children);

            CheckedCommand = ReactiveCommand.Create(SetChecked);

            this.WhenAnyValue(x => x.IsChecked).Subscribe( x => SpecificLogic(x));
        }

        protected virtual void SpecificLogic(bool? value)
        {
            //put your specific logic here
        }

        public string Value { get; set; }

        public ITreeViewItemModel Parent { get; }

        public ObservableCollection<ITreeViewItemModel> Children { get; }

        public bool? IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public ICommand CheckedCommand { get; }


        private void SetChecked()
        {
            CheckChildNodes(this, IsChecked.Value);
            UpdateParentCheckedState(Parent);
        }
        
        private static void CheckChildNodes(ITreeViewItemModel item, bool isChecked)
        {
            item.IsChecked = isChecked;
            foreach (var child in item.Children)
            {
                CheckChildNodes(child, isChecked);
            }
        }

        private static void UpdateParentCheckedState(ITreeViewItemModel item)
        {
            if (item == null) return;

            item.IsChecked = item.Children.Select(x => x.IsChecked)
                .Aggregate((acc, cur) => acc.HasValue && acc == cur ? cur : null);

            UpdateParentCheckedState(item.Parent);
        }
    }
}
