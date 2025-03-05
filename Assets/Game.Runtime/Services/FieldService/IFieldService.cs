namespace Game.Runtime.Services.FieldService
{
    using UniGame.GameFlow.Runtime.Interfaces;
    using Unity.Mathematics;
    using UnityEngine;

    public interface IFieldService : IGameService
    {
        public int2 GetFieldSize();

        KeyCode RestartKey { get;}
        int MinesCount { get; }
    }
}