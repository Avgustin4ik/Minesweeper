﻿namespace Game.Ecs.Movement.Components
{
    using System;
    using Unity.Mathematics;
    using UnityEngine.Serialization;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct MovementTargetComponent
    {
        public float3 Value;
    }
}