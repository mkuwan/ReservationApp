import React, {useContext} from 'react';
import logo from './logo.svg';
import './App.css';
import Layout from "./Layout";
import {Routes, Route, Navigate} from 'react-router-dom';
import {Login} from "./contents/Login";
import {UserContext} from "./contexts/UserContext";
import {ClientMenu} from "./contents/RouteMenuItems/ClientMenu";
import {FirstMenu} from "./contents/RouteMenuItems/FirstMenu";
import {UserRoles} from "./contents/Contstants/UserRoles";
import Welcome from "./contents/Welcome/index.";

const App = () => {
    const {login, isLogin, user } = useContext(UserContext);

    if(!isLogin) {
        return (
            <div className="App" >
                <Routes>
                    <Route path={'/'} element={<Layout/>}>
                        { FirstMenu.map((item, index) =>(
                            <Route path={item.path}
                                   element={item.content}
                                   key={index}/>
                        ))}
                    </Route>
                </Routes>
            </div>
        )
    }

    if(isLogin && user.userRole === UserRoles.client) {
        return (
            <div className="App" >
                <Routes>
                    <Route path={'/'} element={<Layout/>}>
                        { ClientMenu.map((item, index) => (
                            <Route path={item.path}
                                   element={item.content}
                                   key={index}/>
                        ))}
                    </Route>
                </Routes>
            </div>
        )
    }

    if(isLogin && user.userRole === UserRoles.manager){
        return (
            <>
                Managerのページ
            </>
        )
    }

    return (
        <>
            <h1>ここはどこ？</h1>
        </>
    )
}

export default App;
