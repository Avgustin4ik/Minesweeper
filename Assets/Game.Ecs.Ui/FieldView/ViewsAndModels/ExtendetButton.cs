namespace Game.Ecs.Ui.FieldView.ViewsAndModels
{
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class ExtendedButton : Button // Исправлено имя на более стандартное
    {
        // События для разных типов кликов
        public event System.Action OnLeftClick;
        public event System.Action OnRightClick;
        public event System.Action OnMiddleClick;

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData); 

            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnLeftClick?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    OnRightClick?.Invoke();
                    break;
                case PointerEventData.InputButton.Middle:
                    OnMiddleClick?.Invoke();
                    break;
            }
        }
    }
}