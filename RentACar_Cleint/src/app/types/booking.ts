export interface Booking {
    id: number;
    pickUpDateAndTime: Date;
    dropOffDateAndTime: Date;
    duration: number;
    totalAmount: number;
    paymentType: string;
    isActive: boolean;
    isPaid: boolean;
    carMake: string;
    carModel: string;
    regNumber: string;
    pickUpLocation: string;
    dropOffLocation: string;
    insurance?: string;
    renterId: string;
  }