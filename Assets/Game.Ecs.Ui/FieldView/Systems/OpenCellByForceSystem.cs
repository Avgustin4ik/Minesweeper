namespace Game.Ecs.Ui.FieldView.Systems
{
    using System;
    using System.Linq;
    using Field.Cell.Aspects;
    using Field.Cell.Components;
    using Leopotam.EcsLite;
    using UniCore.Runtime.ProfilerTools;
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
    public class OpenCellByForceSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _viewFilter;
        private EcsFilter _cellFilter;
        private CellAspect _cellAspect;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _viewFilter = _world.ViewFilter<CellViewModel>().End();
            _cellFilter = _world.Filter<CellComponent>().Exc<OpenCellForceComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var view in _viewFilter)
            {
                var model = _world.GetViewModel<CellViewModel>(view);
                if(!model.leftClick.Take()) continue;
                foreach (var cell in _cellFilter)
                {
                    ref var cellComponent = ref _world.GetPool<CellComponent>().Get(cell);
                    
                    if (cellComponent.position != model.position) continue;
                    _cellAspect.OpenCellForce.Add(cell);
                GameLog.LogWarning("OpenCellByForceSystem"); 
                    break;
                }
            }
        }
    }
}