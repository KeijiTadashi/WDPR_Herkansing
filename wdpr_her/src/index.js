import React from 'react';
import ReactDOM from 'react-dom/client';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import './CSS/index.css';
import App from './Pages/App';
import Scam from './Pages/Onderzoeken';
import reportWebVitals from './reportWebVitals';
import {ApiExample} from "./Pages/ApiExample";
import {Login} from "./Pages/Login";
import Homepage from './Pages/Homepage';
import Beheerder from './Pages/Beheerder';
import Onderzoeken from './Pages/Onderzoeken';

const router = createBrowserRouter([
    {
        path: "/Onderzoeken",
        element: <Onderzoeken/>,
    },
    {
        path: "/",
        element: <App/>,
    },
    {
        path: "/Test",
        element: <ApiExample/>,
    },
    {
        path: "/Login",
        element: <Login/>,
    },
    {
        path: "/Home",
        element: <Homepage/>
    },
    {
        path: "/beheerder",
        element: <Beheerder/>
    }
    {
        path: "scam",
        element: <Scam/>
    }
]);


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    // <React.StrictMode>
    //   <App />
    // </React.StrictMode>

    <React.StrictMode>
        <RouterProvider router={router}/>
    </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
