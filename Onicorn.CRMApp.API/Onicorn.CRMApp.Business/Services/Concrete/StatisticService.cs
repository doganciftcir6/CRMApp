using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.StatisticDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Enums;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class StatisticService : IStatisticService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUow _uow;
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ISaleRepository _saleRepository;
        public StatisticService(UserManager<AppUser> userManager, IUow uow, IProjectRepository projectRepository, ITaskRepository taskRepository, ISaleRepository saleRepository)
        {
            _userManager = userManager;
            _uow = uow;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _saleRepository = saleRepository;
        }

        public async Task<CustomResponse<StatisticDto>> GetStatisticsAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            int userCount = users.Count();
            int maleUserCount = users.Count(x => x.GenderId == (int)GenderEnum.Male);
            int femaleUserCount = users.Count(x => x.GenderId == (int)GenderEnum.Female);
            int unknownUserCount = users.Count(x => x.GenderId == (int)GenderEnum.Unspecified);
            var customers = await _uow.GetRepository<Customer>().GetAllAsync();
            int customerCount = customers.Count();
            var projects = await _projectRepository.GetAllFilterAsync(x => x.Status == true);
            int projectCount = projects.Count();
            var tasks = await _taskRepository.GetAllFilterAsync(x => x.Status == true);
            int taskCount = tasks.Count();
            int activeTaskCount = tasks.Count(x => x.TaskSituation.Id == (int)TaskSituationEnum.Continues);
            int FinishedTaskCount = tasks.Count(x => x.TaskSituation.Id == (int)TaskSituationEnum.Completed);
            var sales = await _saleRepository.GetAllFilterAsync(x => x.Status == true);
            int salesCount = sales.Count();
            decimal totalSalesPrice = sales.Sum(x => x.SalesAmount);
            decimal totalWithKDV = CalculateTotalWithKDV(totalSalesPrice);

            StatisticDto statisticDto = new StatisticDto()
            {
                UserCount = userCount,
                MaleUserCount = maleUserCount,
                FemaleUserCount = femaleUserCount,
                UnknownUserCount = unknownUserCount,
                CustomerCount = customerCount,
                ProjectCount = projectCount,
                TaskCount = taskCount,
                ActiveTaskCount = activeTaskCount,
                FinishedTaskCount = FinishedTaskCount,
                saleCount = salesCount,
                totalSalesPrice = totalWithKDV,
            };

            return CustomResponse<StatisticDto>.Success(statisticDto, ResponseStatusCode.OK);
        }

        public decimal CalculateTotalWithKDV(decimal totalSalesPrice)
        {
            decimal kdvRate = 0.20m;
            decimal kdvAmount = totalSalesPrice * kdvRate;
            decimal totalWithKDV = totalSalesPrice + kdvAmount;

            return totalWithKDV;
        }
    }
}
