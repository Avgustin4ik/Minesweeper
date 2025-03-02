namespace Game.Runtime.Services.FieldService
{
    using Cysharp.Threading.Tasks;
    using UniGame.Core.Runtime;
    using UniGame.GameFlow.Runtime.Services;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Services/FieldService Source", fileName = "FieldService Source")]
    public class FieldServiceSource : DataSourceAsset<IFieldService>
    {
        public FieldSettings settings;
        protected override async UniTask<IFieldService> CreateInternalAsync(IContext context)
        {
            return new FieldService(settings);
        }
    }
}