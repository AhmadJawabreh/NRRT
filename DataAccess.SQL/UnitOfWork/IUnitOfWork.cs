/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using DataAccess.SQL.Entities;
using DataAccess.SQL.Repositories;

namespace DataAccess.SQL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRepository<PatientEntity> PatientRepository { get; }

        public IRepository<PatientMovementEntity> PatientMovementRepository { get; }

        public IRepository<PatientHistoryEntity> PatientHistoryRepository { get; }

        public Task SaveChanges();
    }
}
