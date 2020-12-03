using System.Collections.Generic;

namespace Framework.Shared.Tree
{
    public interface ITreeDataService<T, TSource> where T : class
    {
        TreeData<T> GetData(IEnumerable<TSource> inputData, IEnumerable<T> nodeData, EnumTreeDataBehaivor treeDataBehaivor = EnumTreeDataBehaivor.ReplicateDataNone);
    }
}
