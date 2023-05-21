namespace PluginSample1;

/// <summary>
/// 設定ウィンドウ
/// </summary>
public partial class SettingsWindow : Window
{
    /// <summary>
    /// PluginProcessing
    /// </summary>
    private PluginProcessing PluginProcessing;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public SettingsWindow()
    {
        throw new Exception("Error.");
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="pluginProcessing">PluginProcessing</param>
    public SettingsWindow(
        PluginProcessing pluginProcessing
        )
    {
        InitializeComponent();

        PluginProcessing = pluginProcessing;

        if ((PluginProcessing.GetWindowEventType & GetWindowEventType.MoveSizeEnd) == GetWindowEventType.MoveSizeEnd)
        {
            MoveSizeEndCheckBox.IsChecked = true;
        }
        if ((PluginProcessing.GetWindowEventType & GetWindowEventType.Show) == GetWindowEventType.Show)
        {
            ShowCheckBox.IsChecked = true;
        }

        MoveSizeEndCheckBox.Checked += MoveSizeEndCheckBox_Checked;
        MoveSizeEndCheckBox.Unchecked += MoveSizeEndCheckBox_Unchecked;
        ShowCheckBox.Checked += ShowCheckBox_Checked;
        ShowCheckBox.Unchecked += ShowCheckBox_Unchecked;
    }

    private void MoveSizeEndCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        try
        {
            SettingHookWindowEventType();
        }
        catch
        {
        }
    }

    private void MoveSizeEndCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        try
        {
            SettingHookWindowEventType();
        }
        catch
        {
        }
    }

    private void ShowCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        try
        {
            SettingHookWindowEventType();
        }
        catch
        {
        }
    }

    private void ShowCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        try
        {
            SettingHookWindowEventType();
        }
        catch
        {
        }
    }

    private void SettingHookWindowEventType()
    {
        GetWindowEventType eventType = 0;

        if (MoveSizeEndCheckBox.IsChecked == true)
        {
            eventType |= GetWindowEventType.MoveSizeEnd;
        }
        if (ShowCheckBox.IsChecked == true)
        {
            eventType |= GetWindowEventType.Show;
        }

        PluginProcessing.GetWindowEventTypeValue = eventType;
        PluginProcessing.ChangeGetWindowEventTypeData.DoChangeEventType(eventType);
    }
}
