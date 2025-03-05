namespace Game.Ecs.Field.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Aspects;
    using Cell.Aspects;
    using Cell.Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Runtime.Services.FieldService;
    using UniGame.Core.Runtime.Extension;
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
    public class PlaceMinesSystem : IEcsInitSystem,
        IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _cellFilter;
        private IGameSettingsService _gameSettingsService;
        private FieldAspect _fieldAspect;
        private CellAspect _cellAspect;
        private int _mineCount;
        private Vector2Int _size;
        private EcsFilter _placeRequestFilter;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _placeRequestFilter = _world.Filter<PlaceMinesRequest>()
                .End();

            _cellFilter = _world.Filter<CellComponent>()
                .Exc<OpenCellByClickSelfEvent>()
                .End();
            _size = _gameSettingsService.GetFieldSize;
            _mineCount = _gameSettingsService.MinesCount;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _placeRequestFilter)
            {
                ref var cell = ref _world.GetPool<PlaceMinesRequest>().Get(entity);
                PlaceMines(cell.savePosition.x, cell.savePosition.y).Forget();
                _fieldAspect.PlaceMinesRequest.Del(entity);
                break;
            }
        }
        
        

        private UniTaskVoid PlaceMines(int clickedX, int clickedY)
        {
            var reserved = new List<(int, int)>(9){ (clickedX, clickedY) };
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    var nx = clickedX + dx;
                    var ny = clickedY + dy;
                    if (nx >= 0 && nx < _size.x && ny >= 0 && ny < _size.y)
                    {
                        reserved.Add((nx, ny));
                    }
                }
            }
            
            var minesPlaced = 0;
            var span = new Span<int>(new int[_size.x * _size.y]);
            for (int i = 0; i < span.Length; i++)
            {
                span[i] = i;
            }
            span.Shuffle();
            
            var index = 0;
            while (minesPlaced < _mineCount)
            {
                var x = span[index] % _size.x;
                var y = span[index] / _size.x;
                index++;
                var pos = (x, y);
                if(reserved.Contains(pos)) continue;

                foreach (var cell in _cellFilter)
                {
                    ref var cellComponent = ref _cellAspect.Cell.Get(cell);
                    
                    if (cellComponent.position.x == x && cellComponent.position.y == y)
                    {
                        _cellAspect.Mine.Add(cell);
                        minesPlaced++;
                    }
                }
            }
            return default;
        }
    }
}