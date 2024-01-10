import React from 'react';
import ReactDOM from 'react-dom/client';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import './CSS/index.css';
import App from './Pages/App';
import Scam from './Pages/Scam';
import reportWebVitals from './reportWebVitals';
import {ApiExample} from "./Pages/ApiExample";
import {Login} from "./Pages/Login";
import Homepage from './Pages/Homepage';

const router = createBrowserRouter([
    {
        path: "/Scam",
        element: <Scam/>,
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
