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
    styled
} from "@mui/material";
import { Link } from "react-router-dom";
import {ChangeEvent, useState} from "react";


const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));

export const ClientMenu = () => {
    const [spacing, setSpacing] = useState(5);

    return (
        <Grid sx={{ flexGrow: 1 }} container spacing={2}>
            <Grid item xs={12}>
                <Grid container justifyContent="center" spacing={spacing}>
                    <Grid item xs={5}>
                        <Button sx={{ height: 300}}
                            onClick={() => {console.log('初めての方をクリックした')}}
                        >
                            初めての方
                        </Button>
                    </Grid>
                    <Grid item xs={5}>
                        <Button sx={{ height: 300}}>
                            予約
                        </Button>
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
    );
}

export default ClientMenu;