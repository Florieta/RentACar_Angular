export interface CarForm {
    regNumber: string;
    model: string;
    make: string;
    makeYear: number;
    airCondition: boolean;
    seats: number;
    doors: number;
    navigationSystem: boolean;
    fuel: string;
    transmission: string;
    dailyRate: number;
    imageUrl: string;
    categoryId: number;
    dealerId: number | null;
  }
  