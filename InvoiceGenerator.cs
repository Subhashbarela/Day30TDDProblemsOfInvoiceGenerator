using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        // variable
        RideType rideType;
        private RideRepository rideRepository;

        //Constants
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PET_TIME;
        private readonly double MINIMUN_FARE;

        //constroctor to create rideRepositore instance
        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            rideRepository = new RideRepository();
            try
            {
                // if Ride Time is Preminum Then Rates Set For Premium else For Normal.
                if(rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PET_TIME = 2;
                    this.MINIMUN_FARE = 20;
                }
                else if (rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PET_TIME = 1;
                    this.MINIMUN_FARE = 5;
                }
            }
            catch (CabInvoiceException) {

                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Time ");


            }
        }
        public double CalculateFare(double distance,int time)
        {
            double totalFare = 0;
            try
            {
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PET_TIME;
            }
            catch (CabInvoiceException)
            {
                if(rideType.Equals(null))
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type ");
                }
                if(distance <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance ");

                }
                if (time <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time ");

                }
            }
            return Math.Max(totalFare, MINIMUN_FARE);
        }
    }
}
