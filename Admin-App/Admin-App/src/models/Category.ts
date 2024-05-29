export interface Category {
    categoryId: number;
    categoryName: string;
    subCategories: Category[] | [];
    parentId: number | null;
}
