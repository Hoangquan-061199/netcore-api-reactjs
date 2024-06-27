import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithReauth } from '../BaseApi'
import { searchModel } from '../../types/SearchModel.type'
import { LogAdmin } from '../../types/LogAdmin.type'

export const logAdminApi = createApi({
  reducerPath: 'logAdminApi',
  tagTypes: ['logAdmin'],
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    GetLogAdmin: builder.query<LogAdmin[], searchModel>({
      query: (args: searchModel) => {
        return {
          methods: 'GET',
          url: '/LogAdmin',
          headers: {
            'content-type': 'application/json'
          },
          params: args
        }
      }
    })
  })
})

export const { useGetLogAdminQuery } = logAdminApi
