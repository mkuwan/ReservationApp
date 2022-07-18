import {Button, Typography} from "@mui/material";
import { useNavigate} from "react-router-dom";
import {ClientMenu} from "../RouteMenuItems/ClientMenu";
import {useContext, useEffect, useState} from "react";
import {UserContext} from "../../contexts/UserContext";
import {IUser} from "../../types/UserType";
import {UserRoles} from "../Contstants/UserRoles";
import {useLocation} from "react-router";

export const Login = () => {
    const {login, isLogin, logout, user } = useContext(UserContext);
    const navigate = useNavigate();

    const navSchedule = ClientMenu.find(x => x.searchValue === 'scheduleForClient');
    const navSchedulePath = navSchedule ? navSchedule.path : '/';

    const demoUser: IUser = {
        userId: "demoClient",
        userName: '患者A',
        userRole: UserRoles.client,
        lastLoginDateTime: new Date(Date.now())
    }

    logout();

    // const location = useLocation();
    // const [locationKeys, setLocationKeys] = useState<string[]>([]);
    //
    // useEffect(() => {
    //     logout();
    //
    //     return history.listen(() => {
    //         if(history.action === 'PUSH'){
    //             setLocationKeys([location.key])
    //         }
    //
    //         if(history.action === 'POP'){
    //             if(locationKeys[1] === location.key){
    //                 setLocationKeys(([_, ...keys]) => keys);
    //
    //                 // handle forward event
    //                 alert('forward event');
    //             } else {
    //                 setLocationKeys((keys) => [location.key, ...keys]);
    //
    //                 // handle back event
    //                 alert('back event');
    //             }
    //         }
    //     })
    // }, [locationKeys]);

    return (
        <div >
            <h4 className={'my-5'}>ログインフォーム</h4>
            <h5>{`${user.userName}`}</h5>

            <Button variant={'contained'}
                    color={'success'}
                    className={'col-2 '}
                    onClick={() => navigate('/')}>
                戻る
            </Button>
            <Button variant={"contained"}
                    style={{color: 'black', backgroundColor: 'lightgreen'}}
                    sx={{height: 250, width: 250}}
                    onClick={() => {
                        login(demoUser);
                        navigate(navSchedulePath);
                    }}>
                <Typography variant={"h4"}>予約へ</Typography>
            </Button>
        </div>
    )
}