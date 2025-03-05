namespace Game.Ecs.Ui.HUD
{
    using Cysharp.Threading.Tasks;
    using Game.Ecs.GameRules.Components;
    using Leopotam.EcsLite;
    using NAMESPACE.ViewsAndModels;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.ViewSystem.Extensions;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Gameplay/HudFeature")]
    public class HudFeature : BaseLeoEcsFeature
    {
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.ShowSingleOn<LooseGameComponent, LooseScreenView>();
            ecsSystems.ShowSingleOn<WinGameComponent, WinScreenView>();
            ecsSystems.CloseOn<RestartGameEvent, LooseScreenViewModel>();
            ecsSystems.CloseOn<RestartGameEvent, WinScrennViewModel>();
            ecsSystems.Add(new RestartByButtonSystem());
        }
    }
}