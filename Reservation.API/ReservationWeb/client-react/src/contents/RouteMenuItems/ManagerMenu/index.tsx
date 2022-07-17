import {ScheduleForClient} from "../../Scheduler/ScheduleForClient";
import {Schedule as ScheduleIcon} from "@mui/icons-material";
import {RouteItems} from "../IRouteItems";
import {ScheduleForManager} from "../../Scheduler/ScheduleForManager";

export const ManagerMenu: RouteItems[] = [
    {
        path: '/schedule-management',
        content: <ScheduleForManager/>,
        icon: <ScheduleIcon/>,
        iconColor: 'darkgreen',
        toolTip: '承認処理',
        searchValue: 'scheduleForManager'
    }
]