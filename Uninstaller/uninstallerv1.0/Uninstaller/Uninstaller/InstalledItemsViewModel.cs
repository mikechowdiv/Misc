﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Deployment.WindowsInstaller;
using NLog;
using System.Configuration;

namespace Uninstaller
{
    public class InstalledItemsViewModel : INotifyPropertyChanged
    {
        private readonly Logger _logger = LogManager.GetLogger("log");
        private readonly InstalledItemsView _view;

        public ICommand UninstallCommand { get; set; }
        public ICommand CancelUninstallCommand { get; set; }
        public ICommand SelectCommand { get; set; }

        private ICollectionView _filteredItems;
        public ICollectionView FilteredItems
        {
            get { return _filteredItems; }
            private set
            {
                _filteredItems = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<InstalledItem> _items; 
        public  ObservableCollection<InstalledItem> Items 
        {
            get { return _items; }
            private set
            {
                _items = value;
                OnPropertyChanged();
            } 
        }

        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged();
            }
        }

        private int _uninstallCurrent;
        public int UninstallCurrent
        {
            get { return _uninstallCurrent; }
            set
            {
                _uninstallCurrent = value;
                OnPropertyChanged();
            }
        }

        private int _uninstallTotal;
        public int UninstallTotal
        {
            get { return _uninstallTotal; }
            set
            {
                _uninstallTotal = value;
                OnPropertyChanged();
            }
        }

        private int _uninstallProgress;
        public int UninstallProgress
        {
            get { return _uninstallProgress; }
            set
            {
                _uninstallProgress = value;
                OnPropertyChanged();
            }
        }

        private bool _uninstallInProgress;
        public bool UninstallInProgress
        {
            get { return _uninstallInProgress; }
            set
            {
                _uninstallInProgress = value;
                OnPropertyChanged();
            }
        }

        private bool _cancelling;
        public bool Cancelling
        {
            get { return _cancelling; }
            set
            {
                _cancelling = value;
                OnPropertyChanged();
            }
        }

        public InstalledItemsViewModel(InstalledItemsView view)
        {
            _logger.Info("Startup");
            _view = view;

            UninstallCommand       = new DelegateCommand(Uninstall);
            CancelUninstallCommand = new DelegateCommand(CancelUninstall);
            SelectCommand          = new DelegateCommand(SelectForUninstall);

            LoadUninstallableItems();

            Observable.FromEventPattern<PropertyChangedEventArgs>(this, "PropertyChanged")
                      .Where(x => x.EventArgs.PropertyName == "FilterText")
                      .Subscribe(_ => FilteredItems.Refresh());
        }
        
        private void SelectForUninstall()
        {
            foreach (var row in _view.DataGrid.SelectedItems.Cast<InstalledItem>())
                row.Uninstall = true;
        }

        private void CancelUninstall()
        {
            _logger.Info("Canceling uninstall");
            Cancelling          = true;
            UninstallInProgress = false;
        }

        private void LoadUninstallableItems()
        {
            var uninstallProductNames = ConfigurationManager.AppSettings["UninstallProductNames"] as string;
            var uninstallProductNameList = new ObservableCollection<string>(uninstallProductNames.ToLower().Split(','));

            var installations = ProductInstallation.GetProducts(null, "s-1-1-0", UserContexts.All)
                                                   .Where(ins => ins.ProductName != null 
                                                   && uninstallProductNameList.Contains(ins.ProductName.ToLower()))
                                                   .Select(ins => new InstalledItem(ins.ProductName,
                                                                                    ins.ProductCode,
                                                                                    ins.InstallDate,
                                                                                    ins.InstallLocation,
                                                                                    ins.UrlInfoAbout,
                                                                                    GetProductVersion(ins)))
                                                   .OrderBy(ins => ins.Product);

            _logger.Info("Found {0} installed items", installations.Count());
            Items = new ObservableCollection<InstalledItem>(installations);
            FilteredItems = CollectionViewSource.GetDefaultView(Items);
            FilteredItems.Filter = ItemFilter();
            FilteredItems.Refresh();

            Cancelling          = false;
            UninstallInProgress = false;
            UninstallCurrent    = 0;
            UninstallTotal      = 0;
            UninstallProgress   = 0;
        }

        private Version GetProductVersion(ProductInstallation ins)
        {
            try
            {
                return ins.ProductVersion;
            }
            catch (Exception)
            {
                _logger.Warn("Count not determine product version for '{0}'", ins.ProductName);
                return new Version(0, 0, 0, 0);
            }
        }

        private Predicate<object> ItemFilter()
        {
            return item => String.IsNullOrWhiteSpace(FilterText) || ((InstalledItem) item).Product.ToUpper().Contains(FilterText.ToUpper());
        }

        private void Uninstall()
        {
            var itemsToUninstall = Items.Where(i => i.Uninstall)
                                        .ToList();

            var count = itemsToUninstall.Count;
            if (count == 0)
                return;

            string plural = count > 1 ? "s" : String.Empty;

            Task<MessageDialogResult> task = _view.ShowMessageAsync(String.Format("Uninstall {0} item{1}", count, plural),
                                                                    String.Format("Are you sure you want to uninstall the selected {0} item{1}?", count, plural),
                                                                    MessageDialogStyle.AffirmativeAndNegative);

            task.ContinueWith(t =>
            {
                try
                {
                    if (t.Result == MessageDialogResult.Negative)
                        return;
                    
                    _logger.Info("Uninstalling {0} item{1}: {2}", count, plural, String.Join(",", itemsToUninstall));
                    UninstallInProgress = true;
                    UninstallTotal = count;
                    foreach (InstalledItem item in itemsToUninstall.TakeWhile(item => !Cancelling))
                        Uninstall(item);

                    LoadUninstallableItems();
                }
                catch (Exception e)
                {
                    _logger.Error("Error uninstalling:", e);
                }
            });
        }

        private void Uninstall(InstalledItem item)
        {
            _logger.Info("Uninstalling {0}", item);
            UninstallCurrent++;
            UninstallProgress = UninstallCurrent * 100 / UninstallTotal;

            Installer.SetInternalUI(InstallUIOptions.ProgressOnly | InstallUIOptions.SourceResolutionOnly | InstallUIOptions.UacOnly);
            Installer.ConfigureProduct(item.ProductCode, 0, InstallState.Absent, "IGNOREDEPENDENCIES=\"ALL\"");

            _logger.Info("Uninstalled {0}", item);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
