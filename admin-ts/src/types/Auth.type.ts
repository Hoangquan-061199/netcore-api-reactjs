export type LoginRequest = {
    username: string;
    password: string;
}

export type ChangePasswordRequest ={
    passwordOld: string;
    passwordNew: string;
    passwordConfirm: string;
}

//------------------------------------------

export type LoginResponse = {
    token: string;
    message: string;
}

export type LogoutResponse ={
    message: string;
}




