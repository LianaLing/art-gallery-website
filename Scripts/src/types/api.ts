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

export type Author = {
  id: string;
  name: string;
  avatarUrl: string;
};

export type Art = {
  id: string;
  imageSrc: string;
  title: string;
  price: number;
  author: Author;
};

export type Icon = {
  id: string;
  src: string;
  alt: string;
  href: string;
  creditRef: string;
  author: string;
}

export type Save = {
  id: string;
  title: string;
  pinNo: number;
  updatedAt: number;
  href: string;
  arts: SavedArt[];
}

export type SavedArt = {
  id: string;
  imageSrc: string;
  title: string;
}

export type Profile = {
  name: string;
  username: string;
  following: number;
  avatarUrl: string;
  profileUrl: string;
}