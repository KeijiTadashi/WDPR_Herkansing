import useLocalStorage from "use-local-storage";
import Header from "../standaardformats/Header";
import {Link} from "react-router-dom";
import React, {useEffect, useState} from "react";
import axios from "axios";
import {apiPath} from "../Helper/Api";


export function Bedrijf() {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    // const [bedrijven, setBedrijven] = useState([]);
    const [onderzoeken, setOnderzoeken] = useState([]);

    useEffect(() => {
        // getBedrijven()
        getOnderzoeken();
    }, [])

    // async function getBedrijven() {
    //     axios.get(apiPath + "Bedrijf/GetAllBedrijven")
    //         .then(response => {
    //             setBedrijven(JSON.parse(JSON.stringify(response.data)));
    //             console.log("Bedrijven:")
    //             console.log(response.data)
    //         })
    //         .catch(e => {
    //             console.log(e)
    //         })
    // }

    const getOnderzoeken = () => {
        axios.get(apiPath + "Onderzoek/GetOwnOnderzoeken")
            .then(response => {
                setOnderzoeken(response.data);
            })
    }

    return (
        <>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header Title={"Bedrijfsportaal"}/>
                <div className={"Body"}>
                    <div className={"Section-border"}>
                    <table>
                            <thead>
                            <tr>
                                <th>Titel</th>
                                <th>Beschrijving</th>
                            </tr>
                            </thead>
                            <tbody>
                            {onderzoeken.map((onderzoek) => {
                                return (
                                    <tr key={onderzoek.id}>
                                        <td>{onderzoek.titel}</td>
                                        <td>{onderzoek.beschrijving}</td>
                                    </tr>
                                );
                            })}
                            </tbody>
                        </table>

                        {/* <table>
                            <thead>
                            <tr>
                                <th>Bedrijf</th>
                                <th>Website</th>
                                <th>Kvk</th>
                            </tr>
                            </thead>
                            <tbody>
                            {bedrijven.map((bedrijf) => {
                                return (
                                    <tr key={bedrijf.id}>
                                        <td>{bedrijf.bedrijfsnaam}</td>
                                        <td>{bedrijf.website}</td>
                                        <td>{bedrijf.kvk}</td>
                                    </tr>
                                );
                            })}
                            </tbody>
                        </table> */}
                        {/* Todo Add list of onderzoeken met buttons, eerst onderzoeken in de database, voor nu een lijst met bedrijven (eigenlijk voor Beheerder) */}
                    </div>
                    <Link to={"/CreateOnderzoek"}>
                        <button className={"Button-body"} aria-label="Knop maak nieuw onderzoek">Nieuw onderzoek
                        </button>
                    </Link>

                </div>
            </div>
        </>
    )
}