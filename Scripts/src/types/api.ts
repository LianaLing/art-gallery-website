/**
 * This is the general structure of an api response returned from backend.
 * A model type (eg. Art) should be provided as generic to ensure
 * all data are properly typed.
 */
export type ApiResponse<T> = {
  data: T | null;
  error: Error | null;
};

// This is the general structure of an error returned from backend
export type Error = {
  errorType: string;
  errorCode: string;
  message: string;
  statusCode: number;
};

export type AuthorTest = {
  id: string;
  name: string;
  avatarUrl: string;
};

export type ArtTest = {
  id: string;
  imageSrc: string;
  title: string;
  price: number;
  author: AuthorTest;
};

export type Icon = {
  id: string;
  src: string;
  alt: string;
  href: string;
  creditRef: string;
  author: string;
};

export type Save = {
  id: string;
  title: string;
  pinNo: number;
  updatedAt: number;
  href: string;
  arts: SavedArt[];
};

export type SavedArt = {
  id: string;
  imageSrc: string;
  title: string;
};

export type Profile = {
  name: string;
  username: string;
  following: number;
  avatarUrl: string;
  profileUrl: string;
};

// This is the response of an Art that is returned from ASP.NET
export type ArtResponse = {
  id: number;
  style: string;
  description: string;
  price: number;
  stock: number;
  likes: number;
  url: string;
  author: {
    id: number;
    description: string;
    verified: boolean;
    username: string;
    name: string;
    ic: string;
    dob: string;
    contactNo: string;
    email: string;
    avatarUrl: string;
  };
};

// export type ArtDetailResponse = {
//   id: number;
//   style: string;
//   description: string;
//   price: number;
//   stock: number;
//   likes: number;
//   url: string;
//   author: {
//     id: number;
//     description: string;
//     verified: boolean;
//     username: string;
//     name: string;
//     ic: string;
//     dob: string;
//     contactNo: string;
//     email: string;
//     avatarUrl: string;
//   };
// }

export type Art = {
  id: number;
  style: string;
  description: string;
  price: number;
  stock: number;
  likes: number;
  url: string;
}

export type Author = {
  id: number;
  description: string;
  verified: boolean;
}

export type User = {
  id: number;
  username: string;
  name: string;
  ic: string;
  dob: string;
  contactNo: string;
  email: string;
  avatarUrl: string;
}

export type FavResponse = {
  id: number;
  name: string;
  art: Art;
  user: User;
  author: Author;
}