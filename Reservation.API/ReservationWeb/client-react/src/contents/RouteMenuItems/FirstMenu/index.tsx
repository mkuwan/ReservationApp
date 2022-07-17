import {RouteItems} from "../IRouteItems";
import Welcome from "../../Welcome/index.";
import DraftsIcon from "@mui/icons-material/Drafts";
import {Login} from "../../Login";
import {AppRegistration, LoginRounded} from "@mui/icons-material";
import {Registration} from "../../Registration";

export const FirstMenu: RouteItems[] = [
    {
        path: '/',
        content: <Welcome/>,
        icon: <DraftsIcon/>,
        iconColor: 'darkgreen',
        toolTip: 'Welcome!',
    },
    {
        path: '/welcome',
        content: <Welcome/>,
        icon: <DraftsIcon/>,
        iconColor: 'darkgreen',
        toolTip: 'Welcome!',
    },
    {
        path: '/login',
        content: <Login/>,
        icon: <LoginRounded/>,
        iconColor: 'darkgreen',
        toolTip: 'login'
    },
    {
        path: '/registration',
        content: <Registration/>,
        icon: <AppRegistration/>,
        iconColor: 'darkgreen',
        toolTip: 'login'
    }
]