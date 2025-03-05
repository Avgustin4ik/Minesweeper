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
    public class GameSettingsServiceSource : DataSourceAsset<IGameSettingsService>
    {
        public FieldSettings settings;
        public GameSettings gameSettings;
        protected override async UniTask<IGameSettingsService> CreateInternalAsync(IContext context)
        {
            return new GameSettingsService(settings, gameSettings);
        }
    }
}