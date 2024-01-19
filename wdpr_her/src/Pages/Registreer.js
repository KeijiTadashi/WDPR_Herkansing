// import 'bootstrap/dist/css/bootstrap.css';
import Header from "../standaardformats/Header";
import axios from "axios";
import {apiPath} from "../Helper/Api";
import {useEffect, useRef, useState} from "react";
import useLocalStorage from "use-local-storage";
import {useNavigate} from "react-router-dom";
import '../CSS/StichtingTheme.css';


export function Registreer() {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');
    
    
    const [accountType, setAccountType] = useState("Ervaringsdeskundige");
    const usernameRef = useRef(null);
    const passwordRef = useRef(null);
    const voornaamRef = useRef(null);
    const achternaamRef = useRef(null);
    const emailRef = useRef(null);
    const telefoonnummerRef = useRef(null);
    const postcodeRef = useRef(null);
    const websiteRef = useRef(null);
    const bedrijfsnaamRef = useRef(null);
    const locatieRef = useRef(null);
    const kvkRef = useRef(null);

    const nav = useNavigate();
    
    const changeAccountType = (e) => {
        setAccountType(e.target.value);
    }

    useEffect(() => {
        
    }, [accountType]);
    
    const registreerAccount = () => {
        let path = "";
        let info = {};
        if (accountType === "Ervaringsdeskundige") {
            info = {
                Gebruikersnaam: usernameRef.current.value,
                Wachtwoord: passwordRef.current.value,
                Email: emailRef.current.value,
                Voornaam: voornaamRef.current.value,
                Achternaam: achternaamRef.current.value,
                Telefoonnummer: telefoonnummerRef.current.value,
                Postcode: postcodeRef.current.value
            };

            path = apiPath + "RegistreerErvaringsdeskundige";
        }
        else if (accountType === "Bedrijf") {
            info = {
                Gebruikersnaam: usernameRef.current.value,
                Wachtwoord: passwordRef.current.value,
                Email: emailRef.current.value,
                Telefoonnummer: telefoonnummerRef.current.value,
                Bedrijfsnaam: bedrijfsnaamRef.current.value,
                Website: websiteRef.current.value,
                Locatie: locatieRef.current.value,
                Kvk: kvkRef.current.value
            };

            path = apiPath + "RegistreerBedrijf";
        }

        axios
            .post(path, info)
            .then(() => {
                nav("/Login");
            }).catch((err) => {
            console.log(err.toJSON());
        });
    }
    
    

    return (
        <>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header Title={"Registreer"}/>
                <div className={"Body"}>
                    <form className={"Section-border"}>
                        Account type
                        <div className={"radio"}>
                            <label><input type={"radio"} value={"Ervaringsdeskundige"} checked={accountType === "Ervaringsdeskundige"} onChange={changeAccountType}/>Ervaringsdeskundige</label>
                            <br/>
                            <label><input type={"radio"} value={"Bedrijf"} checked={accountType === "Bedrijf"} onChange={changeAccountType}/>Bedrijf</label>
                        </div>
                        <br/>
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
                        <br /><label htmlFor={"email"}>E-mail</label>
                        <br/>
                        <input
                            type={"email"}
                            id={"email"}
                            ref={emailRef}
                            aria-label="Invoerveld Email"
                            className="inputFontSize"
                        >
                        </input>
                        <br />
                        <label htmlFor={"telefoonnummer"}>Telefoonnummer</label>
                        <br/>
                        <input
                            type={"tel"}
                            id={"telefoonnummer"}
                            ref={telefoonnummerRef}
                            aria-label="Invoerveld Telefoonnummer"
                            className="inputFontSize"
                        >
                        </input>

                        <br />
                        {accountType === "Ervaringsdeskundige" ?
                            <>
                                <label htmlFor={"voornaam"}>Voornaam</label>
                                <br/>
                                <input
                                    type={"text"}
                                    id={"voornaam"}
                                    ref={voornaamRef}
                                    aria-label="Invoerveld Voornaam"
                                    className="inputFontSize"
                                >
                                </input>
                                <br />
                                <label htmlFor={"achternaam"}>Achternaam</label>
                                <br/>
                                <input
                                    type={"text"}
                                    id={"achternaam"}
                                    ref={achternaamRef}
                                    aria-label="Invoerveld achternaam"
                                    className="inputFontSize"
                                >
                                </input>
                                <br />
                                <label htmlFor={"postcode"}>Postcode</label>
                                <br/>
                                <input
                                    type={"text"}
                                    id={"postcode"}
                                    ref={postcodeRef}
                                    aria-label="Invoerveld postcode"
                                    className="inputFontSize"
                                >
                                </input>
                                <br />
                            </> :
                            // Else bedrijf
                            <>
                                <label htmlFor={"bedrijfsnaam"}>Bedrijfsnaam</label>
                                <br/>
                                <input
                                    type={"text"}
                                    id={"bedrijfsnaam"}
                                    ref={bedrijfsnaamRef}
                                    aria-label="Invoerveld bedrijfsnaam"
                                    className="inputFontSize"
                                >
                                </input>
                                <br /><label htmlFor={"website"}>Website</label>
                                <br/>
                                <input
                                    type={"url"}
                                    id={"website"}
                                    ref={websiteRef}
                                    aria-label="Invoerveld website"
                                    className="inputFontSize"
                                >
                                </input>
                                <br />
                                <label htmlFor={"kvk"}>Kvk</label>
                                <br/>
                                <input
                                    type={"text"}
                                    id={"kvk"}
                                    ref={kvkRef}
                                    aria-label="Invoerveld kvk nummer"
                                    className="inputFontSize"
                                >
                                </input>
                                <br />
                                <label htmlFor={"locatie"}>Locatie</label>
                                <br/>
                                <input
                                    type={"text"}
                                    id={"locatie"}
                                    ref={locatieRef}
                                    aria-label="Invoerveld locatie"
                                    className="inputFontSize"
                                >
                                </input>
                                <br />
                            </>
                        }
                        <button
                            className={"Button-body"}
                            aria-label="Registreer knop"
                            type="button"
                            onClick={registreerAccount}
                        >
                            Registreer {accountType}
                        </button>
                    </form>
                </div>
            </div>
        </>
    )
}