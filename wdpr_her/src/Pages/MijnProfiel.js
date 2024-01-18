import React, { useEffect, useState } from 'react';
import Header from '../standaardformats/Header';
import useLocalStorage from 'use-local-storage';
import "../CSS/MijnProfiel.css";
import { Link } from "react-router-dom";
import axios from "axios";
import { apiPath } from "../Helper/Api";

const MijnProfiel = () => {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    const [bedrijven, setBedrijven] = useState([]);

    useEffect(() => {
        getBedrijven();
    }, []);

    async function getBedrijven() {
        axios.get(apiPath + "Bedrijf/GetAllBedrijven")
            .then(response => {
                setBedrijven(JSON.parse(JSON.stringify(response.data)));
                console.log("Bedrijven:")
                console.log(response.data)
            })
            .catch(e => {
                console.log(e)
            })
    }

    // Alleen het tweede element weergeven
    const secondBedrijf = bedrijven[1];

    return (
        <div>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header />
                <div className={"Body"}>
                    {secondBedrijf && (
                        <div className="Profielblok">
                            <h3>Algemene informatie</h3>
                            <p><strong>Naam:</strong> {secondBedrijf.bedrijfsnaam}</p>
                            <p><strong>Postcode:</strong> 2523XT </p>
                            <p><strong>Kvk nummer:</strong> {secondBedrijf.kvk}</p>
                            <p><strong>E-mail:</strong> TheNightMare@gmail.com </p>
                            <p><strong>Telefoonnummer:</strong> 070 888 8187 </p>
                            <p><strong>Link:</strong> https://{secondBedrijf.website}</p>
                            <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
                                <Link to={"/"}>
                                    <button aria-label="Edit">Edit</button>
                                </Link>
                            </div>

                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}

export default MijnProfiel;
