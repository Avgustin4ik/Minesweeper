namespace Game.Ecs.Ui.FieldView.Systems
{
    using System;
    using System.Linq;
    using Field.Cell.Aspects;
    using Field.Cell.Components;
    using Field.Components;
    using Leopotam.EcsLite;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.ViewSystem.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;
    using ViewsAndModels;

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
    public class OpenMineCellSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _eventFilter;
        private EcsFilter _viewFilter;
        private EcsFilter _minesFilter;
        private CellAspect _cellAspect;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _eventFilter = _world.Filter<ExplosionEvent>()
                .End();
            _minesFilter = _world.Filter<CellComponent>()
                .Inc<MineComponent>()
                .End();
            _viewFilter = _world.ViewFilter<CellViewModel>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _eventFilter)
            {
                foreach (var mine in _minesFilter)
                {
                    ref var mineComponent = ref _cellAspect.Cell.Get(mine);
                    foreach (var view in _viewFilter)
                    {
                        var model = _world.GetViewModel<CellViewModel>(view);
                        if (model.position == mineComponent.position)
                        {
                            model.detonate.Execute();
                        }
                    }
                }
            }
        }
    }
}