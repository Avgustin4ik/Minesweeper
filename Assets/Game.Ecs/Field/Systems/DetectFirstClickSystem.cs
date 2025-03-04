namespace Game.Ecs.Field.Systems
{
    using System;
    using Aspects;
    using Cell.Components;
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
    public class DetectFirstClickSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _clickedCellFilter;
        private EcsFilter _minedCellsFilter;
        private FieldAspect _fieldAspect;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _clickedCellFilter = _world.Filter<OpenCellForceComponent>()
                .Inc<CellComponent>()
                .End();
            _minedCellsFilter = _world.Filter<CellComponent>()
                .Inc<MineComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _clickedCellFilter)
            {
                if(_minedCellsFilter.Has()) continue;
                ref var component = ref _fieldAspect.PlaceMinesRequest.Add(_world.NewEntity());
                ref var cell = ref _world.GetPool<CellComponent>().Get(entity);
                component.savePosition = cell.position;
            }
        }
    }
}