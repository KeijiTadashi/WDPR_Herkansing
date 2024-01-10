import Header from "../standaardformats/Header";
import {apiPath} from "../Helper/Api";
import axios from "axios";
// import {Layout} from "../standaardformats/Layout";
import {useRef} from "react";
import {SetAuthToken} from "../Helper/AuthToken";


export function Login() {
    const usernameRef = useRef(null);
    const passwordRef = useRef(null);

    return (
        // <Layout>
        <>
        <Header/>
            <form>
                <label for="UsernameField">Gebruikersnaam</label>
                <br/>
                <input 
                    type={"text"}
                    id={"username"}
                    ref={usernameRef}
                    name="UsernameField"
                >
                </input>

                <br/>

                <label for="PasswordField">Wachtwoord</label>
                <br/>
                <input
                    type={"password"}
                    id={"password"}
                    ref={passwordRef}
                    name="PasswordField"
                >
                </input>
                <br/>
                
                <button
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