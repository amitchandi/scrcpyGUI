using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace scrcpyGUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand UseCustomArgumentsCommand { get; }
    public bool IsScreenTurnedOff { get; set; }
    public bool CaptureCamera { get; set; }
    public bool ReadOnly { get; set; }
    public bool PrintFPS { get; set; }
    public bool UseCustomArgumentsChecked { get; set; }
    public string CustomArgumentsValue { get; set; } = "";
    public string PushTargetDir { get; set; } = "/sdcard/Download/";

    public TextBox CustomArguments { get; set; }

    readonly Process proc = new();

    public MainViewModel()
    {
        StartCommand = ReactiveCommand.Create(() =>
        {
            //SETUP ALL NEEDED VARS
            string WorkingDir = Directory.GetCurrentDirectory();
            string ExecutablePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\scrcpy\scrcpy.exe";

            proc.StartInfo.FileName = ExecutablePath;
            proc.StartInfo.WorkingDirectory = WorkingDir;

            proc.StartInfo.ArgumentList.Add($"--push-target={PushTargetDir}");

            if (IsScreenTurnedOff)
                proc.StartInfo.ArgumentList.Add("-S");

            if (CaptureCamera)
                proc.StartInfo.ArgumentList.Add("--video-source=camera");

            if (ReadOnly)
                proc.StartInfo.ArgumentList.Add("-n");

            if (UseCustomArgumentsChecked)
                proc.StartInfo.ArgumentList.Add("--print-fps");

            if (UseCustomArgumentsChecked)
            {
                proc.StartInfo.Arguments = CustomArgumentsValue;
            }


            //proc.StartInfo.UseShellExecute = true;

            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;

            proc.Start();
        });


        StopCommand = ReactiveCommand.Create(() =>
        {
            proc.CloseMainWindow();
            proc.Close();
        });
    }
}
