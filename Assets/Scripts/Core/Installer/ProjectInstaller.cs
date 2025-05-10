using Core.FSM;
using Core.Input;
using Core.Pool;
using Core.Settings;
using Modules.Figure;
using Modules.Figure.Bar;
using Modules.Figure.Container;
using Modules.Figure.Model;
using Modules.UI.Main;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Installer
{
    public sealed class ProjectInstaller : LifetimeScope
    {
        [SerializeField] private Camera _sceneCamera;

        [SerializeField] private PrefabsTable _prefabsTable;
        [SerializeField] private GameplaySettings _gameplaySettings;

        protected override void Configure(IContainerBuilder builder)
        {
            Application.targetFrameRate = 60;

            builder.RegisterInstance(_prefabsTable);
            builder.RegisterInstance(_gameplaySettings);

            builder.Register<FiguresModel>(Lifetime.Singleton)
                .AsSelf();

            RegisterScene(builder);
            RegisterUI(builder);
            RegisterFSM(builder);
        }

        private void RegisterScene(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sceneCamera);

            var figuresContainerView = Instantiate(_prefabsTable.FiguresContainerPrefab);
            builder.RegisterInstance(figuresContainerView)
                .AsSelf();
            builder.Register<FiguresContainerController>(Lifetime.Singleton)
                .AsImplementedInterfaces();

            var figuresBarView = Instantiate(_prefabsTable.FiguresBarViewPrefab, figuresContainerView.FiguresBarPoint,
                false);
            builder.RegisterInstance(figuresBarView)
                .AsSelf();
            builder.Register<FiguresBarController>(Lifetime.Singleton)
                .AsImplementedInterfaces();

            var figuresPool = new PoolSimple<FigureView>(50, () =>
            {
                var view = Instantiate(_prefabsTable.FigureViewPrefab, figuresContainerView.Content);
                view.gameObject.SetActive(false);
                return view;
            });
            builder.RegisterInstance(figuresPool)
                .As<PoolSimple<FigureView>>();
        }

        private void RegisterFSM(IContainerBuilder builder)
        {
            builder.Register<FiniteStateMachine>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }

        private void RegisterUI(IContainerBuilder builder)
        {
            var mainUiView = Instantiate(_prefabsTable.MainScreenPrefab);
            builder.RegisterInstance(mainUiView)
                .AsSelf();
            builder.Register<MainScreenController>(Lifetime.Singleton)
                .AsImplementedInterfaces();

            builder.Register<InputModel>(Lifetime.Singleton)
                .AsSelf();
            var inputView = Instantiate(_prefabsTable.InputViewPrefab);
            builder.RegisterInstance(inputView)
                .AsSelf();
            autoInjectGameObjects.Add(inputView.gameObject);

            var loseScreen = Instantiate(_prefabsTable.LoseScreenPrefab);
            loseScreen.gameObject.SetActive(false);
            builder.RegisterInstance(loseScreen)
                .AsSelf();

            var winScreen = Instantiate(_prefabsTable.WinScreenPrefab);
            winScreen.gameObject.SetActive(false);
            builder.RegisterInstance(winScreen)
                .AsSelf();
        }
    }
}