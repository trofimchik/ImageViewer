using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Domain.Ports;
using ImageViewer.ViewModels;
using ImageViewer.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Infrastructure.Repositories;
using MediatR;
using ImageViewer.Models;
using System.Reflection;
using Domain.UseCases;
using Domain.Models;
using ImageViewer.Services;

namespace ImageViewer
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var collection = RegisterServices();
            
            var services = collection.BuildServiceProvider();
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = services.GetRequiredService<MainWindowViewModel>(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }

        private static ServiceCollection RegisterServices()
        {
            var collection = new ServiceCollection();
            // Repository
            collection.AddSingleton<IImagesRepository, ImagesRepository>();

            // Mediator
            collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Commands
            collection.AddTransient<IRequestHandler<GetImagesCommand, ImagesAggregate>, GetImagesHandler>();
            
            // Services
            collection.AddSingleton<IImagesService, ImagesService>();
            collection.AddSingleton<IFolderPickerService, FolderPickerService>();
            collection.AddSingleton<IImagesPickerService, ImagesPickerService>();
            
            collection.AddTransient<MainWindowViewModel>();
            return collection;
        }
    }
}