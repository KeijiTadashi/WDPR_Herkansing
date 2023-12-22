import React from 'react';
import ReactDOM from 'react-dom/client';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import './index.css';
import App from './Pages/App';
import Scam from './Pages/Scam';
import reportWebVitals from './reportWebVitals';
import {ApiExample} from "./Pages/ApiExample";

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
