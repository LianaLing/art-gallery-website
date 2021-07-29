// This file should contains the model of each entity

export type Author = {
  id: string;
  description: string;
  verified: boolean;
};

export type Art = {
  id: number;
  style: string;
  description: string;
  price: number;
  stock: number;
  likes: number;
  url: string;
  author_id: number;
};
