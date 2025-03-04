namespace Game.Ecs.Field.Systems
{
    using System;
    using Aspects;
    using Cell.Aspects;
    using Cell.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine;

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
    public class OpenMinceCellSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private FieldAspect _aspect;
        private CellAspect _cellAspect;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<CellComponent>()
                .Inc<OpenCellForceComponent>()
                .Inc<MineComponent>()
                .Exc<CellIsOpenComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var cellEntity in _filter)
            {
                Debug.Log("Game Over");
                _aspect.ExplosionEvent.Add(_world.NewEntity());
                _cellAspect.IsOpen.Add(cellEntity);
            }
        }
    }
}