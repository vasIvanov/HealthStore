using AutoMapper;
using HealthStore.Models.Contracts.Requests;
using HealthStore.Models.Contracts.Responses;
using HealthStore.Models.Products;
using HealthStore.Models.Users;
using System.Collections.Generic;

namespace HealthStore.Extensions
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserRequest, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();
            CreateMap<IEnumerable<UserResponse>, IEnumerable<User>>();

            CreateMap<EmployeeRequest, Employee>().ReverseMap();
            CreateMap<EmployeeResponse, Employee>().ReverseMap();
            CreateMap<IEnumerable<EmployeeResponse>, IEnumerable<Employee>>();

            CreateMap<PlanRequest, Plan>().ReverseMap();
            CreateMap<PlanResponse, Plan>().ReverseMap();
            CreateMap<IEnumerable<PlanResponse>, IEnumerable<Plan>>();

            CreateMap<DietRequest, Diet>().ReverseMap();
            CreateMap<DietResponse, Diet>().ReverseMap();
            CreateMap<IEnumerable<DietResponse>, IEnumerable<Diet>>();

            CreateMap<SupplementsRequest, Supplement>().ReverseMap();
            CreateMap<SupplementsResponse, Supplement>().ReverseMap();
            CreateMap<IEnumerable<SupplementsResponse>, IEnumerable<Supplement>>();
        }
    }
}
