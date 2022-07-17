export type IUser = {
    userId: string,
    userName: string,
    userRole: string,
    lastLoginDateTime: Date | null
}

export const InitIUser: IUser = {
    userId: "",
    userName: "",
    userRole: "",
    lastLoginDateTime: null
}