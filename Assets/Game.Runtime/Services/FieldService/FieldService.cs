namespace Game.Runtime.Services.FieldService
{
    using System;
    using UniGame.UniNodes.GameFlow.Runtime;
    using Unity.Mathematics;

    [Serializable]
    public struct FieldSettings
    {
        public int width;
        public int height;
    }

    [Serializable]
    public class FieldService : GameService, IFieldService
    {
        private readonly FieldSettings _settings;
        private int2 _fieldSize;

        public FieldService(FieldSettings settings)
        {
            _settings = settings;
            _fieldSize = new int2(_settings.width, _settings.height);
        }

        public int2 GetFieldSize() => _fieldSize;
    }
}