namespace Game.Runtime.Services.FieldService
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public struct FieldSettings
    {
        [Range(1, 100)]
        public int width;
        [Range(1, 100)]
        public int height;
        
        public bool setMinesManually;
        [Range(1, 50)]
        [ShowIf("@this.setMinesManually")]
        public int minesCount;
        
        public float offset;
    }
}