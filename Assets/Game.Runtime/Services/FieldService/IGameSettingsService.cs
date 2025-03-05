namespace Game.Runtime.Services.FieldService
{
    using UniGame.GameFlow.Runtime.Interfaces;
    using Unity.Mathematics;
    using UnityEngine;

    public interface IGameSettingsService : IGameService
    {
        public Vector2Int GetFieldSize {get;}
        KeyCode RestartKey { get;}
        int MinesCount { get; }
    }
}