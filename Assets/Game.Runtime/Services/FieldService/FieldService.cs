namespace Game.Runtime.Services.FieldService
{
    using System;
    using UniGame.UniNodes.GameFlow.Runtime;
    using Unity.Mathematics;
    using UnityEngine;

    [Serializable]
    public class FieldService : GameService, IFieldService
    {
        private readonly FieldSettings _settings;
        private int2 _fieldSize;
        private readonly GameSettings _gameSettings;
        private readonly FieldSettings _fieldSettings;

        public FieldService(FieldSettings settings, GameSettings gameSettings)
        {
            _settings = settings;
            _fieldSettings = settings;
            _gameSettings = gameSettings;
            _fieldSize = new int2(_settings.width, _settings.height);
        }

        public int2 GetFieldSize() => _fieldSize;
        public KeyCode RestartKey => _gameSettings.restartKey;
        public int MinesCount => _fieldSettings.minesCount;
    }
}