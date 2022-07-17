import {useContext} from "react";
import {UserContext} from "../../../contexts/UserContext";
import {UserRoles} from "../../Contstants/UserRoles";
import {Button, Grid} from "@mui/material";
import {useNavigate} from "react-router-dom";

export const ScheduleForManager = () => {
    const { user } = useContext(UserContext);
    const navigate =useNavigate();

    if(user.userRole === UserRoles.manager){
        return(
            <div className={'container justify-content-center'}>
                <h4>{`管理者メニュー ${user.userName} が処理しています`}</h4>
                <Grid container
                      justifyContent={'center'}
                      alignItems={'center'}
                      direction={'row'}>
                    <Button variant={'contained'}
                            color={'success'}
                            className={'col-2 '}
                            onClick={() => navigate('/login')}>
                        戻る
                    </Button>

                    <Button variant={'contained'}
                            className={'col-2'}>
                        承認とかいろいろ
                    </Button>

                </Grid>
            </div>
        )
    }

    return (
        <>
            DEMO
        </>
    )
}