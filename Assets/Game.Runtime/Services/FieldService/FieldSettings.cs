namespace Game.Runtime.Services.FieldService
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct FieldSettings
    {
        [Range(1, 100)]
        public int width;
        [Range(1, 100)]
        public int height;
        [Range(1, 50)]
        public int minesCount;
    }
}