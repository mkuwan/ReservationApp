import {useContext} from "react";
import {UserContext} from "../../contexts/UserContext";
import {UserRoles} from "../Contstants/UserRoles";

export const Schedule = () => {
    const { user } = useContext(UserContext);

    if(user.userRole === UserRoles.client){
        return(
            <>
                ClientのSchedule
            </>
        )
    }

    if(user.userRole === UserRoles.manager){
        return (
            <>
                ManagerのSchedule
            </>
        )
    }

    return (
        <>
            見ちゃだめー
        </>
    )
}