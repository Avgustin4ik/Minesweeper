namespace Game.Runtime.Services.FieldService
{
    using System;
    using UniGame.UniNodes.GameFlow.Runtime;
    using UnityEngine;

    [Serializable]
    public class GameSettingsService : GameService, IGameSettingsService
    {
        private readonly FieldSettings _settings;
        private Vector2Int _fieldSize;
        private readonly GameSettings _gameSettings;
        private readonly FieldSettings _fieldSettings;

        public GameSettingsService(FieldSettings settings, GameSettings gameSettings)
        {
            _settings = settings;
            _fieldSettings = settings;
            _gameSettings = gameSettings;
            _fieldSize = new Vector2Int(_settings.width, _settings.height);
        }

        public Vector2Int GetFieldSize => _fieldSize;
        public KeyCode RestartKey => _gameSettings.restartKey;

        public int MinesCount =>
            _fieldSettings.setMinesManually
                ? _fieldSettings.minesCount
                : _fieldSize.x * _fieldSize.y / 5;
    }
}