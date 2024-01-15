import React, { useEffect, useState } from 'react'
import Header from '../standaardformats/Header';
import useLocalStorage from 'use-local-storage';
import "../Bedrijven.css"
import axios from "axios";
import {apiPath} from "../Helper/Api";

const Bedrijven = () => {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    const [bedrijvenList, setbedrijvenList] = useState([{ Naam: "testBedrijf", Kvk: "28194738", Locatie: "adres", Website: "https://www.gogole.com" },
    { Naam: "tBedrijf2", Kvk: "00028194738", Locatie: "adres2", Website: "https://www.gogole2.com" }])

    // we gebruiken useState om de lijst van de bedrijven op te slaan

    useEffect(() => {       // useEffect zorgt in dit geval voor elke refresh dat Getbedrijven worden aangeroepen
        getBedrijven()
    }, [])

    async function getBedrijven() {
        axios.get(apiPath + "Bedrijf/GetAllBedrijven")
            .then(response => {
                console.log(response.data);
                setbedrijvenList(response.data);
            })
            .catch(e => {
                console.log(e)
            })
        // try {
        //     await fetch("http://localhost:5027/Bedrijf/GetAllBedrijven").then(res=>res.json()).then(data=>{
        //     setbedrijvenList(data)
        // })
        // }catch{}
        //
    }

    return (
        <div>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header />
                <div className={"Body"}>
                    <p>Bedrijven ({bedrijvenList.length})</p>
                    {bedrijvenList.map((bedrijf, i) => {
                        // .map = voor elke object returnt er een div
                        return <div className="Bedrijfblok" key={i}>
                            <p>Naam: {bedrijf.Naam}</p>
                            <p>Kvk: {bedrijf.Kvk}</p>
                            <p>Locatie: {bedrijf.Locatie}</p>
                            <p>Website: {bedrijf.Website}</p>
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