import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithReauth } from '../BaseApi'
import { UpdateAccountRequest, UpdateAccountResponse, UserHeader, UserUpdateGet } from '../../types/User.type'

export const userAdminApi = createApi({
  reducerPath: 'userAdminApi',
  tagTypes: ['useradmin'],
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    GetUserHeader: builder.query<UserHeader, void>({
      query: () => '/User/GetUserHeader'
    }),
    GetUserUpdate: builder.query<UserUpdateGet, void>({
      query: () => '/User/GetUserUpdate'
    }),
    UpdateAccountLogin: builder.mutation<UpdateAccountResponse, UpdateAccountRequest>({
      query: (body: any) => {
        const bodyFormData = new FormData()
        bodyFormData.append('fullName', body.fullName)
        bodyFormData.append('email', body.email)
        bodyFormData.append('file', body.file)
        return {
          url: '/User/UpdateAccountLogin',
          method: 'PATCH',
          // headers: {
          //     'Content-Type': 'multipart/form-data;',
          // },
          formData: true,
          body: bodyFormData
        }
      }
    })
  })
})

export const { useGetUserHeaderQuery, useGetUserUpdateQuery, useUpdateAccountLoginMutation } = userAdminApi
