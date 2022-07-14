import { Outlet } from "react-router-dom";
import Header from "./Header";
import {Route} from "react-router";
import Grid2 from "@mui/material/Unstable_Grid2";
import {Box} from "@mui/material";

export const LayoutClient = () => {

    return(
        <>
            <Box sx={{ mt: 5, mb: 5 }}>
                <Header/>
            </Box>
            <Box>
                <Outlet/>
            </Box>
        </>

    )
}

export default LayoutClient;