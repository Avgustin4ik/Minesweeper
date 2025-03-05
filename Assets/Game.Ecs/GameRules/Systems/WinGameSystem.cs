namespace Game.Ecs.GameRules.Systems
{
    using System;
    using System.Linq;
    using Aspects;
    using Components;
    using Field.Cell.Components;
    using Leopotam.EcsLite;
    using Runtime.Services.FieldService;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UnityEngine;
    using UnityEngine.Pool;

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
    public class WinGameSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _minesWithFlag;
        private EcsFilter _mines;
        private IFieldService _service;
        private EcsFilter _winEventFilter;
        private GameRulesAspect _gameRulesAspect;
        private EcsFilter _cellsFilter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _winEventFilter = _world.Filter<WinGameComponent>()
                .End();
            _mines = _world.Filter<MineComponent>()
                .Exc<FlagComponent>()
                .End();
            _minesWithFlag = _world.Filter<MineComponent>()
                .Inc<FlagComponent>()
                .End();
            _cellsFilter = _world.Filter<CellComponent>()
                .Exc<CellIsOpenComponent>().Exc<MineComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            if (!_minesWithFlag.Has() || _mines.Has() && _cellsFilter.Has()) return;
            
            if(_winEventFilter.Has()) return;
            GameLog.Log("Win Game, all mines are flagged", Color.green);
            _gameRulesAspect.WinGame.Add(_world.NewEntity());

        }
    }
}