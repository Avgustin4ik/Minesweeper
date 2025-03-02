namespace Game.Ecs.Ui.FieldView.Systems
{
    using System;
    using System.Linq;
    using Core.Components;
    using Field.Aspects;
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
    public class BindCellToViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _viewFilter;
        private EcsFilter _cellFilter;
        private FieldAspect _fieldAspect;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _viewFilter = _world.ViewFilter<CellViewModel>().Exc<OwnerComponent>().End();
            _cellFilter = _world.Filter<CellComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var viewEntity in _viewFilter)
            {
                var model = _world.GetViewModel<CellViewModel>(viewEntity);
                
                foreach (var cellEntity in _cellFilter)
                {
                    ref var cell = ref _fieldAspect.Cell.Get(cellEntity);
                    
                    if (cell.position != model.position) continue;
                    
                    model.DEBUG_position.Value = cell.position;
                    
                    ref var ownerComponent = ref _world.AddComponent<OwnerComponent>(viewEntity);
                    ownerComponent.Value = cellEntity.PackedEntity(_world);
                    break;
                }
            }
        }
    }
}