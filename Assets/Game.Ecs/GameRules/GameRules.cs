namespace Game.Ecs.GameRules
{
    using Components;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Field.Systems;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
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
            ecsSystems.DelHere<RestartGameEvent>();
            ecsSystems.Add(new RestartGameSystem());
            ecsSystems.Add(new InitGameSystem());
            ecsSystems.Add(new LooseGameSystem());
            ecsSystems.Add(new WinGameSystem());
            ecsSystems.Add(new CleanupByRestartCellsSystem()); 
            ecsSystems.Add(new CleanupByRestartGameStateSystem());

        }
    }
}