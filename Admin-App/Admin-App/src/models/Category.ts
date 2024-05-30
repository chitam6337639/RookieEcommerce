export interface Category {
    categoryId: number;
    categoryName: string;
    categoryDescription: string;
    subCategories: Category[] | [];
    parentId: number | null;
}
