import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import Header from '../standaardformats/Header';
import "../CSS/Onderzoeken.css";
import {Link, useNavigate} from 'react-router-dom';
import {apiPath} from "../Helper/Api";
import axios, {get} from "axios";
import {useEffect, useState} from "react";
import {map} from "react-bootstrap/ElementChildren";

function ErvaringsdeskundigeOnderzoek(onderzoekId) {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');
    const nav = useNavigate();
    
    const [onderzoek, setOnderzoek] = useState([]);
    const [onderzoeksData, setOnderzoeksData] = useState([]);
    const [antwoorden, setAntwoorden] = useState([]);
    const [titel, setTitel] = useState("Onderzoek");

    // useEffect(() => {
    //     setOnderzoek()
    // }, [])
    
    useEffect(() => {
        getVragen(onderzoekId);
    }, [])

    const submitOpdracht = () => {
        // const info = {
        //         Gebruikersnaam: usernameRef.current.value,
        //         Wachtwoord: passwordRef.current.value,
        //         Email: emailRef.current.value,
        //         Voornaam: voornaamRef.current.value,
        //         Achternaam: achternaamRef.current.value,
        //         Telefoonnummer: telefoonnummerRef.current.value,
        //         Postcode: postcodeRef.current.value
        //     };
        //
        //
        // axios
        //     .post(apiPath, info)
        //     .then(() => {
        //         nav("/Succes");
        //     }).catch((err) => {
        //     console.log(err.toJSON());
        // });
    }
    
    const getVragen = (id) => {
        //TODO REMOVE For now testing id = 1
        id = 1;
        axios.get(apiPath + "Onderzoek/GetOnderzoek/"+ id).then(response => {
            setOnderzoek(JSON.parse(JSON.stringify(response.data)));
            // console.log("Onderzoek:");
            // console.log(response.data);
            // console.log("Data:")
            // console.log(response.data.onderzoeksData);
            
            // console.log("Data parse:")
            // console.log(JSON.parse(response.data.onderzoeksData));
            // setOnderzoeksData(JSON.parse(response.data.onderzoeksData));
            
            // console.log("Data stringify:")
            // console.log(JSON.stringify(response.data.onderzoeksData));
            // console.log("Data parse stringify:")
            // console.log(JSON.parse(JSON.stringify(response.data.onderzoeksData)));
            setOnderzoeksData([
                {
                    type: "open",
                    vraag: "Waar kan ik open vragen beantwoorden?"
                }, {
                    type: "open",
                    vraag: "Wat is jouw favoriete smaak bowlingbal? Die van mij is paars."
                }, {
                    type: "meerkeuze", 
                    vraag: "Kies 1 of meerdere antwoorden",
                    opties: [
                        "David Robert Jones",
                        "The rhythm of the crowd",
                        "Ground control to major Tom",
                        "And the stars look very different today"
                    ]
                }, {
                    type: "text",
                    text: "You remind me of the babe (what babe?)\nBabe with the power (what power?)\nPower of voodoo (who do?)\nYou do (do what?)\nRemind me of the babe\nI saw my baby\nCrying hard as babe could cry\nWhat could I do?\nMy baby's love had gone\nAnd left my baby blue\nNobody knew\nWhat kind of magic spell to use\n(Slime and snails) or puppy dog tails\n(Thunder or lightning) then baby said"
                }, {
                    type: "radio",
                    vraag: "From what movie, staring David Bowie, are the above lyrics",
                    opties: [
                        "The Spiders From Mars",
                        "Labyrinth",
                        "Tommy",
                        "The Pick Of Destiny",
                        "Who is David Bowie? And what is a movie? Is anything even real anymore?"
                    ]
                }
            ])
            // setTitel(onderzoek.titel);
        })
    }

    const handle = (e, type, indexVraag, indexOptie) => {
        console.log("Start handle:");
        console.log(antwoorden);
        console.log(e);
        console.log(indexVraag)
        let localAntwoorden = [antwoorden];
        switch (type) {
            case "open" : antwoorden[indexVraag] = e; break;
            case "meerkeuze" :
                let localOpties = antwoorden[indexVraag] ?? [];
                if (e === false)
                    localOpties = localOpties.filter(optie => optie !== indexOptie);
                else
                    localOpties = [...localOpties, indexOptie];
                antwoorden[indexVraag] = localOpties;
                break;
            default : console.log("This question type is unknown... nothing will happen with this change.")
        }
        
        setAntwoorden(...localAntwoorden)
        console.log("End handle:");
        console.log(antwoorden);
    }

    return (
        <>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header Title={onderzoek == null ? "Onderzoek" : onderzoek.titel}/>
                <div className={"Body"}>
                    <div className={"Section-border"}>
                        <h3>Beloning: </h3>
                        <p>{onderzoek.beloning ?? "Dit onderzoek heeft geen beloning"}</p>
                        <h3>Beschrijving: </h3>
                        <p>{onderzoek.beschrijving}</p>                        
                    {onderzoek == null ? "Waiting for data" : onderzoeksData.map((vraag, i) => { return (
                            vraag.type === "open" ?
                                <div key={i}>
                                    <p>{vraag.vraag}</p> 
                                    <textarea aria-label={"Invoerveld voor de vraag: " + vraag.vraag} onChange={(e) => handle(e.target.value, vraag.type, i)}></textarea>
                                </div> :
                            vraag.type === "meerkeuze" ?
                                <div key={i}>
                                    <p>{vraag.vraag}</p>
                                    <ul>
                                    {vraag.opties.map((optie, j) => {
                                        return <li key={i + "," + j}><label><input type={"checkbox"} defaultChecked={false} onChange={(e) => {handle(e.target.checked, vraag.type, i, j)}}/>{optie}</label></li>
                                    })}
                                    </ul>
                                </div> :
                            vraag.type === "radio" ?
                                <p>{vraag.vraag}</p> :
                            vraag.type === "text" ?
                                <p>{vraag.text}</p> :
                            "UNKNOWN TYPE"
                        )
                    })}
                    </div>
                    {/*<div className="Onderzoek">*/}
                    {/*    <h2>Doel van het onderzoek</h2>*/}
                    {/*    <p>Het doel van dit onderzoek is om te kijken wat jouw gebruikers ervaring is bij het gebruik*/}
                    {/*        van de website.*/}
                    {/*        etc.*/}
                    {/*        and some more info.*/}
                    {/*    </p>*/}
                    {/*    <Link to={"/Onderzoeken"}>*/}
                    {/*        <button className="neem-deel-button">Neem deel</button>*/}
                    {/*    </Link>*/}
                    {/*</div>*/}
                </div>
            </div>
        </>
    )
}

export default ErvaringsdeskundigeOnderzoek;