// This file should contains the model of each entity

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
