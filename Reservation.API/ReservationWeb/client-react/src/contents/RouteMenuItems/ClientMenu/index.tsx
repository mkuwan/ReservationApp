import {ReactNode} from "react";
import Welcome from "../../Welcome/index.";
import DraftsIcon from '@mui/icons-material/Drafts';

import {RouteItems} from "../IRouteItems";
import {Registration} from "../../Registration";
import {Login} from "../../Login";
import {ScheduleForClient} from "../../Scheduler/ScheduleForClient";
import {Schedule as ScheduleIcon} from "@mui/icons-material";

export const ClientMenu: RouteItems[] = [
    // {
    //     path: '/',
    //     content: <Welcome/>,
    //     icon: <DraftsIcon/>,
    //     iconColor: 'darkgreen',
    //     toolTip: 'Welcome!',
    //     searchValue: 'welcome'
    // },
    // {
    //     path: '/login',
    //     content: <Login/>,
    //     icon: <DraftsIcon/>,
    //     iconColor: 'darkgreen',
    //     toolTip: 'login',
    //     searchValue: 'login'
    // },
    {
        path: '/registration',
        content: <Registration/>,
        icon: <DraftsIcon/>,
        iconColor: 'darkgreen',
        toolTip: '登録',
        searchValue: 'registration'
    },
    {
        path: '/schedule',
        content: <ScheduleForClient/>,
        icon: <ScheduleIcon/>,
        iconColor: 'darkgreen',
        toolTip: '登録',
        searchValue: 'scheduleForClient'
    },
]