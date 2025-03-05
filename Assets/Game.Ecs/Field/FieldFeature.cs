namespace Game.Ecs.Field
{
    using Cell.Components;
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Field.Components;
    using Game.Ecs.Field.Systems;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Runtime.Services.FieldService;
    using UniGame.Context.Runtime.Extension;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Gameplay/FiledFeature")]
    public class FieldFeature : BaseLeoEcsFeature
    {
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            var context = ecsSystems.GetShared<IContext>();
            var service = await context.ReceiveFirstAsync<IFieldService>();
            var world = ecsSystems.GetWorld();
            world.SetGlobal(service);
            
            ecsSystems.Add(new GenerateFieldSystem());
            ecsSystems.DelHere<GenerateFieldRequest>();
            ecsSystems.Add(new PlaceMinesSystem());
            ecsSystems.Add(new DetectFirstClickSystem());
            ecsSystems.Add(new CalculateNeighborMinesSystem());
            ecsSystems.Add(new OpenSaveCellSystem());
            ecsSystems.DelHere<ExplosionEvent>();
            ecsSystems.Add(new OpenMineCellSystem());
            ecsSystems.Add(new OpenCellSystem());
            ecsSystems.DelHere<OpenCellForceComponent>();
            ecsSystems.Add(new PlaceMinesSystem());
            

        }
    }
}