import axios from 'axios';
import { User } from '../../models/User';

const API_BASE_URL = 'https://localhost:7245/api';

export const getAllUsers = async (): Promise<User[]> => {
    try {
        const response = await axios.get(`${API_BASE_URL}/account/allusers`);
        return response.data;
    } catch (error) {
        console.error("There was an error fetching the users!", error);
        throw error;
    }
};
