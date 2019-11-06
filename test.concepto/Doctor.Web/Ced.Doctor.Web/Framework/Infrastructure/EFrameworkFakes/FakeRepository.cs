using Microsoft.EntityFrameworkCore;
using Framework.Core;
using Framework.Core.Interfaces;
using Framework.Infrastructure.EFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Infrastructure.EFrameworkFakes
{

    public class FakeRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
    {
        private EntityStatus _status;

        public FakeRepository(IEnumerable<T> entities)
        {
            Entities = entities;
        }

        public IEnumerable<T> Entities { get; set; }
                
        public IEnumerable<T> ListAll() => Entities;
        
        public IEnumerable<T> List(ISpecification<T> spec) => Entities.AsQueryable().Where(spec.Criteria).ToList();

        public T GetSingleBySpec(ISpecification<T> spec) => Entities.AsQueryable().Where(spec.Criteria).SingleOrDefault();
        
        public T GetById(int i) => Entities.Where(x => x.Id == i).SingleOrDefault();
           
        public bool Update(T entity, bool omitExceptions) 
        {
            bool result = false;
            List<T> locEntities = new List<T>();

            try
            {
                

                var validate = entity.GetValidationStatus();
                if (!validate.IsValid) throw new DbEntityValidationException("Validation error entity", validate.EfErrors.ToList());

                var locUpdate = Entities.Where(e => e.Id == entity.Id).SingleOrDefault();
                if (locUpdate == null) throw new DbUpdateException("Entity not exist.", new Exception());

                foreach (var item in Entities)
                {
                    if (entity.Id == item.Id)
                    {
                        locEntities.Add(entity);
                    }
                    else
                    {
                        locEntities.Add(item);
                    }
                }

                Entities = locEntities.AsEnumerable();
                result = true;
            }
            catch (DbEntityValidationException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = entity.Id };
                _status.SetErrors(ex?.EntityValidationErrors);

            }
            catch (DbUpdateException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = entity.Id };
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }
            catch (Exception ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = entity.Id };
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }


            return result;
        }

        public Tuple<bool, T> Add(T entity, bool omitExceptions)
        {
            Tuple<bool, T> result = new Tuple<bool, T>(false, entity);

            try
            {
                           

                var validate = entity.GetValidationStatus();
                if (!validate.IsValid) throw new DbEntityValidationException("Validation error entity",validate.EfErrors.ToList());

                var locInsert = Entities.Where(x => x.Id == entity.Id).SingleOrDefault();
                if (locInsert != null) throw new DbUpdateException("Entity exist.", new Exception());

                entity.Id = Entities.Count() + 1;
                Entities = Entities.Concat(new[] { entity });
                result = new Tuple<bool, T>(true, entity);                

            }
            catch (DbEntityValidationException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = entity.Id };
                _status.SetErrors(ex?.EntityValidationErrors);

            }
            catch (DbUpdateException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = entity.Id };
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }
            catch (Exception ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus { Id = entity.Id };
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }

            return result;
        }
        
        public Tuple<bool, List<T>> AddRange(List<T> items, bool omitExceptions)
        {
            Tuple<bool, List<T>> result = new Tuple<bool, List<T>>(false, items);

            List<T> data = new List<T>();

            try
            {
                
                foreach (var entity in items)
                {
                    var validate = entity.GetValidationStatus();
                    if (!validate.IsValid) throw new DbEntityValidationException("Validation error entity", validate.EfErrors.ToList());

                    var locInsert = Entities.Where(e => e.Id == entity.Id).SingleOrDefault();
                    if (locInsert != null) throw new DbUpdateException("Entity exist.", new Exception());

                    entity.Id = Entities.Count() + 1;
                    Entities = Entities.Concat(new[] { entity });
                    data.Add(entity);
                }

                result = new Tuple<bool, List<T>>(true, data);
            }
            catch (DbEntityValidationException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus();
                _status.SetErrors(ex?.EntityValidationErrors);
            }
            catch (DbUpdateException ex)
            {
                if (!omitExceptions) throw;                
                _status = new EntityStatus();
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });

            }
            catch (Exception ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus();
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }            
            
            return result;
        }

        public Tuple<bool, List<T>> UpdateRange(List<T> items, bool omitExceptions)
        {
            Tuple<bool, List<T>> result = new Tuple<bool, List<T>>(false, items);

            List<T> locEntities = new List<T>();

            try
            {
                foreach (var entity in items)
                {
                    var validate = entity.GetValidationStatus();
                    if (!validate.IsValid) throw new DbEntityValidationException("Validation error entity", validate.EfErrors.ToList());

                    var locUpdate = Entities.Where(e => e.Id == entity.Id).SingleOrDefault();
                    if (locUpdate == null) throw new DbUpdateException("Entity not exist.", new Exception());

                }

                foreach (var item in Entities)
                {
                    var locItem = items.Where(e => e.Id == item.Id).SingleOrDefault();
                    if (locItem != null)
                    {
                        locEntities.Add(locItem);
                    }
                    else
                        locEntities.Add(item);
                }

                Entities = locEntities.AsEnumerable();

                result = new Tuple<bool, List<T>>(true, locEntities);
            }
            catch (DbEntityValidationException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus();
                _status.SetErrors(ex?.EntityValidationErrors);
            }
            catch (DbUpdateException ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus();
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });

            }
            catch (Exception ex)
            {
                if (!omitExceptions) throw;
                _status = new EntityStatus();
                _status.SetErrors(new List<ValidationResult> { new ValidationResult(ex.Message) });
            }

            return result;
        }

        public EntityStatus GetLastStatus()
        {
            return _status ?? new EntityStatus();
        }
        
        public T Add(T entity)
        {
            return this.Add(entity, true).Item2;
        }

        public void Update(T entity)
        {
            this.Update(entity,true);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> AddRange(List<T> entities)
        {
            return this.AddRange(entities, true).Item2;
        }

        public void UpdateRange(List<T> entities)
        {
            this.UpdateRange(entities, true);
        }

        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> Attach(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity, bool omitExceptions)
        {            

            Entities = Entities.ToList().Where(e => e.Id != entity.Id).AsEnumerable<T>();
            return true;            
        }

        #region Async methods
        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            throw new NotImplementedException();
        }

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
