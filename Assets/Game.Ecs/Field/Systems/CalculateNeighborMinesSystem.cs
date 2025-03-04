namespace Game.Ecs.Field.Systems
{
    using System;
    using System.Linq;
    using Cell.Aspects;
    using Cell.Components;
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
    public class CalculateNeighborMinesSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _minesFilter;
        private EcsFilter _cellFilter;
        private CellAspect _cellAspect;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _minesFilter = _world.Filter<CellComponent>()
                .Inc<MineComponent>()
                .Exc<MineScoredComponent>()
                .End();
            _cellFilter = _world.Filter<CellComponent>().Exc<MineComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var mine in _minesFilter)
            {
                ref var mineComponent = ref _world.GetPool<CellComponent>().Get(mine);
                foreach (var cell in _cellFilter)
                {
                    ref var cellComponent = ref _world.GetPool<CellComponent>().Get(cell);
                    ref var neighborMinesComponent = ref _cellAspect.NeighborMines.GetOrAddComponent(cell);
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (cellComponent.position.x == mineComponent.position.x + i &&
                                cellComponent.position.y == mineComponent.position.y + j)
                            {
                                neighborMinesComponent.count++;
                            }
                        }
                    }
                }
                _cellAspect.MineScored.Add(mine);
            }
        }
    }
}