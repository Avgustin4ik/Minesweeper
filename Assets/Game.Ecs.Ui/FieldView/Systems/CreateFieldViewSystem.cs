namespace Game.Ecs.Ui.FieldView.Systems
{
    using System;
    using System.Linq;
    using Components;
    using Field.Components;
    using Leopotam.EcsLite;
    using Runtime.Services.FieldService;
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
    public class CreateFieldViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _viewFilter;
        private IFieldService _fieldService;
        private EcsFilter _requestFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _viewFilter = _world.ViewFilter<FieldViewModel>().End();
            _requestFilter = _world.Filter<GenerateCellsViewsRequest>().End();
            _world.AddComponent<GenerateCellsViewsRequest>(_world.NewEntity());
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestFilter)
            {
                foreach (var fieldView in _viewFilter)
                {
                    var model = _world.GetViewModel<FieldViewModel>(fieldView);
                    var size = _fieldService.GetFieldSize();
                    model.GenerateField.Execute(new Vector2Int(size.x, size.y));
                    
                    _world.RemoveComponent<GenerateCellsViewsRequest>(requestEntity);
                }
            }
        }
    }
}