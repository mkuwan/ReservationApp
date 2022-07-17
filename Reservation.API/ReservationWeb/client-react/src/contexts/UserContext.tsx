import {InitIUser, IUser} from "../types/UserType";
import {v5 as uuid} from "uuid";
import {createContext, ReactElement, useCallback, useState} from "react";

type UserContext = {
    user: IUser;
    isLogin: boolean;
    login: (user: IUser) => void;
    logout: () => void;
    token: string | null;
}

const KEY_USER_TOKEN = uuid.toString();
const KEY_USER_LOGIN = uuid.toString();

export const UserContext = createContext<UserContext>({} as UserContext);


export  const UserProvider = ({children}: {children: ReactElement}) => {
    const sessionToken = sessionStorage.getItem(KEY_USER_TOKEN);
    const [token, setToken] = useState(sessionToken ? sessionToken : null);

    const sessionLogin = sessionStorage.getItem(KEY_USER_LOGIN);
    const tempUser : IUser = JSON.parse(sessionLogin ? sessionLogin : JSON.stringify(InitIUser));
    const [user, setUser] = useState(tempUser);

    const [isLogin, setIsLogin] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    /**
     * ログイン
     */
    const login = useCallback(async (user: IUser)=> {
        setIsLoading(true);

        user.lastLoginDateTime = new Date(Date.now());

        setToken(user.userId);
        sessionStorage.setItem(KEY_USER_TOKEN, user.userId);
        sessionStorage.setItem(KEY_USER_LOGIN, JSON.stringify(user));
        setUser(user);
        setIsLogin(true);

        setIsLoading(false);
    }, []);

    /**
     * ログアウト
     */
    const logout = useCallback(() => {
        setUser(InitIUser);
        setIsLoading(false);
        setToken(null);
        sessionStorage.removeItem(KEY_USER_TOKEN);
        sessionStorage.removeItem(KEY_USER_LOGIN);
    }, []);

    return (
        <UserContext.Provider value={{login, logout, isLogin, user, token}}>
            {!isLoading && children}
        </UserContext.Provider>
    )
}



