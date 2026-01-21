import axios from 'axios';

const API_BASE_URL = 'http://localhost:5275/api';

export const todoAPI = {
    getAll: () => axios.get(`${API_BASE_URL}/todo`), 
    getById: (id) => axios.get(`${API_BASE_URL}/todo/${id}`),
    create: (todo) => axios.post(`${API_BASE_URL}/todo`, todo),
    update: (id, todo) => axios.put(`${API_BASE_URL}/todo/${id}`, todo),
    delete: (id) => axios.delete(`${API_BASE_URL}/todo/${id}`),
};