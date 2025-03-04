namespace Game.Ecs.Ui.FieldView.ViewsAndModels
{
    using System;
    using Cysharp.Threading.Tasks;
    using UniGame.LeoEcs.ViewSystem.Converters;
    using UniGame.Runtime.Common;
    using UniGame.Rx.Runtime.Extensions;
    using UniGame.UiSystem.Runtime;
    using UniModules.UniGame.Core.Runtime.Rx;
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
        public Image bomb;
        public Image flag;
        public ExtendedButton button;
        public TMPro.TextMeshProUGUI text;
        protected override UniTask OnInitialize(CellViewModel model)
        {
            this.Bind(model.DEBUG_position, x => text.text = x.ToString());
            this.Bind(model.DEBUG_isBomb, x => text.color = x ? Color.red : Color.gray);
            this.Bind(model.flag, SetupFlag);
            this.Bind(model.detonate, Detonate);
            this.Bind(model.ShowNeighborMines, x => text.text = x.ToString());
            this.Bind(model.reset, SetDefaultState);
            
            button.OnLeftClick += OnLeftClickHandle;
            button.OnRightClick += OnRightClickHandle;
            
            SetDefaultState();
            return base.OnInitialize(model);
        }

        private void SetDefaultState()
        {
            flag.enabled = false;
            bomb.enabled = false;
            text.text = string.Empty;
        }

        private void Detonate()
        {
            bomb.enabled = true;
        }


        private void OnRightClickHandle()
        {
            Model.rightClick.Value = true;
        }

        private void OnLeftClickHandle()
        {
            Model.leftClick.Value = true;
        }

        private void SetupFlag(bool value)
        {
            flag.enabled = value;
        }
        
        private void OnDestroy()
        {
            button.OnLeftClick -= OnLeftClickHandle;
            button.OnRightClick -= OnRightClickHandle;
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
        public SignalValueProperty<bool> rightClick = new();
        public SignalValueProperty<bool> leftClick = new();
        public ReactiveProperty<Vector2Int> DEBUG_position = new();
        public ReactiveValue<bool> DEBUG_isBomb = new();
        public ReactiveValue<bool> flag = new();
        public ReactiveCommand detonate = new();
        public ReactiveCommand<int> ShowNeighborMines = new();
        public ReactiveCommand reset = new();
    }
}