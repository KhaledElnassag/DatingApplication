
export interface Members {
    id: string;
    userName: string;
    photoUrl: string;
    age: number;
    knownAs: string;
    created: string;
    lastActive: string;
    gender: string;
    introduction: string;
    lokingFor: string;
    intersts: string;
    city: string;
    country: string;
    photos: Photo[];
  }
  
  export interface Photo {
    id: number;
    url: string;
    isMain: boolean;
  }