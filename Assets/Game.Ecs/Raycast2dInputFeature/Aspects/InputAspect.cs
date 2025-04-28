namespace Game.Ecs.Raycast2dInputFeature.Aspects
{
    using System;
    using InputFeature;
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
    public class InputAspect : EcsAspect
    {
        public EcsPool<LMBClickSelfEvent> LMBClick;
        public EcsPool<RMBClickSelfEvent> RMBClick;
        public EcsPool<RaycastHitSelfEvent> RaycastHit;
    }
}