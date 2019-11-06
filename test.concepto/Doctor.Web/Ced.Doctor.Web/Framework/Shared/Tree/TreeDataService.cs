using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Shared.Tree
{
    public class TreeDataService<T, TSource> : ITreeDataService<T, TSource> where T : class
    {
        private List<T> NodeData;
        private EnumTreeDataBehaivor TreeDataBehaivor = EnumTreeDataBehaivor.ReplicateDataNone;

        #region Constructores

        public TreeDataService()
        {
            NodeData = new List<T>();
        }

        #endregion

        #region Métodos
        public TreeData<T> GetData(IEnumerable<TSource> inputData, IEnumerable<T> nodeData, EnumTreeDataBehaivor treeDataBehaivor = EnumTreeDataBehaivor.ReplicateDataNone)
        {

            this.NodeData = nodeData.ToList() ?? new List<T>();
            this.TreeDataBehaivor = treeDataBehaivor;

            PropertyInfo[] properties = typeof(TSource).GetProperties();


            var result = new TreeData<T>
            {
                Nodes = new List<TreeNode<T>>()
            };

            var nodesTypes = new ArrayList
            {
                "Root"
            };
            nodesTypes.AddRange(properties.Select(p => p.Name).ToArray());


            var rootNode = new TreeNode<T>();

            int autoId = 0;

            try
            {

                foreach (var item in inputData)
                {

                    var previousNode = rootNode;

                    for (int i = 1; i < nodesTypes.Count; i++)
                    {
                        var value = item.GetType().GetProperty(nodesTypes[i].ToString()).GetValue(item);

                        if (value != null)
                        {

                            var nodeName = value.ToString();
                            var findNode = previousNode.Childs.SingleOrDefault(n => n.Name == nodeName);

                            if (findNode == null)
                            {

                                if (i == nodesTypes.Count - 1)
                                { 
                                    AddDataToNode(previousNode, nodeName);
                                }
                                else
                                {

                                    //los campos q empiezan por Id no se consideran nodos
                                    if (IsNode(nodesTypes[i].ToString()))
                                    {

                                        previousNode = previousNode.AddChild(EnumTreeNodeType.Common, nodeName, nodesTypes[i].ToString(), null);
                                        autoId++;

                                        int.TryParse(item.GetType().GetProperty("Id" + previousNode.Label)?.GetValue(item).ToString(), out int nodeId);
                                        previousNode.ExternalId = nodeId;
                                        previousNode.Id = autoId;
                                    }

                                }

                            }
                            else
                            {
                                previousNode = findNode;
                            }

                        }

                    }
                }

                result.Nodes.Add(rootNode);

            }
            catch
            {

                throw;
            }

            return result;

        }

        private bool IsNode(string nodeType)
        {

            var locValue = nodeType.ToLower();
            if (locValue.Length > 3 && locValue.Substring(0, 2) == "id") return false;            
            
            return true;
        }

        private void AddDataToNode(TreeNode<T> previousNode, string nodeName)
        {
            try
            {
                //puede dar excepción si no existe propiedad "Id"
                var locData = this.NodeData.SingleOrDefault(d => d.GetType().GetProperty("Id").GetValue(d).ToString() == nodeName);

                if (locData != null)
                {
                    previousNode.SetData(locData, this.TreeDataBehaivor == EnumTreeDataBehaivor.ReplicateDataInParent ? true : false);
                    previousNode.Type = EnumTreeNodeType.Final;
                }
            }
            catch (Exception ex)
            {

                //para debuggear excepción
                var locException = ex;
            }


        }

        #endregion
    }
}
