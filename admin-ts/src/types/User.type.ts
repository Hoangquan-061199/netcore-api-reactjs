import { GetProp, UploadProps } from 'antd'

type FileType = Parameters<GetProp<UploadProps, 'beforeUpload'>>[0]

export type UserType = {
  UserHeader: UserHeader | null
  UserUpdateGet: UserUpdateGet | null
  UserUpdateLogin: UserUpdateLogin | null
}

export type UserHeader = {
  fullName: string
  urlPicture: string
  roles: string
  userId: string
}

export type UserUpdateGet = {
  fullName: string
  urlPicture: string
  roles: string
  userId: string
  departmentName: string
  email: string
  createdDate: string
  userName: string
}

export type UserUpdateLogin = {
  fullName: string
  urlPicture: string
  email: string
}

export type UpdateAccountRequest = {
  fullName: string
  email: string
  file: FileType | undefined
}

export type UpdateAccountResponse = {
  fullName: string
  email: string
  urlPicture: string
}

export type ModalPermissions = {
  type: 'role' | 'tree' | 'add' | 'edit'
  title: string
  width?: number
  // userId: string;
}
