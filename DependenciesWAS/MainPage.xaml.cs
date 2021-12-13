﻿using Dependencies.Properties;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Dependencies
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page, INotifyPropertyChanged
	{
		public MainPage()
		{
			this.InitializeComponent();

			var window = new DependencyWindow("coreclr.dll");
			window.Header = "Test";
			FileTabs.TabItems.Add(window);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void SetStatusBarMessage(string message)
		{
			AppStatusBar.Message = message;
		}
		
		/// <summary>
		/// Open a new depedency tree window on a given PE.
		/// </summary>
		/// <param name="Filename">File path to a PE to process.</param>
		public void OpenNewDependencyWindow(String Filename)
		{
			var newDependencyWindow = new DependencyWindow(Filename);
			newDependencyWindow.Header = Path.GetFileNameWithoutExtension(Filename);

			FileTabs.TabItems.Add(newDependencyWindow);
			FileTabs.SelectedItem = newDependencyWindow;

			// Update recent files entries
#if TODO
			App.AddToRecentDocuments(Filename);
			PopulateRecentFilesMenuItems();
#endif
		}

		private async void OpenItem_Click(object sender, RoutedEventArgs e)
		{
			FileOpenPicker loadPicker = new FileOpenPicker();

			loadPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
			loadPicker.FileTypeFilter.Add("*");

			WinRT.Interop.InitializeWithWindow.Initialize(loadPicker, MainWindow.GetWindowHandle());

			StorageFile loadFile = await loadPicker.PickSingleFileAsync();

			if (loadFile == null)
				return;

			OpenNewDependencyWindow(loadFile.Path);

		}

		private async void ExitItem_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.GetWindow().Close();

		}

		private void FileTabs_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
		{
			FileTabs.TabItems.Remove(args.Item);
		}
		private void FileTabs_TabItemsChanged(TabView sender, IVectorChangedEventArgs args)
		{
			this.DefaultMessage.Visibility = FileTabs.TabItems.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
		}

		bool FullPathSetting { get => Settings.Default.FullPath; set { Settings.Default.FullPath = value; OnPropertyChanged(); } }
		bool UndecorateSetting { get => Settings.Default.Undecorate; set { Settings.Default.Undecorate = value; OnPropertyChanged(); } }

	}
}
