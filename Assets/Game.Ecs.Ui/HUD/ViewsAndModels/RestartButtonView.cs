namespace NAMESPACE.ViewsAndModels
{
    using System;
    using Cysharp.Threading.Tasks;
    using UniGame.LeoEcs.ViewSystem.Converters;
    using UniGame.Runtime.Common;
    using UniGame.Rx.Runtime.Extensions;
    using UniGame.UiSystem.Runtime;
    using UniGame.ViewSystem.Runtime;
    using UniModules.UniGame.Core.Runtime.Rx;
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
    public class RestartButtonView : EcsUiView<RestartButtonViewModel>
    {
        public Button button;
        protected override UniTask OnInitialize(RestartButtonViewModel model)
        {
            this.Bind(button, model.pressed);
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
    public class RestartButtonViewModel : ViewModelBase
    {
        public SignalValueProperty<bool> pressed = new ();

    }
}