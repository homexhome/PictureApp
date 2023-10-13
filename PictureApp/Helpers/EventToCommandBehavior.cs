using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureApp.Helpers;

public class EventToCommandBehavior : Behavior<CollectionView>
{
    public static readonly BindableProperty EventNameProperty =
        BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior), null);

    public string EventName
    {
        get => (string)GetValue(EventNameProperty);
        set => SetValue(EventNameProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnAttachedTo(CollectionView bindable) {
        base.OnAttachedTo(bindable);
        bindable.SelectionChanged += OnSelectionChanged;
    }

    protected override void OnDetachingFrom(CollectionView bindable) {
        base.OnDetachingFrom(bindable);
        bindable.SelectionChanged -= OnSelectionChanged;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        if (Command != null && Command.CanExecute(e.CurrentSelection)) {
            Command.Execute(e.CurrentSelection);
        }
    }

    private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue) {

    }
}

