export interface User {
    id: string;
    token: string;
    expiration: string;
    user: {
      renterId: number | null;  
      dealerId: number | null;  
      firstName: string;
      lastName: string;
    }
  }
  