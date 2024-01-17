import Header from "../standaardformats/Header";
import { apiPath } from "../Helper/Api";
import axios from "axios";
// import {Layout} from "../standaardformats/Layout";
import {useEffect, useRef, useState} from "react";
import {GetAuthRole, SetAuthToken} from "../Helper/AuthToken";

import "../CSS/StichtingTheme.css"
import useLocalStorage from "use-local-storage";
import {redirect, useNavigate} from "react-router-dom";


export function Login() {
    const usernameRef = useRef(null);
    const passwordRef = useRef(null);

    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');
    
    let [role, setRole] = useState(GetAuthRole());
    const nav = useNavigate();

    useEffect(() => {
        if (role === 'Beheerder') nav("/Beheerder");
        if (role === 'Ervaringsdeskundige') nav("/Ervaringsdeskundige");
        if (role === 'Bedrijf') nav("/Bedrijf");
    }, [role])
    
    return (
        // <Layout>
        <>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header Title={"Login"}/>
                <div className={"Body"}>
                <form>
                    <label htmlFor="username">Gebruikersnaam</label>
                    <br />
                    <input
                        type={"text"}
                        id={"username"}
                        ref={usernameRef}
                        aria-label="Invoerveld gebruikersnaam"
                        className="inputFontSize"
                    >
                    </input>

                    <br />

                    <label htmlFor="password">Wachtwoord</label>
                    <br />
                    <input
                        type={"password"}
                        id={"password"}
                        ref={passwordRef}
                        aria-label="Invoerveld wachtwoord"
                        className="inputFontSize"
                    >
                    </input>
                    <br />

                    <button
                        aria-label="Log in"
                        type="button"
                        onClick={() =>
                        {
                            const info = {
                                Gebruikersnaam: usernameRef.current.value,
                                Wachtwoord: passwordRef.current.value
                            };
                            
                            axios
                                .post(apiPath + "Login", info)
                                .then(response => {
                                    // const token = response.data.api_key;
                                    SetAuthToken(response.data);
                                    
                                }).then(() => {
                                    setRole(localStorage.getItem('role'));
                                }).catch((err) => {
                                    console.log(err.toJSON());
                                    setRole(false);
                                });}
                            // LoginJWT(
                            //     usernameRef.current.value,
                            //     passwordRef.current.value
                            // )
                        }
                    >
                        Log in
                    </button>
                </form>
                </div>
            </div>
        </>
        // </Layout>
    )
}

function LoginJWT(userName, password) {
    const info = {
        Gebruikersnaam: userName,
        Wachtwoord: password
    };

    axios
        .post(apiPath + "Login", info)
        .then(response => {
            // const token = response.data.api_key;
            SetAuthToken(response.data);
            // GetAuthRole();
        })
        .catch((err) => {
            console.log(err.toJSON());
            return false;
        });
}