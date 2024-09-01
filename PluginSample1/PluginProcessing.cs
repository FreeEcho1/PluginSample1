namespace PluginSample1;

public class PluginProcessing : IPlugin
{
    /// <summary>
    /// Disposed
    /// </summary>
    private bool Disposed;
    /// <summary>
    /// プラグインの名前
    /// </summary>
    public string PluginName { get; } = "Plugin Sample 1";
    /// <summary>
    /// ウィンドウが存在するかの値
    /// </summary>
    public bool IsWindowExist { get; } = true;
    /// <summary>
    /// ウィンドウハンドルがウィンドウの場合のみイベント処理 (処理しない「false」/処理する「true」)
    /// </summary>
    public bool IsWindowOnlyEventProcessing { get; } = true;
    /// <summary>
    /// 取得するウィンドウイベントの種類 (なし「0」)
    /// </summary>
    public GetWindowEventType GetWindowEventTypeValue = 0;
    /// <summary>
    /// 取得するウィンドウイベントの種類 (なし「0」)
    /// </summary>
    public GetWindowEventType GetWindowEventType
    {
        get
        {
            return GetWindowEventTypeValue;
        }
    }
    /// <summary>
    /// 取得するウィンドウイベントの種類の変更イベントのデータ
    /// </summary>
    public ChangeGetWindowEventTypeData ChangeGetWindowEventTypeData
    {
        get;
    } = new();
    /// <summary>
    /// イベント処理のデータ
    /// </summary>
    public EventProcessingData EventProcessingData
    {
        get;
    } = new();

    /// <summary>
    /// ウィンドウ
    /// </summary>
    private SettingsWindow? window;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PluginProcessing()
    {
        EventProcessingData.EventProcessing += EventProcessingData_EventProcessing;
    }

    /// <summary>
    /// デストラクタ
    /// </summary>
    ~PluginProcessing()
    {
        Dispose(false);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
    }

    /// <summary>
    /// 非公開Dispose
    /// </summary>
    /// <param name="disposing">disposing</param>
    protected virtual void Dispose(
        bool disposing
        )
    {
        if (Disposed)
        {
            return;
        }
        if (disposing)
        {
            Destruction();
        }
        Disposed = true;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="settingDirectory">設定のディレクトリ</param>
    /// <param name="language">言語</param>
    public void Initialize(
        string settingDirectory,
        string language
        )
    {
    }

    /// <summary>
    /// 破棄
    /// </summary>
    public void Destruction()
    {
        if (window != null)
        {
            window.Close();
            window = null;
        }
    }

    /// <summary>
    /// ウィンドウを表示
    /// </summary>
    public void ShowWindow()
    {
        if (window == null)
        {
            window = new(this);
            window.Closed += Window_Closed;
            window.Show();
        }
    }

    /// <summary>
    /// ウィンドウの「Closed」イベント
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Closed(
        object? sender,
        EventArgs e
        )
    {
        try
        {
            window = null;
        }
        catch
        {
        }
    }

    /// <summary>
    /// 「EventProcessing」イベント
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EventProcessingData_EventProcessing(
        object? sender,
        EventProcessingArgs e
        )
    {
        try
        {
            if (e.EventType == EVENT_CONSTANTS.EVENT_SYSTEM_MOVESIZEEND)
            {
            }
            else if (e.EventType == EVENT_CONSTANTS.EVENT_OBJECT_SHOW)
            {
            }

            string path = "";

            try
            {
                _ = NativeMethods.GetWindowThreadProcessId(e.Hwnd, out int id);       // プロセスID
                IntPtr process = NativeMethods.OpenProcess(ProcessAccessFlags.QueryInformation | ProcessAccessFlags.VirtualMemoryRead, false, id);
                if (process != IntPtr.Zero)
                {
                    if (NativeMethods.EnumProcessModules(process, out IntPtr pmodules, (uint)Marshal.SizeOf(typeof(IntPtr)), out _))
                    {
                        StringBuilder getString;
                        getString = new(256);
                        _ = NativeMethods.GetModuleFileNameEx(process, pmodules, getString, getString.Capacity);
                        path = getString.ToString();
                    }
                    NativeMethods.CloseHandle(process);
                }
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(path) == false
                && System.IO.Path.GetFileNameWithoutExtension(path) == "Notepad")
            {
                NativeMethods.SetWindowPos(e.Hwnd, 0, 100, 100, 0, 0, (int)SWP.SWP_NOZORDER | (int)SWP.SWP_NOSIZE);
            }
        }
        catch
        {
        }
    }
}
