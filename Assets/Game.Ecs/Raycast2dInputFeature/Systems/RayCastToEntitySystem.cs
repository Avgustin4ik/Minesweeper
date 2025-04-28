namespace Game.Ecs.Raycast2dInputFeature.Systems
{
    using System;
    using Aspects;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class RayCastToEntitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private readonly Camera _raycastCamera;
        private readonly Transform _raycastCameraTransform;
        private InputAspect _inputAspect;
        private RaycastHit2D[] _hits;
        private LayerMask _layerMask;
        private Vector3 _point;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        public RayCastToEntitySystem(Camera raycastCamera = null, LayerMask layerMask = default, int RaycastHitCapacity = 3)
        {
            _raycastCamera = raycastCamera ?? Camera.main;
            _raycastCameraTransform = _raycastCamera.transform;
            _hits = new RaycastHit2D[RaycastHitCapacity];
            _layerMask = layerMask;
        }
        
        public void Run(IEcsSystems systems)
        {
            if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButtonUp(1)) return;
            Array.Clear(_hits,0,_hits.Length);
            
            _point = _raycastCamera.ScreenToWorldPoint(Input.mousePosition);
            
            Physics2D.RaycastNonAlloc(_point, _raycastCameraTransform.forward, _hits,Mathf.Infinity);
            Debug.DrawRay(_point, _raycastCameraTransform.forward, Color.red, 2f);
            Debug.DrawLine(_point, _point + _raycastCameraTransform.forward * _raycastCamera.farClipPlane, Color.red, 2f);
            foreach (var hit2d in _hits)
            {
                if(hit2d.collider == null) continue;
                if(hit2d.transform.TryGetComponent<ILeoEcsMonoConverter>(out var converter) ==  false) continue;
                if(converter.PackedEntity.Unpack(_world, out var entity) == false) continue;
                
                _inputAspect.RaycastHit.Add(entity);
                
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log($"LMBClick! Target entity: {entity}");
                    _inputAspect.LMBClick.Add(entity);
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    Debug.Log($"RMBClick! Target entity: {entity}");
                    _inputAspect.RMBClick.Add(entity);
                }
            }
        }
    }
}