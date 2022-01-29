using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using DynamicData.Binding;
using ReactiveUI;

namespace WPF.Edu.ViewModels
{
    public class ValidationExampleViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<bool> _hasErrors;

        private string _value;
        private IReadOnlyCollection<ValidationError> _errors;

        public ValidationExampleViewModel()
        {
            _hasErrors = this.WhenValueChanged(x => x.Errors)
                .Select(x => x is { Count: > 0 })
                .ToProperty(this, x => x.HasErrors);

            ApplyCommand = ReactiveCommand.Create(Apply, this.WhenValueChanged(x => x.HasErrors).Select(x => !x));
        }

        public ICommand ApplyCommand { get; }

        public string Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        public bool HasErrors => _hasErrors.Value;

        public IReadOnlyCollection<ValidationError> Errors
        {
            get => _errors;
            set => this.RaiseAndSetIfChanged(ref _errors, value);
        }

        private void Apply()
        {

        }
    }
}