namespace Game.Ecs.Ui.HUD.Systems
{
    using System;
    using System.Linq;
    using GameRules.Aspects;
    using Leopotam.EcsLite;
    using NAMESPACE.ViewsAndModels;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.Core.Runtime.Extension;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcs.ViewSystem.Extensions;
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
    public class RestartByButtonSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _buttonFilter;
        private GameRulesAspect _gameRules;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _buttonFilter = _world.ViewFilter<RestartButtonViewModel>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var buttonEntity in _buttonFilter)
            {
                var model = _world.GetViewModel<RestartButtonViewModel>(buttonEntity);
                if(!model.pressed.Take()) continue;
                GameLog.Log("Restart Game", Color.red);
                _gameRules.RequestToRestartGame.Add(_world.NewEntity());
            }
        }
    }
}