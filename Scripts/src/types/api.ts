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
  title: string;
  iconSrc: string;
  href: string;
}