export interface User {
    id: string;
    userName: string;
    email: string;
    roles?: string[];
    token: string;
    expiration: string;
  }
  