namespace Game.Ecs.Ui.FieldView.ViewsAndModels
{
    using System;
    using Cysharp.Threading.Tasks;
    using UniGame.LeoEcs.ViewSystem.Converters;
    using UniGame.Rx.Runtime.Extensions;
    using UniGame.UiSystem.Runtime;
    using UniModules.UniGame.UISystem.Runtime.Extensions;
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
    public class FieldView : EcsUiView<FieldViewModel>
    {
        public GridLayoutGroup layout;
        private Transform _layoutTransform;
        
        protected override UniTask OnInitialize(FieldViewModel model)
        {
            _layoutTransform = layout.transform;
            
            // this.Bind(model.FieldSize,x => UpdateField(x));
            this.Bind(model.GenerateField, SpawnElements);
            return base.OnInitialize(model);
        }

        private UniTask SpawnElements(Vector2Int arg)
        {
            UpdateField(arg);
            for (int i = 0; i < arg.x; i++)
            {
                for (int j = 0; j < arg.y; j++)
                {
                    var model = new CellViewModel();
                    model.position = new Vector2Int(i, j);
                    this.CreateViewAsync<CellView>(model, parent: _layoutTransform);
                }
            }
            return UniTask.CompletedTask;
        }

        private void UpdateField(Vector2Int size)
        {
            layout.constraintCount = size.x;
            RecalculateLayout(size);
        }

        private void RecalculateLayout( Vector2Int size)
        {
            var parentRect = RectTransform.rect;
            int columns = layout.constraintCount;
            int rows = size.y;

            float totalSpacingX = layout.spacing.x * (columns - 1);
            float totalSpacingY = layout.spacing.y * (rows - 1);

            float cellWidth = (parentRect.width - totalSpacingX) / columns;
            float cellHeight = (parentRect.height - totalSpacingY) / rows;
            float cellSize = Mathf.Min(cellWidth, cellHeight);

            layout.cellSize = new Vector2(cellSize, cellSize);
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
    public class FieldViewModel :  ViewModelBase
    {
        public ReactiveProperty<Vector2Int> FieldSize = new ();
        public ReactiveCommand<Vector2Int> GenerateField = new ();
    }
}