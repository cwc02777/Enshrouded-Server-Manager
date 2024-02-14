﻿using Enshrouded_Server_Manager.UI;
using Enshrouded_Server_Manager.Views;
using System.ComponentModel;

namespace Enshrouded_Server_Manager;
public partial class RestoreBackupView : UserControl, IRestoreBackupView
{
    public RestoreBackupView()
    {
        InitializeComponent();
    }

    public string RestoreFilePath
    {
        get => txtRestoreFilePath.Text;
        set => txtRestoreFilePath.Text = value;
    }

    public event EventHandler RestoreSelectedFileClicked
    {
        add => btnRestoreSaveFile.Click += value;
        remove => btnRestoreSaveFile.Click -= value;
    }

    public event EventHandler SelectFileToRestoreClicked
    {
        add => btnSelectRestoreFile.Click += value;
        remove => btnSelectRestoreFile.Click -= value;
    }

    public event CancelEventHandler FileToRestoreSelected
    {
        add => ofdBackupFileSelector.FileOk += value;
        remove => ofdBackupFileSelector.FileOk -= value;
    }

    public OpenFileDialog FileDialog => ofdBackupFileSelector;

    public void AnimateSaveButton()
    {
        Interactions.AnimateSaveChangesButton(btnRestoreSaveFile, btnRestoreSaveFile.Text, Constants.ButtonText.RESTORED_SUCCESS);
    }

    private void btnSaveAutoBackup_EnabledChanged(object sender, EventArgs e)
    {
        var button = ((Button)sender);
        Interactions.HandleEnabledChanged_PrimaryButton(button);
    }
}
