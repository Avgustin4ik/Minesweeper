namespace Game.Ecs.Field.Systems
{
    using System;
    using System.Linq;
    using Cell.Aspects;
    using Cell.Components;
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
    public class OpenSaveCellSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _cellToOpenFilter;
        private CellAspect _cellAspect;
        private EcsFilter _cellFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _cellToOpenFilter = _world.Filter<CellComponent>()
                .Inc<NeighborMinesComponent>()
                .Inc<CellIsOpenComponent>()
                .Exc<MineComponent>()
                .End();

            _cellFilter = _world.Filter<CellComponent>()
                .Exc<CellIsOpenComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var cellToOpen in _cellToOpenFilter)
            {
                ref var neighborMinesComponent = ref _cellAspect.NeighborMines.Get(cellToOpen);
                if (neighborMinesComponent.count > 0) continue;
                
                foreach (var cell in _cellFilter)
                {
                    ref var cellComponent = ref _cellAspect.Cell.Get(cell);
                    ref var cellToOpenComponent = ref _cellAspect.Cell.Get(cellToOpen);
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if(cellComponent.position.x == cellToOpenComponent.position.x + i &&
                               cellComponent.position.y == cellToOpenComponent.position.y + j)
                                _cellAspect.IsOpen.Add(cell);
                        }
                    }
                }
            }
        }
    }
}