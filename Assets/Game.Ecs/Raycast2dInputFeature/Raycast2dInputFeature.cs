namespace Game.Ecs.Raycast2dInputFeature
{
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Raycast2dInputFeature.Systems;
    using InputFeature;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Gameplay/Raycast2dInputFeature")]
    public class Raycast2dInputFeature : BaseLeoEcsFeature
    {
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.DelHere<RaycastHitSelfEvent>();
            ecsSystems.DelHere<LMBClickSelfEvent>();
            ecsSystems.DelHere<RMBClickSelfEvent>();
            ecsSystems.Add(new RayCastToEntitySystem());
        }
    }
}