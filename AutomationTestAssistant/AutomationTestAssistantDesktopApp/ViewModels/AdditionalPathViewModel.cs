﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATADataModel;
using AutomationTestAssistantCore;

namespace AutomationTestAssistantDesktopApp
{
    public class AdditionalPathViewModel : AdditionalPath, INotifyPropertyChanged
    {
        private string localPath;
        public string LocalPath
        {
            get
            {
                return localPath;
            }
            set
            {
                localPath = value;
                OnPropertyChanged("LocalPath");
            }
        }

        public string UserName { get; set; }

        private void OnPropertyChanged(string property)
        {
            if (!String.IsNullOrEmpty(LocalPath))
            {
                ATACore.RegistryManager.WriterLocalPathToRegistry(UserName, TfsPath, LocalPath);
            }
        }

        public AdditionalPathViewModel(AdditionalPath ap)
        {
            base.TfsPath = ap.TfsPath;
            base.TfsUrl = ap.TfsUrl;
            base.AdditionalPathId = ap.AdditionalPathId;
            WorkspacesForDelete = new List<string>();
        }

        public AdditionalPathViewModel(AdditionalPath ap, string userName)
           :this(ap)
        {
            UserName = userName;
            LocalPath = ATACore.RegistryManager.GetProjectLocalPath(UserName, TfsPath);
            WorkspacesForDelete = new List<string>();
        }

        public List<string> WorkspacesForDelete { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
