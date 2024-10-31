export interface WorkersAllReponse {
    id: number;
    firstName: string;
    lastName: string;
    corporateEmail: string;
    workerNumber: string;
    leaderName?: string | null;
    phones?: string[] | null;
}

export interface TokenResponse {
    access_token: string;
    token_type: string;
    expires_in: number;
}

export interface UserResponse {
    id: number;
    email: string;
}

export interface LoginReponse {
    status: boolean;
    message?: string;
    user?: UserResponse;
    token?: TokenResponse
}

export interface WorkersItem {
    id: number;
    firstName: string;
    lastName: string;
    corporateEmail: string;
    workerNumber: string;
    leaderName?: string | null;
    passwordHash: string;
    phones?: string[] | null;
}

export interface DefaultBaseResponse<T> {
    status: boolean;
    message?: string;
    content?: T;
}