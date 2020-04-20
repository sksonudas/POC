namespace ProjectDetailService.Services
{
    using ProjectDetailService.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class EmpProjectService : IEmpProjectService
    {
        private List<EmployeeProjectModel> _empprojectList = new List<EmployeeProjectModel>
        {
            new EmployeeProjectModel { EmpId = 33425, ProjectId = "P33425", ProjectName = "POS_Project" },
            new EmployeeProjectModel { EmpId = 32433, ProjectId = "P32433", ProjectName = "Hercules_Project" }
        };

        public EmployeeProjectModel GetEmpProjectDetail(int empid)
        {
            var empProjectDetail = _empprojectList.SingleOrDefault(x => x.EmpId == empid);

            // return null if details not found
            if (empProjectDetail == null)
                return null;

            return empProjectDetail;
        }
              
    }
}
