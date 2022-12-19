namespace EmployeeService.Services;

public interface IRepository<TClass, TId>
{
    IList<TClass> GetAll();
    TClass GetById(TId id);
    TId Create(TClass data);
    bool Update(TClass data);
    bool Delete(TId id);
}
