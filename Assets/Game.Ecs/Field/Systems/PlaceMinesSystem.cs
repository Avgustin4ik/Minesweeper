namespace Game.Ecs.Field.Systems
{
    using System;
    using System.Collections.Generic;
    using Aspects;
    using Cell.Aspects;
    using Cell.Components;
    using Leopotam.EcsLite;
    using Runtime.Services.FieldService;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

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
        private IFieldService _fieldService;
        private FieldAspect _fieldAspect;
        private CellAspect _cellAspect;
        private int _fieldWidth;
        private int _fieldHeight;
        private int _mineCount;
        private EcsFilter _placeRequestFilter;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _placeRequestFilter = _world.Filter<PlaceMinesRequest>()
                .End();

            _cellFilter = _world.Filter<CellComponent>()
                .Exc<OpenCellForceComponent>()
                .Exc<MineComponent>()
                .End();
            
            _fieldWidth = _fieldService.GetFieldSize().x;
            _fieldHeight = _fieldService.GetFieldSize().y;
            _mineCount = _fieldWidth * _fieldHeight / 10;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _placeRequestFilter)
            {
                ref var cell = ref _world.GetPool<PlaceMinesRequest>().Get(entity);
                PlaceMines(cell.savePosition.x, cell.savePosition.y);
                _fieldAspect.PlaceMinesRequest.Del(entity);
                break;
            }
        }

        private void PlaceMines(int clickedX, int clickedY)
        {
            var reserved = new HashSet<(int, int)>();

            // Резервируем кликнутую ячейку и её соседей
            reserved.Add((clickedX, clickedY));
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int nx = clickedX + dx;
                    int ny = clickedY + dy;
                    if (nx >= 0 && nx < _fieldWidth && ny >= 0 && ny < _fieldHeight)
                    {
                        reserved.Add((nx, ny));
                    }
                }
            }

            // Размещаем мины
            int minesPlaced = 0;
            var random = new System.Random();
            while (minesPlaced < _mineCount)
            {
                int x = random.Next(0, _fieldWidth);
                int y = random.Next(0, _fieldHeight);
                var pos = (x, y);
                if (!reserved.Contains(pos))
                {
                    int cellEntity = FindEntityByPosition(x, y);
                    if (!_cellAspect.Mine.Has(cellEntity))
                    {
                        _cellAspect.Mine.Add(cellEntity);
                        minesPlaced++;
                        reserved.Add(pos); // Чтобы избежать повторений
                    }
                }
            }

            // Рассчитываем количество соседних мин
            CalculateNeighborMines();
        }

        private int FindEntityByPosition(int x, int y)
        {
            foreach (int entity in _cellFilter)
            {
                ref var cell = ref _cellAspect.Cell.Get(entity);
                if (cell.position.x == x && cell.position.y == y) return entity;
            }

            return -1;
        }

        private void CalculateNeighborMines()
        {
            foreach (int entity in _cellFilter)
            {
                ref var cell = ref _cellAspect.Cell.Get(entity);
                int count = 0;
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = cell.position.x + dx;
                        int ny = cell.position.y + dy;
                        if (nx >= 0 && nx < _fieldWidth && ny >= 0 && ny < _fieldHeight)
                        {
                            int neighborEntity = FindEntityByPosition(nx, ny);
                            if (_cellAspect.Mine.Has(neighborEntity))
                            {
                                count++;
                            }
                        }
                    }
                }

                if (count > 0)
                {
                    ref var neighborMines = ref _cellAspect.NeighborMines.Add(entity);
                    neighborMines.count = count;
                }
            }
        }
    }
}