import { createSlice } from '@reduxjs/toolkit'
import { LoginResponse } from '../../types/Auth.type'

const initialState: LoginResponse = {
  token: '',
  message: ''
}

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setCredentials: (state, action) => {
      state.token = action.payload.data.token
      state.message = action.payload.message
    },
    logout: (state) => {
      state.token = ''
      state.message = ''
    }
  }
})

const authReducer = authSlice.reducer
export const { setCredentials, logout } = authSlice.actions
export default authReducer
export const selectCurrentToken = (state: any) => state.auth.token
