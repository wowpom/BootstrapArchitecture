using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapperPrefab;

        private void Awake()
        {
            var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            if (gameBootstrapper == null)
                Instantiate(_gameBootstrapperPrefab);
        }
    }
}