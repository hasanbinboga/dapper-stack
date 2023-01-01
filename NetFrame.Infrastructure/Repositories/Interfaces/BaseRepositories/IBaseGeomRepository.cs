using NetFrame.Core;
using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Özel repository leri tanımlanmamış ve geometry field ı içeren Entityler için genel repository interface'i. 
    /// Temel CRUD işlemlerini sağlar.
    /// </summary>
    /// <typeparam name="T"> Geometry field ı olan Entity türü </typeparam>
    public interface IBaseGeomRepository<T>: IRepository<T> where T: BaseGeomEntity
    {
        
    }
}
