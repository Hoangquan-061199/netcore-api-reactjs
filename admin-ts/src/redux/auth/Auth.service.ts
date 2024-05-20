import { createApi } from '@reduxjs/toolkit/query/react';
import { LoginRequest, LoginResponse, LogoutResponse } from '../../types/Auth.type';
import { baseQueryWithReauth } from '../BaseApi';

export const  authApi = createApi({
    reducerPath: 'authApi',
    tagTypes: ['auth'], 
    baseQuery: baseQueryWithReauth,
    endpoints: (builder) => ({
        login: builder.mutation<LoginResponse, LoginRequest>({
            query: (body) => {
                return {
                    url: '/Auth/Login',
                    method: 'POST',
                    headers: {
                        'content-type': 'application/json',
                    },
                    body,
                };
            },
        }),
        logout: builder.mutation<LogoutResponse, void>({
            query: () => {
                return {
                    url: '/Auth/Logout',
                    method: 'POST',
                };
            },
         })
    }),
});

export const { useLoginMutation, useLogoutMutation } = authApi;