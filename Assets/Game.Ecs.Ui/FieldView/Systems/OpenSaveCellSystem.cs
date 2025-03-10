﻿namespace Game.Ecs.Ui.FieldView.Systems
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
    public class OpenSaveCellSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private CellAspect _cellAspect;
        private EcsFilter _viewFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<CellComponent>()
                .Inc<NeighborMinesComponent>()
                .Inc<CellIsOpenComponent>()
                .Exc<MineComponent>()
                .End();
            
            _viewFilter = _world.ViewFilter<CellViewModel>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var cell in _filter)
            {
                ref var cellComponent = ref _cellAspect.Cell.Get(cell);
                foreach (var view in _viewFilter)
                {
                    var model = _world.GetViewModel<CellViewModel>(view); 
                    if(model.isOpen.Value) continue;
                    
                    if(cellComponent.position != model.position) continue;
                    
                    ref var neighborMines = ref _cellAspect.NeighborMines.Get(cell);
                    model.ShowNeighborMines.Execute(neighborMines.count);
                    model.neighborMines.Value = neighborMines.count;
                    model.isOpen.Value = true;
                }
                
            }
        }
    }
}