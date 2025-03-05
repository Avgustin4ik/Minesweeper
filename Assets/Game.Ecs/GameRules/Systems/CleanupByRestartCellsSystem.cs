namespace Game.Ecs.GameRules.Systems
{
    using System;
    using System.Linq;
    using Components;
    using Field.Aspects;
    using Field.Cell.Aspects;
    using Field.Cell.Components;
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
    public class CleanupByRestartCellsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _cellFilter;
        private CellAspect _cellAspect;
        private FieldAspect _fieldAspect;
        private EcsFilter _eventFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _cellFilter = _world.Filter<CellComponent>()
                .End();
            _eventFilter = _world.Filter<RestartGameEvent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _eventFilter)
            {
                foreach (var cell in _cellFilter)
                {
                    _cellAspect.Mine.TryRemove(cell);
                    _cellAspect.Flag.TryRemove(cell);
                    _cellAspect.OpenCellForce.TryRemove(cell);
                    _cellAspect.IsOpen.TryRemove(cell);
                    _cellAspect.MineScored.TryRemove(cell);
                    _cellAspect.NeighborMines.TryRemove(cell);
                }
            }
        }
    }
}