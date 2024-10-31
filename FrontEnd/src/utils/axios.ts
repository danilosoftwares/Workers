import axios from "axios";

const baseURL = 'https://localhost:5001';
const http = axios.create({ baseURL: baseURL });

export default http;
