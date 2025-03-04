namespace Game.Ecs.Ui.FieldView.Systems
{
    using System;
    using System.Linq;
    using Field.Cell.Components;
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
    public class DEBUG_MarkMinesSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _cellFilter;
        private EcsFilter _viewFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _cellFilter = _world.Filter<CellComponent>().Inc<MineComponent>().End();
            _viewFilter = _world.ViewFilter<CellViewModel>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var view in _viewFilter)
            {
                foreach (var cell in _cellFilter)
                {
                    ref var cellComponent = ref _world.GetPool<CellComponent>().Get(cell);
                    var cellViewModel = _world.GetViewModel<CellViewModel>(view);
                    if (cellComponent.position.x == cellViewModel.position.x &&
                        cellComponent.position.y == cellViewModel.position.y)
                    {
                        cellViewModel.DEBUG_isBomb.Value = true;
                    }
                }
            }

        }
    }
}