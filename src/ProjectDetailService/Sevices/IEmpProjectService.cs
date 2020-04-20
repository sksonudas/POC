namespace ProjectDetailService.Services
{
    using ProjectDetailService.Models;
    public interface IEmpProjectService
    {
        EmployeeProjectModel GetEmpProjectDetail(int empid);
    }
}
