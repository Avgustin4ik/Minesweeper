namespace Game.Ecs.Ui.FieldView.ViewsAndModels
{
    using System;
    using Cysharp.Threading.Tasks;
    using UniGame.LeoEcs.ViewSystem.Converters;
    using UniGame.Rx.Runtime.Extensions;
    using UniGame.UiSystem.Runtime;
    using UniGame.ViewSystem.Runtime;
    using UniRx;
    using UnityEngine;
    using UnityEngine.UI;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [Serializable]
    public class CellView : EcsUiView<CellViewModel>
    {
        public Button button;
        public TMPro.TextMeshProUGUI text;
        protected override UniTask OnInitialize(CellViewModel model)
        {
            this.Bind(model.DEBUG_position, x => text.text = x.ToString());
            return base.OnInitialize(model);
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [Serializable]
    public class CellViewModel : ViewModelBase
    {
        public Vector2Int position;
        public ReactiveProperty<Vector2Int> DEBUG_position = new();
    }
}