import axios from "axios";

// const API_URL = "https://task-management-app-jawl.onrender.com/api"; // Adjust based on your backend URL

const API_URL = "http://localhost:5284/api"; // Adjust based on your backend URL
// Create an axios instance
const api = axios.create({
    baseURL: API_URL,
    headers: {
        "Content-Type": "application/json",
    },
});

// Add an interceptor to include the token in all requests
api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem("token");
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);  

export const fetchTasks = async (id: number) => {
    const response = await api.get(`/task/${id}`);
    return response.data;
};

export const createTask = async (taskData: object) => {
    console.log(taskData);
    
    const response = await api.post("/task/create", taskData);
    return response.data; // Assuming the backend returns the created task data
};

export const updateTaskStatus = async (taskId: string, status: string) => {
    const response = await api.put(`/task/${taskId}`, { status });
    return response.data;
};

export const deleteTask = async (taskId: string) => {
    const response = await api.delete(`/task/delete/${taskId}`);
    return response.data;
};

export const loginUser = async (credentials: { email: string; password: string }) => {
    try {
        const response = await api.post("/user/login", credentials);
        return response.data;
    } catch (error) {
        console.error("Login failed:", error);
        throw error;
    }
};

export const registerUser = async (userData:    { name: string; email: string; password: string }) => {
    const response = await api.post("/users/create", userData);
    
    return response.data;
};

export const getUser = async(userId:number)=>{
    const response = await api.get(`/users/${userId}`);
    return response.data;
}

export default api;
