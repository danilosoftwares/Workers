import { AxiosError } from "axios";
import http from "../utils/axios";
import { DefaultBaseResponse, LoginReponse, TokenResponse, WorkersAllReponse, WorkersItem } from "./types";

const getToken = () => {
  const token = localStorage.getItem(`auth`);
  try{
    return token ? (JSON.parse(token) as TokenResponse).access_token : '';
  } catch(e){
    console.log(e);
  }
}

const makeHeader = () => {
  return { headers: { "Authorization": `Bearer ${getToken()}` } }
}


export const GetAllWorkers = async (): Promise<WorkersAllReponse[]> => {
  try {
    const response = await http.get<WorkersAllReponse[]>(`/workers/all`, makeHeader());
    return response.data;
  } catch (error) {
    const err = error as AxiosError
    throw err;
  }
}

export const GetWorker = async (id:string): Promise<WorkersAllReponse> => {
  try {
    const response = await http.get<WorkersAllReponse>(`/workers/${id}`, makeHeader());
    return response.data;
  } catch (error) {
    const err = error as AxiosError
    throw err;
  }
}

export const EditWorker = async (worker:WorkersItem): Promise<DefaultBaseResponse<WorkersItem>> => {
  try {
    const response = await http.put<DefaultBaseResponse<WorkersItem>>(`/workers/${worker.id}`, worker, makeHeader());
    return response.data;
  } catch (error) {
    const err = error as AxiosError
    throw err;
  }
}

export const CreateWorker = async (worker:WorkersItem): Promise<DefaultBaseResponse<WorkersItem>> => {
  try {
    const response = await http.post<DefaultBaseResponse<WorkersItem>>(`/workers`, worker, makeHeader());
    return response.data;
  } catch (error) {
    const err = error as AxiosError
    throw err;
  }
}

export const RemoveWorker = async (id:number): Promise<DefaultBaseResponse<string>> => {
  try {
    const response = await http.delete<DefaultBaseResponse<string>>(`/workers/${id.toString()}`, makeHeader());
    return response.data;
  } catch (error) {
    const err = error as AxiosError
    throw err;
  }
}

export const PostLogin = async (user: string, pass: string): Promise<LoginReponse> => {
  try {
    const payload = {
      email: user,
      passwordHash: pass
    }
    const response = await http.post<LoginReponse>(`/user/login`, payload);
    return response.data;
  } catch (error) {
    const err = error as AxiosError
    return err.response?.data as LoginReponse;
  }
}

export const PostRegister = async (user: string, pass: string): Promise<LoginReponse> => {
  try {
    const payload = {
      email: user,
      passwordHash: pass
    }
    const response = await http.post<LoginReponse>(`/user/register`, payload);
    return response.data;
  } catch (error) {
    const err = error as AxiosError
    return err.response?.data as LoginReponse;
  }
}