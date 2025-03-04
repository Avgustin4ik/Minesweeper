namespace Game.Ecs.GameRules.Systems
{
    using System;
    using Aspects;
    using Components;
    using Field.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

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
    public class LooseGameSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _eventFilter;
        private EcsFilter _looseFilter;
        private GameRulesAspect _gameRulesAspect;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventFilter = _world.Filter<ExplosionEvent>().End();
            _looseFilter = _world.Filter<LooseGameComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            if(_eventFilter.Has() == false) return;
            if(_looseFilter.Has()) return;
            _gameRulesAspect.LooseGame.Add(_world.NewEntity());
        }
    }
}