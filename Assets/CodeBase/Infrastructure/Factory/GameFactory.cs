using System.Collections.Generic;
using CodeBase.Infrastructure.AssetsManagment;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string HudPath = "Hud/Hud";
        private const string HeroPath = "Hero/hero";

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private readonly IAsset _assets;
        
        public GameFactory(IAsset assets)
        {
            _assets = assets;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreateHero(GameObject spawnPosition)
            => InstantiateRegistered(HeroPath, spawnPosition.transform.position);

        public void CreateHud()
            => InstantiateRegistered(HudPath);

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath , at);
            RegisteredProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisteredProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisteredProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}