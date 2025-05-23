namespace Game.Ecs.Field.Cell
{
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public class Cell : LeoEcsConverter
    {
        public Collider2D Collider;
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            world.AddComponent<CellComponent>(entity);
            ref var cellComponent = ref world.GetPool<CellComponent>().Get(entity);
            cellComponent.size = Collider == null ? target.GetComponent<Collider>().bounds.size : Collider.bounds.size;
        }
    }
}