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

export type User = {
  name: string;
  username: string;
  following: number;
  avatarUrl: string;
  profileUrl: string;
}