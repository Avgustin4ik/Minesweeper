namespace Game.Ecs.GameRules.Systems
{
    using System;
    using System.Linq;
    using Aspects;
    using Components;
    using Field.Cell.Aspects;
    using Field.Cell.Components;
    using Leopotam.EcsLite;
    using Runtime.Services.FieldService;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RestartGameSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _eventFilter;
        private EcsFilter _cellFilter;
        private IGameSettingsService _gameSettings;
        private GameRulesAspect _gameRulesAspect;
        private CellAspect _cellAspect;
        private EcsFilter _requestFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventFilter = _world.Filter<RestartGameEvent>().End();
            
            _cellFilter = _world.Filter<CellComponent>().End();

            _requestFilter = _world.Filter<RequestToRestartGame>().End();
        }

        public void Run(IEcsSystems systems)
        {
            if (Input.GetKeyUp(_gameSettings.RestartKey))
            {
                GameLog.Log("Restart Game", Color.red);
                _gameRulesAspect.RestartGameEvent.Add(_world.NewEntity());
                return;
            }

            foreach (var request in _requestFilter)
            {
                _gameRulesAspect.RestartGameEvent.Add(_world.NewEntity());
                _gameRulesAspect.RequestToRestartGame.Del(request);
            }
        }
    }
}