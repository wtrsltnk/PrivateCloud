using Microsoft.EntityFrameworkCore;
using PrivateCloud.Domain.Users;
using PrivateCloud.Infra.Sqlite.Dto;
using PrivateCloud.Practises.Exceptions;
using PrivateCloud.Practises.Mapping;
using PrivateCloud.Practises.Repositories;
using System;
using System.Linq;

namespace PrivateCloud.Infra.Sqlite.Repositories
{
    public class AbstractRepository<TDomain, TDto> :
        IRepository<TDomain>
        where TDto : class, IDto
        where TDomain : class, IEntity
    {
        protected readonly DbSet<TDto> _dbSet;
        protected readonly IMapper<TDomain, TDto> _entityMapper;
        protected readonly IMapper<TDto, TDomain> _dtoMapper;

        public AbstractRepository(
            DbSet<TDto> dbSet,
            IMapper<TDomain, TDto> entityMapper,
            IMapper<TDto, TDomain> dtoMapper)
        {
            _dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
            _entityMapper = entityMapper ?? throw new ArgumentNullException(nameof(entityMapper));
            _dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
        }

        public TDomain Add(
            TDomain entity)
        {
            #region Pre-conditions
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            #endregion

            CheckAdd(entity);

            var dto = _entityMapper.Map(entity);

            _dbSet.Add(dto);

            return _dtoMapper.Map(dto);
        }

        public void Delete(
            TDomain entity)
        {
            var dto = _dbSet.Find(entity.Id);

            if (dto == null)
            {
                throw new NotFoundException();
            }

            dto.Flags |= (int)DtoFlags.Deleted;

            _dbSet.Update(dto);
        }

        public virtual IQueryable<TDomain> GetAll()
        {
            return _dbSet
                .Select(_dtoMapper.Map)
                .AsQueryable();
        }

        public TDomain GetById(
            Guid id)
        {
            var dto = Find(id);

            if (dto == null)
            {
                throw new NotFoundException();
            }

            return _dtoMapper.Map(dto);
        }

        public void Update(
            TDomain entity)
        {
            #region Pre-conditions
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            #endregion

            var dto = _dbSet.Find(entity.Id);

            if (dto == null)
            {
                throw new NotFoundException();
            }

            if (dto.Id != entity.Id)
            {
                throw new NotFoundException();
            }

            UpdateDto(ref dto, entity);

            _dbSet.Update(dto);
        }

        protected virtual TDto Find(
            Guid id)
        {
            return _dbSet.Find(id);
        }

        protected virtual void CheckAdd(
            TDomain entity)
        { }

        protected virtual void UpdateDto(
            ref TDto dto,
            TDomain entity)
        { }
    }
}
