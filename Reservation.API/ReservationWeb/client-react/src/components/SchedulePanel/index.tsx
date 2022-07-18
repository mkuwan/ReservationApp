import {SchedulePanelType} from "../../types/SchedulePanelType";

export const SchedulePanel = (panelInfo: SchedulePanelType) => {
    return(
        <div className={'row container'}>
            <h5>{panelInfo.scheduleId}</h5><br/>
            <h5>{panelInfo.startDateTime.toLocaleString()}</h5><br/>
            <h5>{panelInfo.endDateTime.toLocaleString()}</h5>
        </div>
    )
}