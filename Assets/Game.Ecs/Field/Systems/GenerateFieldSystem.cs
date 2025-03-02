namespace Game.Ecs.Field.Systems
{
    using System;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using Runtime.Services.FieldService;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
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
    public class GenerateFieldSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _requestFilter;
        private FieldAspect _fieldAspect;
        private IFieldService _fieldService;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _requestFilter = _world.Filter<GenerateFieldRequest>().End();
            _world.AddComponent<GenerateFieldRequest>(_world.NewEntity());
        }

        public void Run(IEcsSystems systems)
            {
                foreach (var request in _requestFilter)
                {
                    var size = _fieldService.GetFieldSize();
                    for (int i = 0; i < size.x; i++)
                    {
                        for (int j = 0; j < size.y; j++)
                        {
                            ref var fieldCell = ref _world.AddComponent<CellComponent>(_world.NewEntity());
                            fieldCell.position = new Vector2Int(i,j);
                        }
                    }
                }
            }
        }
    }