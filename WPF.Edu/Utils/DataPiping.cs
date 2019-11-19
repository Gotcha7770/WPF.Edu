using System.Windows;

namespace WPF.Edu.Utils
{
    /// <summary>
    /// Используется для создания биндингов к read-only пропертям
    /// </summary>
    /// <example>
    /// <Setter Property="dataPipes:DataPiping.DataPipes">
    ///     <Setter.Value>
    ///         <dataPipes:DataPipeCollection>
    ///             <dataPipes:DataPipe Source="{Binding RelativeSource={RelativeSource AncestorType={x:Type dxdo:DocumentPanel}}, Path=IsVisible}"
    ///                                 Target="{Binding Path=IsVisible, Mode=OneWayToSource}"/>
    ///         </dataPipes:DataPipeCollection>
    ///     </Setter.Value>
    /// </Setter>
    /// </example>
    /// <example>
    /// <Canvas>
    ///     <u:DataPiping.DataPipes>
    ///         <u:DataPipeCollection>
    ///             <u:DataPipe Source = "{Binding RelativeSource={RelativeSource AncestorType={x:Type Canvas}}, Path=ActualWidth}"
    ///                         Target="{Binding Path=ViewportWidth, Mode=OneWayToSource}"/>
    ///             <u:DataPipe Source = "{Binding RelativeSource={RelativeSource AncestorType={x:Type Canvas}}, Path=ActualHeight}"
    ///                         Target="{Binding Path=ViewportHeight, Mode=OneWayToSource}"/>
    ///         </u:DataPipeCollection>
    ///     </u:DataPiping.DataPipes>
    /// </Canvas>
    /// </example>
    public class DataPiping
    {
        #region DataPipes (Attached DependencyProperty)

        public static readonly DependencyProperty DataPipesProperty =
            DependencyProperty.RegisterAttached("DataPipes",
                typeof(DataPipeCollection),
                typeof(DataPiping),
                new UIPropertyMetadata(null));

        public static void SetDataPipes(DependencyObject o, DataPipeCollection value)
        {
            o.SetValue(DataPipesProperty, value);
        }

        public static DataPipeCollection GetDataPipes(DependencyObject o)
        {
            return (DataPipeCollection)o.GetValue(DataPipesProperty);
        }

        #endregion
    }

    public class DataPipeCollection : FreezableCollection<DataPipe>
    {

    }

    public class DataPipe : Freezable
    {
        #region Source (DependencyProperty)

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(DataPipe),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataPipe)d).OnSourceChanged(e);
        }

        protected virtual void OnSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            Target = e.NewValue;
        }

        #endregion

        #region Target (DependencyProperty)

        public object Target
        {
            get { return (object)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(object), typeof(DataPipe),
                new FrameworkPropertyMetadata(null));

        #endregion

        protected override Freezable CreateInstanceCore()
        {
            return new DataPipe();
        }
    }
}
