import React, { useEffect, useState } from 'react'
import useLocalStorage from 'use-local-storage';
import Header from "../standaardformats/Header";
import "../CSS/StichtingTheme.css";
import axios from "axios";
import "../CSS/Ervaringsdeskundige.css"
import { apiPath } from "../Helper/Api";


export const Ervaringsdeskundige = () => {

    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    const [ervaring, setErvaringsdeskundige] = useState([])
    // we gebruiken useState om de lijst van de ervaringdeskundige op te slaan

    useEffect(() => {   // useEffect zorgt in dit geval voor elke refresh dat Getervaringdeskundige worden aangeroepen
        getErvaringsdeskundige()
    }, [])

    async function getErvaringsdeskundige() {
        axios.get(apiPath + "Ervaringsdeskundige/GetErvaringsdeskundige")
            .then(response => {
                setErvaringsdeskundige(JSON.parse(JSON.stringify(response.data)));
                console.log("Ervaringsdeskundige:")
                console.log(response.data)
            })
            .catch(e => {
                console.log(e)
            })
    }

    return (
        <div>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header />
                <div className={"Body"}>
                    <p>Ervaringsdeskundige</p>
                    <div className="deskundigeblok">
                        <p>Naam: {ervaring.voornaam}</p>
                        <p>Postcode: {ervaring.postcode}</p>
                        <p>email: {ervaring.email}</p>
                        <p>telnr: {ervaring.telefoonNummer}</p>
                    </div>
                </div>
            </div>
        </div>

    )

    // return (
    //     <>
    //         <div className="Main" data-theme={theme} data-font-size={fontSize}>
    //             <Header/>
    //             <div className={"Body"}>

    //                 <div className="box">
    //                     <h3>Algemene informatie</h3>
    //                     <p>Name</p>
    //                     <p>Postcode</p>
    //                     <p>E-mail</p>
    //                     <p>Telefoonnummer</p>
    //                     <Link to={"/"}>
    //                         <button aria-label="Edit">Edit</button>
    //                     </Link>


    //                 </div>

    //                 <div className="box">
    //                     <h3>Medische informatie</h3>
    //                     <h4>Deze informatie is alleen zichtbaar voor bedrijven waar je aan een onderzoek meedoet</h4>
    //                     <p>Type beperking</p>
    //                     <p>Hulpmiddelen</p>
    //                     <p>ETC</p>
    //                     <Link to={"/"}>
    //                         <button aria-label="Edit">Edit</button>
    //                     </Link>


    //                 </div>
    //             </div>
    //         </div>
    //     </>
    // );
};

export default Ervaringsdeskundige;