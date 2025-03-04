namespace Game.Ecs.GameRules
{
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Field.Systems;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Gameplay/GameRules")]
    public class GameRules : BaseLeoEcsFeature
    {
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new InitGameSystem());
        }

        
    }
}