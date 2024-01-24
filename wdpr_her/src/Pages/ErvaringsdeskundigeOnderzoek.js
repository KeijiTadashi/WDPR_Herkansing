import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import Header from '../standaardformats/Header';
import "../CSS/Onderzoeken.css";
import {useNavigate, useLocation} from 'react-router-dom';
import {apiPath} from "../Helper/Api";
import axios from "axios";
import {useEffect, useState} from "react";
// import {map} from "react-bootstrap/ElementChildren";

function ErvaringsdeskundigeOnderzoek() {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');
    const nav = useNavigate();
    
    const [onderzoek, setOnderzoek] = useState([]);
    const [onderzoeksData, setOnderzoeksData] = useState([]);
    const [antwoorden, setAntwoorden] = useState([]);
    const [titel, setTitel] = useState("Onderzoek");


    let location = useLocation();

    let pathList = location.pathname.split('/');
    console.log(pathList[pathList.length - 1]);
    const id = pathList[pathList.length - 1];
    
    // useEffect(() => {
    //     setOnderzoek()
    // }, [])
    
    useEffect(() => {
        getVragen();
    }, [])

    const submitOpdracht = () => {
        
        const info = {
            OnderzoekId: onderzoek.UitvoorderId,
            VraagMetAntwoordenJSON: JSON.stringify(antwoorden)
        };
        axios
            .post(apiPath + "OpdrachtRespons/CreateOpdrachtRespons", info)
            .then(() => nav("/Succes"))
            .catch((err) => console.log(err.toJSON()));
    }
    
    const getVragen = async () => {
        //TODO REMOVE For now testing id = 1
        // id = 1;
        await axios.get(apiPath + "Onderzoek/GetOnderzoek/"+ id).then(response => {
            setOnderzoek(JSON.parse(JSON.stringify(response.data)));
            
            console.log("Data parse:")
            console.log(JSON.parse(response.data.onderzoeksData));
            setOnderzoeksData(JSON.parse(response.data.onderzoeksData));
            
            
            //#region Test onderzoeksdata
            // setOnderzoeksData([
            //     {
            //         type: "open",
            //         vraag: "Waar kan ik open vragen beantwoorden?"
            //     }, {
            //         type: "open",
            //         vraag: "Wat is jouw favoriete smaak bowlingbal? Die van mij is paars."
            //     }, {
            //         type: "meerkeuze", 
            //         vraag: "Kies 1 of meerdere antwoorden",
            //         opties: [
            //             "David Robert Jones",
            //             "The rhythm of the crowd",
            //             "Ground control to major Tom",
            //             "And the stars look very different today"
            //         ]
            //     }, {
            //         type: "text",
            //         text: "You remind me of the babe (what babe?)\nBabe with the power (what power?)\nPower of voodoo (who do?)\nYou do (do what?)\nRemind me of the babe\nI saw my baby\nCrying hard as babe could cry\nWhat could I do?\nMy baby's love had gone\nAnd left my baby blue\nNobody knew\nWhat kind of magic spell to use\n(Slime and snails) or puppy dog tails\n(Thunder or lightning) then baby said"
            //     }, {
            //         type: "radio",
            //         vraag: "From what movie, staring David Bowie, are the above lyrics",
            //         opties: [
            //             "The Spiders From Mars",
            //             "Labyrinth",
            //             "Tommy",
            //             "The Pick Of Destiny",
            //             "Who is David Bowie? And what is a movie? Is anything even real anymore?"
            //         ]
            //     }
            // ])
            //#endregion
            
            // setTitel(onderzoek.titel);
        })
    }

    const handle = (e, type, indexVraag, indexOptie) => {
        console.log("Start handle:");
        console.log(antwoorden);
        console.log(e);
        console.log(indexVraag)
        let localAntwoorden = [antwoorden];
        let localOpties;
        switch (type) {
            case "open" : antwoorden[indexVraag] = e; break;
            case "meerkeuze" :
                localOpties = antwoorden[indexVraag] ?? [];
                if (e === false)
                    localOpties = localOpties.filter(optie => optie !== indexOptie);
                else
                    localOpties = [...localOpties, indexOptie];
                antwoorden[indexVraag] = localOpties;
                break;
            case "radio":
                antwoorden[indexVraag] = indexOptie;
                break;
            default : console.log("This question type is unknown... nothing will happen with this change.")
        }
        
        setAntwoorden(...localAntwoorden)
        console.log("End handle:");
        console.log(antwoorden);
        console.log(antwoorden.toString())
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
                        <div className={"Ervaringsdeskundige-Vragen"}>                        
                        {onderzoeksData == null ? "Waiting for data" : onderzoeksData.map((vraag, i) => {
                            return (
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
                                        return <li key={i + "," + j} className={"Ervaringsdeskundige-Antwoorden"}><label><input type={"checkbox"} defaultChecked={false} onChange={(e) => {handle(e.target.checked, vraag.type, i, j)}}/>{optie}</label></li>
                                    })}
                                    </ul>
                                </div> :
                            vraag.type === "radio" ?
                                <div key={i}>
                                        <p>{vraag.vraag}</p>
                                        <ul>
                                            {vraag.opties.map((optie, j) => {
                                                return <li key={i + "," + j} className={"Ervaringsdeskundige-Antwoorden"}><label><input type={"radio"} defaultChecked={false} name={"opties" + i} onChange={(e) => { handle(e.target.checked, vraag.type, i, j) }} />{optie}</label></li>
                                            })}
                                        </ul>
                                </div> :
                            vraag.type === "text" ?
                                <p>{vraag.text}</p> :
                            "UNKNOWN TYPE"
                        )
                    })}
                    </div>
                        <button onClick={submitOpdracht}>Submit</button>
                        {/* <button onClick={() => console.log(JSON.stringify(onderzoeksData))}>Console Log string onderzoeksdata</button> */}
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