namespace Game.Ecs.Field.Aspects
{
    using System;
    using Game.Ecs.Field.Components;
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
    public class FieldAspect : EcsAspect
    {
        public EcsPool<FieldSizeComponent> FieldSize;
        public EcsPool<GenerateFieldRequest> GenerateFieldRequest;
        public EcsPool<CellComponent> Cell;
    }
}