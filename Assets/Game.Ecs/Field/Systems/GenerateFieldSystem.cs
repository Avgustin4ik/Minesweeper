namespace Game.Ecs.Field.Systems
{
    using System;
    using System.Threading.Tasks;
    using Cell;
    using Cell.Aspects;
    using Cell.Components;
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Runtime.Services.FieldService;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using Unity.Mathematics;
    using UnityEngine;
    using Object = UnityEngine.Object;

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
        private CellAspect _aspect;
        private IGameSettingsService _gameSettingsService;
        private readonly LeoEcsMonoConverter _cellPrefab;

        public GenerateFieldSystem(LeoEcsMonoConverter cellPrefab)
        {
            _cellPrefab = cellPrefab;
        }

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
                var size = _gameSettingsService.GetFieldSize;
                for (var i = 0; i < size.x; i++)
                {
                    for (var j = 0; j < size.y; j++)
                    {
                        var cellIndex = new Vector2Int(i, j);
                        var offset = _gameSettingsService.Offset;
                        SpawnAndInitializeCell(cellIndex, offset).Forget();

                    }
                }
            }
        }

        private async UniTaskVoid SpawnAndInitializeCell(Vector2Int cellIndex, float offset)
        {
            var cell = await SpawnCell();
            InitializeAndPlace(cell, cellIndex, offset);
        }

        private void InitializeAndPlace(LeoEcsMonoConverter cell, Vector2Int cellIndex, float offset)
        {
            var entity = cell.Entity;
            ref var cellComponent = ref _world.GetOrAddComponent<CellComponent>(entity);
            cellComponent.position = cellIndex;
            cell.gameObject.transform.position = new Vector3(
                cellIndex.x * (cellComponent.size.x + offset),
                cellIndex.y * (cellComponent.size.y + offset),
                0
            );
        }

        private async UniTask<LeoEcsMonoConverter> SpawnCell()
        {
            var go = await Object.InstantiateAsync(_cellPrefab.gameObject);
            foreach (var gameObject in go)
            {
                var leoEcsMonoConverter = gameObject.GetComponent<LeoEcsMonoConverter>();
                leoEcsMonoConverter.Convert(_world, _world.NewEntity());
                return leoEcsMonoConverter;
            }
            
            return null;
        }
    }
}