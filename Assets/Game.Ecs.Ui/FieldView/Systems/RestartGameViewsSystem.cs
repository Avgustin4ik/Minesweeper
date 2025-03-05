namespace Game.Ecs.Ui.FieldView.Systems
{
    using System;
    using System.Linq;
    using Field.Cell.Components;
    using GameRules.Components;
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
    public class RestartGameViewsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _cellViewFilter;
        private EcsFilter _eventFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
             _cellViewFilter = _world.ViewFilter<CellViewModel>()
                .End();
             _eventFilter = _world.Filter<RestartGameEvent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _eventFilter)
            {
                foreach (var cell in _cellViewFilter) 
                {
                    var model = _world.GetViewModel<CellViewModel>(cell);
                    model.reset.Execute();
                }
            }
        }
    }
}