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
  alt: string;
  href: string;
}
