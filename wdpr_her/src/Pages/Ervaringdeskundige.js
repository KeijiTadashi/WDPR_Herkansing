import React from "react";
import useLocalStorage from 'use-local-storage';
import Header from "../standaardformats/Header";
import "../CSS/StichtingTheme.css";
import { Link } from "react-router-dom";
import "../CSS/Ervaringdeskundige.css"


export const Ervaringdeskundige = () => {

    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    return (
        <>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header />
                <div className={"Body"}>

                    <div className="box">
                        <h3>Algemene informatie</h3>
                        <p>Name</p> 
                        <p>Postcode</p>
                        <p>E-mail</p>
                        <p>Telefoonnummer</p>
                        <Link to={"/"}>
                                    <button aria-label="Edit">Edit</button>
                        </Link>

                       
                    </div>

                    <div className="box">
                        <h3>Medische informatie</h3>
                        <h4>Deze informatie is alleen zichtbaar voor bedrijven waar je aan een onderzoek meedoet</h4>
                        <p>Type beperking</p>
                        <p>Hulpmiddelen</p>
                        <p>ETC</p>
                        <Link to={"/"}>
                                    <button aria-label="Edit">Edit</button>
                        </Link>

                        
                    </div>
                </div>
            </div >
        </>
    );
};

export default Ervaringdeskundige;