﻿namespace Game.Ecs.GameRules.Aspects
{
    using System;
    using Components;
    using Field.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

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
    public class GameRulesAspect : EcsAspect
    {
        public EcsPool<LooseGameComponent> LooseGame;
        public EcsPool<WinGameComponent> WinGame;
        public EcsPool<ExplosionEvent> ExplosionEvent;
        public EcsPool<RestartGameEvent> RestartGameEvent;
        public EcsPool<RequestToRestartGame> RequestToRestartGame;
    }
}