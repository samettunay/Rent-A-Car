using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join a in context.Colors on c.ColorId equals a.Id
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 BrandName = b.Name,
                                 ColorName = a.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByBrandId(int brandId)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join a in context.Colors on c.ColorId equals a.Id
                             where c.BrandId == brandId
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 BrandName = b.Name,
                                 ColorName = a.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByColorId(int colorId)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join a in context.Colors on c.ColorId equals a.Id
                             where c.ColorId == colorId
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 BrandName = b.Name,
                                 ColorName = a.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description
                             };
                return result.ToList();
            }
        }
    }
}
