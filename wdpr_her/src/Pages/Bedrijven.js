import React, { useEffect, useState } from 'react'
import Header from '../standaardformats/Header';
import useLocalStorage from 'use-local-storage';
import "../Bedrijven.css"
import axios from "axios";
import { apiPath } from "../Helper/Api";

const Bedrijven = () => {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    // const [bedrijvenList, setBedrijvenList] = useState([{ Naam: "testBedrijf", Kvk: "28194738", Locatie: "adres", Website: "https://www.gogole.com" },
    // { Naam: "tBedrijf2", Kvk: "00028194738", Locatie: "adres2", Website: "https://www.gogole2.com" }])

    const [bedrijven, setBedrijven] = useState([])
    // we gebruiken useState om de lijst van de bedrijven op te slaan

    useEffect(() => {       // useEffect zorgt in dit geval voor elke refresh dat Getbedrijven worden aangeroepen
        getBedrijven()
    }, [])

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

    return (
        <div>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header />
                <div className={"Body"}>
                    <p>Bedrijven ({bedrijven.length})</p>
                    {bedrijven.map((bedrijf, i) => {
                        // .map = voor elke object returnt er een div
                        return <div className="Bedrijfblok" key={i}>
                            <p>Naam: {bedrijf.bedrijfsnaam}</p>
                            <p>Kvk: {bedrijf.kvk}</p>
                            <p>Locatie: {bedrijf.locatie}</p>
                            <p>Website: {bedrijf.website}</p>
                        </div>
                    }
                    )
                    }

                </div>
            </div>
        </div>

    )

}


export default Bedrijven;