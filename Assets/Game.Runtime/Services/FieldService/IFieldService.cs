namespace Game.Runtime.Services.FieldService
{
    using UniGame.GameFlow.Runtime.Interfaces;
    using Unity.Mathematics;

    public interface IFieldService : IGameService
    {
        public int2 GetFieldSize();

    }
}