using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICustomerService _customerService;
        ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICustomerService customerService, ICarService carService)
        {
            _rentalDal = rentalDal;
            _customerService = customerService;
            _carService = carService;
        }

        public IResult Add(Rental rental)
        {
            var result = RulesForAdding(rental);

            if (!result.Success)
            {
                return result;
            }

            _rentalDal.Add(rental);

            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult RulesForAdding(Rental rental)
        {
            var result = BusinessRules.Run(
                CheckIfRentDateIsBeforeToday(rental.RentDate),
                CheckIfReturnDateIsBeforeRentDate(rental.ReturnDate, rental.RentDate),
                CheckIfThisCarIsAlreadyRentedInSelectedDateRange(rental),
                //CheckIfCustomerIsFindeksPointIsSufficientForThisCar(carId, customerId),
                CheckIfThisCarIsRentedAtALaterDateWhileReturnDateIsNull(rental),
                CheckIfThisCarHasBeenReturned(rental));
            if (result != null)
            {
                return result;
            }
            return new SuccessResult("Ödeme sayfasına yönlendiriliyorsunuz.");
        }

        private IResult CheckIfCustomerIsFindeksPointIsSufficientForThisCar(int carId, int customerId)
        {
            var customer = _customerService.GetCustomerById(customerId);
            if (!customer.Success)
                return new ErrorResult(Messages.CustomerNotFound);

            var car = _carService.GetById(carId);
            if (!car.Success)
                return new ErrorResult(Messages.CarNotFound);

            if (car.Data.FindexPoint > customer.Data.FindexPoint)
                return new ErrorResult(Messages.CustomerFindeksPointIsNotEnoughForThisCar);
            
            return new SuccessResult();

        }

        private IResult CheckIfRentDateIsBeforeToday(DateTime rentDate)
        {
            if (rentDate.Date < DateTime.Now.Date)
            {
                return new ErrorResult(Messages.RentalDateCannotBeBeforeToday);
            }
            return new SuccessResult();
        }

        private IResult CheckIfThisCarIsRentedAtALaterDateWhileReturnDateIsNull(Rental rental)
        {
            var result = _rentalDal.Get(r => 
            r.CarId == rental.CarId 
            && rental.ReturnDate == null 
            && r.RentDate.Date > rental.RentDate.Date
            );

            if (result != null)
            {
                return new ErrorResult(Messages.ReturnDateCannotBeLeftBlankAsThisCarWasAlsoRentedAtALaterDate);
            }

            return new SuccessResult();
        }

        private IResult CheckIfThisCarHasBeenReturned(Rental rental)
        {
            var result = _rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate == null);

            if (result != null)
            {
                if (rental.ReturnDate == null || rental.ReturnDate > result.RentDate)
                {
                    return new ErrorResult(Messages.ThisCarHasNotBeenReturnedYet);
                }
            }
            return new SuccessResult();
        }

        private IResult CheckIfReturnDateIsBeforeRentDate(DateTime? returnDate, DateTime rentDate)
        {
            if (returnDate != null)
                if (returnDate < rentDate)
                {
                    return new ErrorResult(Messages.ReturnDateCannotBeEarlierThanRentDate);
                }
            return new SuccessResult();
        }

        private IResult CheckIfThisCarIsAlreadyRentedInSelectedDateRange(Rental rental)
        {
            var result = _rentalDal.Get(r =>
            r.CarId == rental.CarId
            && (r.RentDate.Date == rental.RentDate.Date
            || (r.RentDate.Date < rental.RentDate.Date
            && (r.ReturnDate == null
            || ((DateTime)r.ReturnDate).Date > rental.RentDate.Date))));

            if (result != null)
            {
                return new ErrorResult(Messages.ThisCarIsAlreadyRentedInSelectedDateRange);
            }
            return new SuccessResult();
        }

    }
}
