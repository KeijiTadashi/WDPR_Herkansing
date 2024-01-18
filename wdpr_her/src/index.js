import React from 'react';
import ReactDOM from 'react-dom/client';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import './CSS/index.css';
import App from './Pages/App';
import ErvaringsdeskundigeOnderzoek from './Pages/ErvaringsdeskundigeOnderzoek';
import Onderzoeken from './Pages/Onderzoeken';
import reportWebVitals from './reportWebVitals';
import {ApiExample} from "./Pages/ApiExample";
import {Login} from "./Pages/Login";
import Homepage from './Pages/Homepage';
import Beheerder from './Pages/Beheerder';
//import Ervaringdeskundige from './Pages/Ervaringdeskundige';
import Bedrijven from './Pages/Bedrijven';
import MijnProfiel from './Pages/MijnProfiel';

import Ervaringdeskundige from './Pages/Ervaringdeskundige';
import Scam from './Pages/Scam';

const router = createBrowserRouter([
    {
        path: "/ErvaringsdeskundigeOnderzoek",
        element: <ErvaringsdeskundigeOnderzoek/>,
    },
    {
        path: "/Onderzoeken",
        element: <Onderzoeken/>,
    },
    {
        path: "/App",
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
        path: "/",
        element: <Homepage/>
    },
    {
        path: "/beheerder",
        element: <Beheerder/>
    },
    {
        path: "/Bedrijven",
        element: <Bedrijven/>
    },
    {
        path: "/Ervaringdeskundige",
        element: <Ervaringdeskundige/>
    },
    {
        path: "/MijnProfiel",
        element: <MijnProfiel/>
    },
    {
        path: "Scam",
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
