// import 'bootstrap/dist/css/bootstrap.css';
import Header from "../standaardformats/Header";
import axios from "axios";
import {apiPath} from "../Helper/Api";
import {useEffect, useRef, useState} from "react";
import {Dropdown} from "react-bootstrap";
import useLocalStorage from "use-local-storage";
import {useNavigate} from "react-router-dom";


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

    const nav = useNavigate();


    useEffect(() => {
        
    }, [accountType]);
    
    const registreerErvaringsdeskundige = () => {
        const info = {
            Gebruikersnaam: usernameRef.current.value,
            Wachtwoord: passwordRef.current.value,
            Email: emailRef.current.value,
            Voornaam: voornaamRef.current.value,
            Achternaam: achternaamRef.current.value,
            Telefoonnummer: telefoonnummerRef.current.value,
            Postcode: postcodeRef.current.value
        };

        axios
            .post(apiPath + "RegistreerErvaringsdeskundige", info)
            .then(() => {
                nav("/Login");
        }).catch((err) => {
            console.log(err.toJSON());
        });}
    
    

    return (
        // <Layout>
        <>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header Title={"Login"}/>
                <div className={"Body"}>
                    <Dropdown 
                        id={"account-type"} 
                        title={accountType} 
                        onSelect={(e => {
                            setAccountType(e);
                        })}
                        autoClose={true}
                    >
                        <Dropdown.Toggle id={"account-type-toggle"} >{accountType}</Dropdown.Toggle>
                        <Dropdown.Menu>
                            <Dropdown.Item eventKey={"Ervaringsdeskundige"}>Ervaringsdeskundige</Dropdown.Item>
                            <Dropdown.Item eventKey={"Bedrijf"}>Bedrijf</Dropdown.Item>
                        </Dropdown.Menu>
                    </Dropdown>

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
                                <button
                                    aria-label="Registreer knop"
                                    type="button"
                                    onClick={registreerErvaringsdeskundige}
                                >
                                    Registreer {accountType}
                                </button>
                            </> :
                            // Else bedrijf
                            <></>
                        }
                    </form>
                    
                   
                    {/*<form>*/}
                    {/*    <label htmlFor="username">Gebruikersnaam</label>*/}
                    {/*    <br />*/}
                    {/*    <input*/}
                    {/*        type={"text"}*/}
                    {/*        id={"username"}*/}
                    {/*        ref={usernameRef}*/}
                    {/*        aria-label="Invoerveld gebruikersnaam"*/}
                    {/*        className="inputFontSize"*/}
                    {/*    >*/}
                    {/*    </input>*/}
                    
                    {/*    <br />*/}
                    
                    {/*    <label htmlFor="password">Wachtwoord</label>*/}
                    {/*    <br />*/}
                    {/*    <input*/}
                    {/*        type={"password"}*/}
                    {/*        id={"password"}*/}
                    {/*        ref={passwordRef}*/}
                    {/*        aria-label="Invoerveld wachtwoord"*/}
                    {/*        className="inputFontSize"*/}
                    {/*    >*/}
                    {/*    </input>*/}
                    {/*    <br />*/}
                    
                    {/*    <button*/}
                    {/*        aria-label="Log in"*/}
                    {/*        type="button"*/}
                    {/*        onClick={() =>*/}
                    {/*        {*/}
                    {/*            const info = {*/}
                    {/*                Gebruikersnaam: usernameRef.current.value,*/}
                    {/*                Wachtwoord: passwordRef.current.value*/}
                    {/*            };*/}
                    
                    {/*            axios*/}
                    {/*                .post(apiPath + "Login", info)*/}
                    {/*                .then(response => {*/}
                    {/*                    // const token = response.data.api_key;*/}
                    {/*                    SetAuthToken(response.data);*/}
                    
                    {/*                }).then(() => {*/}
                    {/*                setRole(localStorage.getItem('role'));*/}
                    {/*            }).catch((err) => {*/}
                    {/*                console.log(err.toJSON());*/}
                    {/*                setRole(false);*/}
                    {/*            });}*/}
                    {/*        }*/}
                    {/*    >*/}
                    {/*        Log in*/}
                    {/*    </button>*/}
                    {/*</form>*/}
                </div>
            </div>
        </>
    )
}