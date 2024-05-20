import { createApi } from '@reduxjs/toolkit/query/react';
import { baseQueryWithReauth } from '../BaseApi';
import { UserHeader, UserUpdateGet } from '../../types/User.type';

export const  userAdminApi = createApi({
    reducerPath: 'userAdminApi',
    tagTypes: ['useradmin'], 
    baseQuery: baseQueryWithReauth,
    endpoints: (builder) => ({
        GetUserHeader: builder.query<UserHeader, void>({
            query: () => '/User/GetUserHeader'
        }),
        GetUserUpdate: builder.query<UserUpdateGet, void>({
            query: () => '/User/GetUserUpdate'
        })
    }),
});

export const { useGetUserHeaderQuery, useGetUserUpdateQuery } = userAdminApi;