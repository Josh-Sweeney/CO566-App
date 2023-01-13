using System;
namespace gha.mobile.common.repositories
{
    public interface IRepository<T>
    {
        event EventHandler<T> OnAdded;
        event EventHandler<T> OnUpdated;
        event EventHandler<T> OnDeleted;
    }
}

