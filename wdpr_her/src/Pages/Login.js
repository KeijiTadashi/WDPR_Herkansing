import Header from "../standaardformats/Header";
import { apiPath } from "../Helper/Api";
import axios from "axios";
// import {Layout} from "../standaardformats/Layout";
import { useRef } from "react";
import { SetAuthToken } from "../Helper/AuthToken";

import useLocalStorage from "use-local-storage";


export function Login() {
    const usernameRef = useRef(null);
    const passwordRef = useRef(null);

    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    return (
        // <Layout>
        <>
            <Header />
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <form>
                    <label for="UsernameField">Gebruikersnaam</label>
                    <br />
                    <input
                        type={"text"}
                        id={"username"}
                        ref={usernameRef}
                        name="UsernameField"
                        aria-label="Invoerveld gebruikersnaam"
                    >
                    </input>

                    <br />

                    <label for="PasswordField">Wachtwoord</label>
                    <br />
                    <input
                        type={"password"}
                        id={"password"}
                        ref={passwordRef}
                        name="PasswordField"
                        aria-label="Invoerveld wachtwoord"
                    >
                    </input>
                    <br />

                    <button
                        aria-label="Log in"
                        type="button"
                        onClick={() =>
                            LoginJWT(
                                usernameRef.current.value,
                                passwordRef.current.value
                            )
                        }
                    >
                        Log in
                    </button>
                </form>
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
            const token = response.data.api_key;
            SetAuthToken(token)
        })
        .catch((err) => console.log(err.toJSON()));
}