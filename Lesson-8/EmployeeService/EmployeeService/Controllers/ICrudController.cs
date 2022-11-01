using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

public interface ICrudController<TClass, TId>
{
    public ActionResult<IList<TClass>> GetAll();
    public ActionResult<TClass> GetById([FromQuery]TId id);
    public ActionResult<TId> Create([FromQuery]TClass data);
    public ActionResult<bool> Update([FromQuery]TClass data);
    public ActionResult<bool> Delete([FromQuery]TId id);
}
