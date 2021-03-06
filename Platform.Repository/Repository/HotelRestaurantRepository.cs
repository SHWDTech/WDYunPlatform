﻿using System;
using System.Linq;
using System.Linq.Expressions;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class HotelRestaurantRepository : SysDomainRepository<HotelRestaurant>, IHotelRestaurantRepository
    {
        public static Expression<Func<HotelRestaurant, bool>> Filter { get; set; }

        public HotelRestaurantRepository()
        {

        }

        public HotelRestaurantRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }

        public override void InitEntitySet()
        {
            base.InitEntitySet();
            if (Filter != null)
            {
                EntitySet = EntitySet.Where(Filter);
            }

            if (ContextLocal.UserContext != null && ContextLocal.UserContext.ContainsKey("district"))
            {
                var userDistricts =
                    ContextLocal.UserContext.Where(obj => obj.Key == "district").Select(item => (Guid) item.Value).ToList();
                EntitySet = EntitySet.Where(obj => userDistricts.Contains(obj.DistrictId));
            }
        }
    }
}
