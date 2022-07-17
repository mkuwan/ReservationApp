import {ReactNode} from "react";

export interface RouteItems {
    path: string;
    content: ReactNode;
    icon?: ReactNode;
    iconColor?: string;
    toolTip?: string;
    searchValue: string;
    items?: RouteItems[];
}