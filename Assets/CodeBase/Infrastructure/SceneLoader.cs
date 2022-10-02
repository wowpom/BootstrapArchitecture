using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
            => _coroutineRunner = coroutineRunner;

        public void Load(string nameScene, Action onLoad = null)
            => _coroutineRunner.StartCoroutine(LoadScene(nameScene, onLoad));
        private IEnumerator LoadScene(string nameScene, Action onLoad = null)
        {
            if (SceneManager.GetActiveScene().name == nameScene)
            {
                onLoad?.Invoke();
                yield break;
            }
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nameScene);

            while (!waitNextScene.isDone)
                yield return null;
            
            onLoad?.Invoke();
        }
    }
}