import {ReactNode} from "react";
import Welcome from "../../Welcome/index.";
import DraftsIcon from '@mui/icons-material/Drafts';
import {RouteItems} from "../IRouteItems";
import {Registration} from "../../Registration";
import {Login} from "../../Login";

export const ClientMenu: RouteItems[] = [
    {
        path: '/login',
        content: <Login/>,
        icon: <DraftsIcon/>,
        iconColor: 'darkgreen',
        toolTip: 'login',
        searchValue: 'login'
    },
    {
        path: '/registration',
        content: <Registration/>,
        icon: <DraftsIcon/>,
        iconColor: 'darkgreen',
        toolTip: '登録',
        searchValue: 'registration'
    },
]