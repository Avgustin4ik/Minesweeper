namespace Game.Ecs.Ui.FieldView
{
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Gameplay/HudFeature")]
    public class FieldViewFeature : BaseLeoEcsFeature
    {
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new CreateFieldViewSystem());
            ecsSystems.Add(new BindCellToViewSystem());
            ecsSystems.Add(new OpenCellByForceSystem());
            ecsSystems.Add(new SetFlagOnCellSystem());
            ecsSystems.Add(new DisplayFlagSystem());
            ecsSystems.Add(new OpenMineCellSystem());
            ecsSystems.Add(new OpenSaveCellSystem());
            ecsSystems.Add(new DEBUG_MarkMinesSystem());
        }
    }
}