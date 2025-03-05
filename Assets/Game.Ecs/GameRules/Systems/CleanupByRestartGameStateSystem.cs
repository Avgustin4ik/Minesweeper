namespace Game.Ecs.GameRules.Systems
{
    using System;
    using System.Linq;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
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
    public class CleanupByRestartGameStateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _eventFilter;
        private EcsFilter _winFilter;
        private EcsFilter _looseFilter;
        private GameRulesAspect _gameRulesAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventFilter = _world.Filter<RestartGameEvent>()
                .End();
            _looseFilter = _world.Filter<LooseGameComponent>()
                .End();
            _winFilter = _world.Filter<WinGameComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _eventFilter)
            {
                foreach (var loose in _looseFilter)
                {
                    _gameRulesAspect.LooseGame.TryRemove(loose);
                }
                foreach (var win in _winFilter)
                {
                    _gameRulesAspect.WinGame.TryRemove(win);
                }
            }

        }
    }
}