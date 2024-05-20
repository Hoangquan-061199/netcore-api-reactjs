export type UserType = {
    UserHeader: UserHeader | null;
    UserUpdateGet: UserUpdateGet | null;
};

export type UserHeader = {
    fullName: string;
    urlPicture: string;
    roles: string;
    userId: string;
};

export type UserUpdateGet = {
    fullName: string;
    urlPicture: string;
    roles: string;
    userId: string;
    departmentName: string;
    email: string;
    createdDate: string;
    userName: string;
};
