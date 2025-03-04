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
        private EcsFilter _filter;
        private CellAspect _cellAspect;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<CellComponent>()
                .Inc<OpenCellComponent>()
                .Exc<CellIsOpenComponent>()
                .Exc<MineComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var cell in _filter)
            {
                _cellAspect.IsOpen.Add(cell);
            }
        }
    }
}