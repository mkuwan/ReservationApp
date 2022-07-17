import {
    Box, Button,
    Card,
    FormControl,
    FormControlLabel,
    FormLabel,
    Grid,
    Paper,
    Radio,
    RadioGroup,
    styled, Typography
} from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import {ChangeEvent, useState} from "react";
import {ClientMenu} from "../RouteMenuItems/ClientMenu";
import {RouteItems} from "../RouteMenuItems/IRouteItems";




export const Welcome = () => {
    const [spacing, setSpacing] = useState(5);
    const navigate = useNavigate();

    const navRegistration = ClientMenu.find(x => x.searchValue === 'registration');
    const navRegistrationPath = navRegistration ? navRegistration.path : '/';

    const navLogin = ClientMenu.find(x => x.searchValue === 'login');
    const navLoginPath = navLogin ? navLogin.path : '/';

    return (
        <Grid sx={{flexGrow:1}} container alignItems={"center"}
              justifyContent="center"
              style={{minHeight: '70vh'}} >
            <Grid item xs={12} sm={5} >
                <Button variant={"contained"}
                        style={{color: 'black', backgroundColor: 'lightgreen'}}
                        sx={{height: 250, width: 250}}
                        onClick={() => {navigate(navRegistrationPath)}}>
                    <Typography variant={"h4"}>初めての方</Typography>
                </Button>
            </Grid>
            <Grid item xs={12} sm={5} >
                <Button variant={'contained'}
                        style={{color: 'black', backgroundColor: 'deepskyblue'}}
                        sx={{height: 250, width: 250}}
                        onClick={() => {navigate(navLoginPath)}}>
                    <Typography variant={"h4"}>予約</Typography>
                </Button>
            </Grid>
        </Grid>
    );
}

export default Welcome;