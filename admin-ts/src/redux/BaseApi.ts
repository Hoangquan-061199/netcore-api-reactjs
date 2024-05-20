import { fetchBaseQuery } from '@reduxjs/toolkit/query';
import { logout, setCredentials } from './auth/Auth.slice';

const baseQuery = fetchBaseQuery({
    baseUrl: import.meta.env.VITE_BASE_URL_API,
    credentials: 'include',
    prepareHeaders: (headers, { getState }: any) => {
        const token = getState().auth.token;
        if (token) {
            headers.set('Authorization', `Bearer ${token}`);
        }
        return headers;
    }
});

export const baseQueryWithReauth = async (args: any, api: any, extraOptions: any) => {
    let result = await baseQuery(args, api, extraOptions);

    if (result?.error?.status === 401) {
        console.log('sending refresh token');
        const refreshResult: any = await baseQuery({
            url: '/auth/refreshtoken',
            method: 'post',
        }, api, extraOptions);
        if (refreshResult?.data?.token) {
            api.dispatch(setCredentials({ token: refreshResult.data.token }));
            result = await baseQuery(args, api, extraOptions);
        } else {
            api.dispatch(logout());
        }
    }
    return result;
};