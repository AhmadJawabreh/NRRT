/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using DataAccess.SQL.ApplicationDbContext;
using DataAccess.SQL.Entities;
using DataAccess.SQL.Repositories;

#nullable disable
namespace DataAccess.SQL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<PatientEntity> _patientRepository;

        private IRepository<PatientMovementEntity> _patientMovementRepository;

        private IRepository<PatientHistoryEntity> _patientHistoryRepository;

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext appDbContext) => _context = appDbContext;

        public IRepository<PatientEntity> PatientRepository
        {
            get
            {
                return _patientRepository ??= new BaseRepository<PatientEntity>(_context);
            }
        }

        public IRepository<PatientMovementEntity> PatientMovementRepository
        {
            get
            {
                return _patientMovementRepository ??= new BaseRepository<PatientMovementEntity>(_context);
            }
        }

        public IRepository<PatientHistoryEntity> PatientHistoryRepository
        {
            get
            {
                return _patientHistoryRepository ??= new BaseRepository<PatientHistoryEntity>(_context);
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
