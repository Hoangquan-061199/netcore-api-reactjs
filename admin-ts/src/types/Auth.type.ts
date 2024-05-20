export type LoginRequest = {
    username: string;
    password: string;
}

export type LoginResponse = {
    token: string;
    message: string;
}

export type LogoutResponse ={
    message: string;
}