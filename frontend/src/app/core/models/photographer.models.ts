export interface Photographer {
  id: string;
  userId: string;
  bio?: string;
  location?: string;
  pricePerHour: number;
  ratingAverage: number;
  ratingCount: number;
  isVerified: boolean;
}

export interface PhotographerPortfolioItem {
  id: string;
  photographerId: string;
  title: string;
  imageUrl: string;
}
