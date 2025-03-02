namespace UniModules.Editor.OdinTools.GameEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
    using UnityEngine;
    
    /// <summary>
    /// base class for custom categories
    /// </summary>
    [Serializable]
    public abstract class GameEditorCategory : IGameEditorCategory
    {
        private const string PathFormat = "{0}/{1}";
        
        #region inspector

        [HideInInspector]
        public BaseEditorConfiguration configuration;
        
        [HorizontalGroup(nameof(category))]
        [LabelWidth(8)]
        [VerticalGroup(nameof(category)+"/"+nameof(icon))]
        [PreviewField(ObjectFieldAlignment.Left)]
        [HideLabel]
        public Sprite icon;

        [VerticalGroup(nameof(category)+"/"+nameof(name))]
        [LabelWidth(60)]
        public string name = nameof(GameEditorCategory);
        
        [VerticalGroup(nameof(category)+"/"+nameof(name))]
        [ValueDropdown(nameof(GetCategories))]
        [LabelWidth(60)]
        public string category = "BASE";

        public bool enabled = true;
        
        public Color color;
        
        #endregion
        
        public bool Enabled => enabled;

        public Color Color => color;
        
        public Sprite        Icon     => icon;
        
        public virtual string Category => category;

        public virtual string Name     => name;

        public abstract object CreateDrawer();

        public virtual void SetupConfiguration(BaseEditorConfiguration asset)
        {
            configuration = asset;
        }
        
        public virtual IGameEditorCategory UpdateCategory()
        {
            if (!string.IsNullOrEmpty(name) && name != Name) name = Name;
            return this;
        }

        public IEnumerable<string> GetCategories()
        {
            if(configuration == null)
               return Enumerable.Empty<string>();
            return configuration.editorGroups
                .Select(x => x.Name);
        }

        public virtual bool IsMatch(string searchString)
        {
            return GameEditorCategoryFilter.IsMatch(this, searchString);
        }
    }

    public static class GameEditorCategoryFilter
    {
        public static bool IsMatch(IGameEditorCategory category,string searchString)
        {
            if (category == null) return false;
            
            if (string.IsNullOrEmpty(searchString))
                return true;
            
            var isMatch = category.Name.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                          category.GetType().Name.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                          category.Category.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) >= 0;
            if (isMatch) return true;

            if (category.Icon!=null && 
                category.Icon.name.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) >= 0) 
                return true;

            return false;
        }
    }
}