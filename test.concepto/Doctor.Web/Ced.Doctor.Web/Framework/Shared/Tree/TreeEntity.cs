using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Shared.Tree
{
    #region Enums
    public enum EnumTreeNodeType
    {
        Root = 0,
        Common = 1,
        Final = 2
    }

    public enum EnumTreeDataBehaivor
    {
        ReplicateDataNone = 0,
        ReplicateDataInParent = 1
    }
    #endregion

    #region Tree
    public class TreeNode<T> where T : class
    {

        public TreeNode()
        {
            Type = EnumTreeNodeType.Root;
            Name = "Root";
            Label = "Root";
            Level = 0;
            Childs = new List<TreeNode<T>>();
        }
        public TreeNode(EnumTreeNodeType nodeType = EnumTreeNodeType.Root, string name = "Root", string label = "Root", TreeNode<T> parentNode = null, List<T> data = null, int? level = null)
        {
            Type = nodeType;
            Name = name ?? string.Empty;
            Label = label ?? string.Empty;
            Parent = parentNode ?? null;
            Level = level ?? 0;
            Childs = new List<TreeNode<T>>();

        }

        public int Id { get; set; }

        public int? ExternalId { get; set; }

        public int Level { get; set; }

        public EnumTreeNodeType Type { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public TreeNode<T> Parent { get; set; }

        public int ParentId => this.Parent?.Id ?? 0;

        public List<T> Data { get; set; }

        public void SetData(T value, bool replicateParent)
        {

            try
            {
                var locData = this.Data ?? new List<T>();
                var elem = locData.SingleOrDefault(d => d.GetType().GetProperty("Id").GetValue(d).ToString() == value.GetType().GetProperty("Id").GetValue(value).ToString());
                if (elem == null) locData.Add(value);

                this.Data = locData;

                //si replica en el padre
                if (replicateParent && this.Parent != null && this.Parent.Type != EnumTreeNodeType.Root)
                    this.Parent.SetData(value, replicateParent);

            }
            catch (Exception ex)
            {

                //debuggear exception
                var locException = ex;
            }




        }

        public List<TreeNode<T>> Childs { get; set; }

        public TreeNode<T> AddChild(EnumTreeNodeType nodeType, string name, string label, List<T> treeData)
        {
            var child = new TreeNode<T>(nodeType, name, label, this, treeData, this.Level + 1);
            this.Childs.Add(child);
            return child;
        }

    }

    public class TreeData<T> where T : class
    {
        public List<TreeNode<T>> Nodes { get; set; }
    }
    #endregion
}
