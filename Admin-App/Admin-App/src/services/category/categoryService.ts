import axios from 'axios';
import { Category } from '../../models/Category';

const API_BASE_URL = 'https://localhost:7245/api';

export const getAllCategory = async (): Promise<Category[]> => {
  try {
    const response = await axios.get<Category[]>(`${API_BASE_URL}/category/all`);
    return response.data;
  } catch (error) {
    console.error('Error fetching data: ', error);
    throw error;
  }
};



export const createCategory = async (categoryDto: { categoryId: number; categoryName: string; parentId: number | null }): Promise<Category> => {
  try {
    const response = await axios.post<Category>(`${API_BASE_URL}/category/create`, categoryDto);
    return response.data;
  } catch (error) {
    console.error('Error creating category:', error);
    throw error;
  }
};

export const getSubcategories = async (id: number): Promise<Category[]> => {
  try {
    const response = await axios.get<Category[]>(`${API_BASE_URL}/category/${id}/subcategories`);
    debugger;
    return response.data;
  } catch (error) {
    console.error('Error fetching category details: ', error);
    throw error;
  }
};


export const updateCategory = async (id: number, updateCategoryDto: { categoryName: string; categoryDescription: string }): Promise<Category> => {
  try {
    const response = await axios.put<Category>(`${API_BASE_URL}/category/update/${id}`, updateCategoryDto);
    return response.data;
  } catch (error) {
    console.error('Error updating category: ', error);
    throw error;
  }
};