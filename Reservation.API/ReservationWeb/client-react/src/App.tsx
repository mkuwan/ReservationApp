import React from 'react';
import logo from './logo.svg';
import './App.css';
import LayoutClient from "./LayoutClient";
import { Routes, Route } from 'react-router-dom';f
import ClientMenu from "./contents/ClientMenu/index.";
import {Login} from "./contents/Login";

function App() {
  return (
    <div className="App">
        <Routes>
            <Route path={'/'} element={<LayoutClient/>}>
                <Route index element={<ClientMenu/>}/>
                <Route path={'login'} element={<Login/>}/>
            </Route>
        </Routes>
    </div>
  );
}

export default App;
