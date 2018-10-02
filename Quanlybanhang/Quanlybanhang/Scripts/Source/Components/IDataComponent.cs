using System.Collections;


namespace Quanlybanhang.Scripts.Source.Components
{
    public abstract class IDataComponent
    {        
        abstract public int GetTotalPage(int pagesize);
        abstract public IList GetDataByPage(int pagesize, int page);
    }
}