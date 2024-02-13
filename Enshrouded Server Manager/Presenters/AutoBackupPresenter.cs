﻿using Enshrouded_Server_Manager.Events;
using Enshrouded_Server_Manager.Helpers;
using Enshrouded_Server_Manager.Models;
using Enshrouded_Server_Manager.Services;
using Enshrouded_Server_Manager.Views;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Enshrouded_Server_Manager.Presenters;
public class AutoBackupPresenter
{
    private readonly IAutoBackupView _autoBackupView;
    private readonly IEventAggregator _eventAggregator;
    private readonly IProfileService _profileManager;
    private readonly IFileSystemService _fileSystemService;
    private readonly IMessageBoxService _messageBox;
    private readonly IBackupService _backupService;

    private BindingList<ServerProfile>? _profiles;

    public AutoBackupPresenter(IAutoBackupView autoBackupView,
        IEventAggregator eventAggregator,
        IProfileService profileManager,
        IFileSystemService fileSystemManager,
        IMessageBoxService messageBox,
        IBackupService backupService,
        BindingList<ServerProfile>? serverProfiles)
    {
        _autoBackupView = autoBackupView;
        _eventAggregator = eventAggregator;
        _profileManager = profileManager;
        _fileSystemService = fileSystemManager;
        _messageBox = messageBox;
        _backupService = backupService;

        _profiles = serverProfiles;

        _autoBackupView.SaveAutoBackupSettingsClicked += OnSaveAutoBackupSettingsClicked;

        _eventAggregator.Subscribe<AutoBackupSuccessMessage>(n => OnAutoBackupSuccess(n.ProfileName));
        _eventAggregator.Subscribe<ProfileSelectedMessage>(s => OnProfileSelected(s.SelectedProfile));
    }

    private void OnAutoBackupSuccess(string profileName)
    {
        _autoBackupView.OnAutoBackupSuccess();
        _autoBackupView.UpdateBackupInfo(profileName, _backupService.GetBackupCount(profileName), _backupService.GetDiskConsumption(profileName));
    }

    private void OnSaveAutoBackupSettingsClicked(object? sender, EventArgs e)
    {

        if (_autoBackupView.SelectedProfile is not null)
        {

            _autoBackupView.SelectedProfile.AutoBackup = new AutoBackup()
            {
                Interval = _autoBackupView.BackupInterval,
                MaxiumBackups = _autoBackupView.MaxAutoBackupCount,
                Enabled = _autoBackupView.IsAutoBackupEnabled
            };

            // write the new profile to the json file
            _fileSystemService.WriteFile(
                Path.Join(Constants.Paths.DEFAULT_PROFILES_DIRECTORY, Constants.Files.SERVER_PROFILES_JSON),
                JsonConvert.SerializeObject(_profiles, JsonSettings.Default));

            _eventAggregator.Publish(new AutoBackupSuccessMessage(_autoBackupView.SelectedProfile.Name));
        }
        else
        {
            _messageBox.Show(Constants.Errors.NO_PROFILE_SELECTED_ERROR_MESSAGE, Constants.Errors.NO_PROFILE_SELECTED_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OnProfileSelected(ServerProfile selectedProfile)
    {
        // get selected item
        if (selectedProfile is not null)
        {
            _autoBackupView.SelectedProfile = selectedProfile;

            // load auto backup settings
            if (selectedProfile.AutoBackup is null)
            {
                // create new auto backup settings
                selectedProfile.AutoBackup = new AutoBackup()
                {
                    Interval = 0,
                    MaxiumBackups = 0,
                    Enabled = false
                };
            }

            // set values
            _autoBackupView.IsAutoBackupEnabled = selectedProfile.AutoBackup.Enabled;
            _autoBackupView.BackupInterval = selectedProfile.AutoBackup.Interval;
            _autoBackupView.MaxAutoBackupCount = selectedProfile.AutoBackup.MaxiumBackups;

            _autoBackupView.UpdateBackupInfo(selectedProfile.Name, _backupService.GetBackupCount(selectedProfile.Name), _backupService.GetDiskConsumption(selectedProfile.Name));
        }
        else
        {
            _autoBackupView.IsAutoBackupStatsVisible = false;
            _autoBackupView.IsAutoBackupEnabled = false;
            _autoBackupView.BackupInterval = 0;
            _autoBackupView.MaxAutoBackupCount = 0;
        }
    }
}
