import { createSlice } from '@reduxjs/toolkit';
import { UserType } from '../../types/User.type';

const initialState: UserType = {
    UserHeader: {
        fullName: '',
        urlPicture: '',
        roles: '',
        userId: '',
    },
    UserUpdateGet: {
        fullName: '',
        urlPicture: '',
        roles: '',
        userId: '',
        departmentName: '',
        email: '',
        createdDate: '',
        userName: '',
    },
    UserUpdateLogin: {
        fullName: '',
        urlPicture: '',
        email: '',
    },
};

const userAdminSlice = createSlice({
    name: 'userAdmin',
    initialState,
    reducers: {
        userHeader: (state, action) => {
            state.UserHeader = {
                fullName: action.payload.fullName,
                urlPicture: action.payload.urlPicture,
                roles: action.payload.roles,
                userId: action.payload.userId,
            };
        },
        userUpdateGet: (state, action) => {
            state.UserUpdateGet = {
                fullName: action.payload.fullName,
                urlPicture: action.payload.urlPicture,
                roles: action.payload.roles,
                userId: action.payload.userId,
                departmentName: action.payload.departmentName,
                email: action.payload.email,
                createdDate: action.payload.createdDate,
                userName: action.payload.userName,
            };
        },
        UpdateAccountLogin: (state, action) => {
            state.UserUpdateLogin = {
                fullName: action.payload.fullName,
                urlPicture: action.payload.urlPicture,
                email: action.payload.email,
            };
        },
    },
});

const userAdminReduce = userAdminSlice.reducer;
export const { userHeader, userUpdateGet, UpdateAccountLogin } = userAdminSlice.actions;
export default userAdminReduce;
// export const selectCurrentUserUpdateLogin = (state: any) => state.UserUpdateLogin;
export const selectCurrentUserUpdateLogin = (state: UserType) => state.UserUpdateLogin;
