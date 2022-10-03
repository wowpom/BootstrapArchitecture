using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_loadingCurtainPrefab));
            _game.GameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}