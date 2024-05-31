import axios from 'axios';
import { Product } from '../../models/Product';
import { CreateProduct } from '../../models/CreateProduct';

const API_BASE_URL = 'https://localhost:7245/api';

export const getProducts = async (): Promise<Product[]> => {
    try {
        const response = await axios.get<Product[]>(`${API_BASE_URL}/product`);
        return response.data;
    } catch (error) {
        console.error('Error fetching products:', error);
        throw error;
    }
};

export async function createProduct(createProduct: CreateProduct): Promise<Product> {
    try {
        const response = await axios.post<Product>(`${API_BASE_URL}/product`, createProduct);
        return response.data;
    } catch (error) {
        console.error('Error creating product:', error);
        throw error;
    }
}