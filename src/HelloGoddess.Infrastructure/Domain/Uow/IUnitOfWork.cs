using System;

namespace HelloGoddess.Infrastructure.Domain.Uow
{
    /// <summary>
    /// Defines a unit of work.
    /// This interface is internally used by HelloGoddess.Infrastructure.
    /// Use <see cref="IUnitOfWorkManager.Begin()"/> to start a new unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Unique id of this UOW.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Reference to the outer UOW if exists.
        /// </summary>
        IUnitOfWork Outer { get; set; }

        bool IsDisposed { get; set; }
    }
}