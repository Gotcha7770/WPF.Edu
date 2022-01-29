using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EnumerableEx = System.Linq.EnumerableEx;

namespace WPF.Edu.ViewModels
{
    public interface ICheckable
    {
        bool? IsChecked { get; set; }

        ICommand CheckCommand { get; }
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
            Children = new ObservableCollection<ITreeViewItemModel>(children ?? Enumerable.Empty<ITreeViewItemModel>());
            CheckCommand = ReactiveCommand.Create(SetChecked);

            this.WhenAnyValue(x => x.IsChecked).Subscribe(SpecificLogic);
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
            set => this.RaiseAndSetIfChanged(ref _isChecked, value);
        }

        public ICommand CheckCommand { get; }


        private void SetChecked()
        {
            CheckChildNodes(IsChecked.Value);
            UpdateParentCheckedState();
        }

        private void CheckChildNodes(bool value)
        {
            foreach(var item in Children.Expand(x => x.Children))
            {
                item.IsChecked = value;
            }
        }

        private IEnumerable<ITreeViewItemModel> EnumerateParents()
        {
            var parent = Parent;

            while (parent is not null)
            {
                yield return parent;
                parent = parent.Parent;
            }
        }

        private void UpdateParentCheckedState()
        {
            // var que = new Queue<ITreeViewItemModel>();
            // que.Enqueue(Parent);
            //
            // while (que.Any())
            // {
            //     ITreeViewItemModel item = que.Dequeue();
            //     if(item != null)
            //     {
            //         item.IsChecked = item.Children.Select(x => x.IsChecked)
            //             .Aggregate((acc, cur) => acc.HasValue && acc == cur ? cur : null);
            //
            //         que.Enqueue(item.Parent);
            //     }
            // }

            foreach (var parent in EnumerateParents())
            {
                parent.IsChecked = parent.Children.Select(x => x.IsChecked)
                    .Aggregate((acc, cur) => acc.HasValue && acc == cur ? cur : null);
            }
        }
    }
}
