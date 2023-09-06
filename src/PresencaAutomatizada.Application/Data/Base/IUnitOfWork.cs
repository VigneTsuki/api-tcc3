﻿namespace PresencaAutomatizada.Application.Data.Base
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
