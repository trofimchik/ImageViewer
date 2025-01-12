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
using Avalonia.Controls;

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
            var services = ConfigureServices();
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                
                var mainVM = services.GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = mainVM,
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

        private static ServiceProvider ConfigureServices()
        {
            var collection = new ServiceCollection();

            // VMs
            collection.AddSingleton<MainWindowViewModel>();
            collection.AddSingleton<ImagesViewModel>();

            // Views
            //collection.AddSingleton<MainWindow>();
            collection.AddSingleton<Window, MainWindow>();

            // Repository
            collection.AddSingleton<IImagesRepository, ImagesRepository>();

            // Mediator
            collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Commands
            collection.AddSingleton<IRequestHandler<GetImagesCommand, ImagesAggregate?>, GetImagesHandler>();
            collection.AddSingleton<IRequestHandler<SaveImagesCommand, bool>, SaveImagesHandler>();

            // Services
            collection.AddSingleton<IPickerService, PickerService>();
            collection.AddSingleton<IImagesService, ImagesService>();
            
            return collection.BuildServiceProvider();
        }
    }
}