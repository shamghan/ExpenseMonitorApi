using AutoMapper;
using ExpenseMonitor.Application.Dishes.Commands.CreateDishes;
using ExpenseMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseMonitor.Application.Dishes.Dtos
{
    public class DishesProfile : Profile
    {
        public DishesProfile() {

            CreateMap<Dish,DishDto>();
            CreateMap<CreateDishCommand,Dish>();
        }
    }
}
