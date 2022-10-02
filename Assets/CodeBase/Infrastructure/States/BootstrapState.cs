using CodeBase.Infrastructure.AssetsManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string NameScene = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private ServicesLocator _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServicesLocator services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }
        
        public void Enter()
        {
            _sceneLoader.Load(NameScene,  onLoad: EnterLoadLevel);
        }

        private void EnterLoadLevel()
           => _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _services.RegisterSingle(InputService());
            _services.RegisterSingle<IAsset>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService( _services.Single<IGameFactory>(), _services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAsset>()));
        }

        public void Exit()
        {
        }
        
        private IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}