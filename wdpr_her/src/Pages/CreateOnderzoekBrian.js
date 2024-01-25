import { useState } from "react";
import { apiPath } from "../Helper/Api";
import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import Header from '../standaardformats/Header';
import axios from "axios";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";


export default function CreateOnderzoek() {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');
    const nav = useNavigate();

    const [vragen, setVragen] = useState([]);
    const [onderzoeksTypes, setOnderzoeksTypes] = useState([]);
    const [onderzoek, setOnderzoek] = useState({
        Beloning: "",
        Beschrijving: "",
        Locatie: "",
        Titel: "",
        OnderzoeksData: "", //Is de vragen
        OnderzoeksTypes: [],
    })


    useEffect(() => {
        getOnderzoeksTypes()
    }, []);

    useEffect(() => {
        if (vragen.length > 0) {
            setOnderzoek((prev) => ({
                ...prev,
                OnderzoeksData: JSON.stringify(vragen)
            }));
        }
     }, [vragen]);

    const getOnderzoeksTypes = () => {
        axios.get(apiPath + "Helper/GetOnderzoeksTypes")
            .then((response) => {
                setOnderzoeksTypes(JSON.parse(JSON.stringify(response.data)));
            })
    };

    const handleOnderzoeksTypes = (checked, typeId) => {
        let localOnderzoek = onderzoek;
        let types = localOnderzoek.OnderzoeksTypes ?? [];
        if (checked === false)
            types = types.filter(t => t !== typeId);
        else
            types = [...types, typeId];
        localOnderzoek.OnderzoeksTypes = types;
        setOnderzoek(localOnderzoek);
        console.log(onderzoek);
    };

    const addQuestion = () => {
        const newQuestion = {
            type: "text",
            text: "Een stukje text"
        }
        setVragen([...vragen, newQuestion]);
        console.log(vragen);
    };

    const removeQuestion = (i, e) => {
        e.preventDefault(); //So it doesn't reload the page
        const newQuestions = vragen.filter((_, index) => index !== i);
        setVragen(newQuestions);
    };

    const updateQuestion = (e, i) => {
        let localVragen = [...vragen];
        const type = localVragen[i].type;
        if (type === "text")
            localVragen[i] = { ...localVragen[i], text: e }
        else 
            localVragen[i] = { ...localVragen[i], vraag: e }
        setVragen(localVragen);
    };

    const changeQuestionType = (e, i) => {
        let localVragen = [...vragen];
        const newQuestion =
            e === "text" ? {
                type: e,
                text: "Een stukje tekst"
            } : e === "open" ? {
                type: e,
                vraag: "Een open vraag"
            } : {
                type: e,
                vraag: "Een " + e + " vraag",
                opties: [
                    "Optie 1",
                    "Optie 2"
                ]
            };
        localVragen[i] = newQuestion;
        setVragen(localVragen);
    };

    const addOption = (i, e) => {
        e.preventDefault();
        let localVragen = [...vragen];
        localVragen[i].opties = [...localVragen[i].opties, ""]
        setVragen([...localVragen])
    }

    const removeOption = (i, j, e) => {
        e.preventDefault();
        setVragen((previousVragen) => {
            const localVragen = [...vragen];
            const localVraag = { ...localVragen[i] };
            localVraag.opties = localVraag.opties.filter((_, index) => index !== j);
            localVragen[i] = localVraag;
            return localVragen;
        });
    }

    const changeOption = (i, j, e) => {
        setVragen((previousVragen) => {
            const localVragen = [...vragen];
            const localVraag = { ...localVragen[i] };
            localVraag.opties[j] = e;
            localVragen[i] = localVraag;
            return localVragen;
        })
    }

    const submit = () => {
        setOnderzoek((previousOnderzoek) => ({
            ...previousOnderzoek,
            OnderzoeksData: JSON.stringify(vragen)
        }));
        axios
            .post(apiPath + "Onderzoek/CreateOnderzoek", onderzoek)
            .then(() => nav("/Succes"))
            .catch((e) => console.log(e));
    }

    return (
        <div className="Main" data-theme={theme} data-font-size={fontSize}>
            <Header Title={"Nieuw onderzoek"} />
            <div className={"Body"}>
                <div className={"Section-border"}>  
                    <h3>Titel</h3>
                    <input aria-label={"Invoerveld voor de titel van het onderzoek"} type={"text"} onChange={(e) => onderzoek.Titel = e.target.value}/>
                    <h3>Onderzoekstype</h3>
                    {onderzoeksTypes.length > 0 ? (<ul> {onderzoeksTypes.map((type, i) => {
                        return (
                            <li key={"OT" + i} className={"Ervaringsdeskundige-Antwoorden"}><label><input type={"checkbox"} defaultChecked={false} onChange={(e) => handleOnderzoeksTypes(e.target.checked, type.id)} />{type.type}</label></li>
                        )
                    })}</ul>) : (<p>Waiting for on the data</p>)}
                    <h3>Beloning:</h3>
                    <textarea aria-label={"Invoerveld voor de beloning"} onChange={(e) => onderzoek.Beloning = e.target.value}></textarea>
                    <h3>Locatie</h3>
                    <input aria-label={"Invoerveld voor de locatie van het onderzoek"} type={"text"} onChange={(e) => onderzoek.Locatie = e.target.value} />
                    <h3>Beschrijving:</h3>
                    <input aria-label={"Invoerveld voor de beschrijving van het onderzoek"} type={"text"} onChange={(e) => onderzoek.Beschrijving = e.target.value} />
                    
                    <h3>Vragen</h3>
                    {vragen === null ? <p>Nog geen vragen</p> : vragen.map((vraag, i) => {
                        return (
                            <form key={i}>
                                <label>
                                <input
                                    type="radio"
                                    name={"type" + i}
                                    value="open"
                                    checked={vraag.type === "open"}
                                    onChange={() => changeQuestionType("open", i)}
                                />
                                Open
                                </label>

                                <label>
                                <input
                                    type="radio"
                                    name={"type" + i}
                                    value="meerkeuze"
                                    checked={vraag.type === "meerkeuze"}
                                    onChange={() => changeQuestionType("meerkeuze", i)}
                                />
                                Meerkeuze
                                </label>

                                <label>
                                <input
                                    type="radio"
                                    name={"type" + i}
                                    value="radio"
                                    checked={vraag.type === "radio"}
                                    onChange={() => changeQuestionType("radio", i)}
                                />
                                Radio
                                </label>

                                <label>
                                <input
                                    type="radio"
                                    name={"type" + i}
                                    value="text"
                                    checked={vraag.type === "text"}
                                    onChange={() => changeQuestionType("text", i)}
                                />
                                Text
                                </label>
                                <br/>    

                                <div className="Question">
                                <button className="Small-remove-button"aria-label="Verwijder deze vraag/tekstveld" onClick={(e) => removeQuestion(i, e)}>-</button>
                                <div className="Question-info">
                            {
                            vraag.type === "open" ?
                                <>
                                    <label>Vraag</label> <br/>
                                    <textarea aria-label="Openvraag invoerveld" value={vraag.vraag} onChange={(e) => updateQuestion(e.target.value, i)}/>
                                </> :
                            vraag.type === "meerkeuze" ?
                            <>
                                <label>Meerkeuze vraag met meerdere antwoorden</label> <br/>
                                <textarea aria-label="Meerkeuze vraag invoerveld" value={vraag.vraag} onChange={(e) => updateQuestion(e.target.value, i)}/>
                                <br/>
                                <label>Opties</label>
                                {vraag.opties.map((optie, j) => {
                                    return (
                                        <>
                                        <br/>
                                            <div className="Question" key={i + "," + j}>
                                                <button className="Small-remove-button" aria-label="Verwijder deze optie" onClick={(e) => removeOption(i, j, e)}>-</button>
                                                <input  type="text"  value={optie} onChange={(e) => changeOption(i, j, e.target.value)}/>
                                        </div>
                                        </>
                                    )
                                })}
                                <button aria-label="Knop voeg optie toe" onClick={(e) => addOption(i, e)}>Nieuwe optie</button>
                            </> :
                            vraag.type === "radio" ?
                            <>
                                <label>Meerkeuze vraag met één antwoord</label> <br/>
                                <textarea aria-label="Radio vraag invoerveld" value={vraag.vraag} onChange={(e) => updateQuestion(e.target.value, i)}/>
                                <br/>
                                <label>Opties</label>
                                {vraag.opties.map((optie, j) => {
                                    return (
                                        <>
                                        <br/>
                                            <div className="Question" key={i + "," + j}>
                                                <button className="Small-remove-button" aria-label="Verwijder deze optie" onClick={(e) => removeOption(i, j, e)}>-</button>
                                                <input  type="text"  value={optie} onChange={(e) => changeOption(i, j, e.target.value)}/>
                                        </div>
                                        </>
                                    )
                                })}
                                <button aria-label="Knop voeg optie toe" onClick={(e) => addOption(i, e)}>Nieuwe optie</button>
                            </> :
                            vraag.type === "text" ?
                            <>
                                <label>Tekst</label> <br/>
                                <textarea aria-label="Tekst invoerveld" value={vraag.text} onChange={(e) => updateQuestion(e.target.value, i)}/>
                            </> :
                            "UNKOWN"
                            }
                            </div>
                            </div>
                            </form>
                       ) 
                    })}
                    <button aria-label={"Knop: voeg vraag toe"} onClick={addQuestion}>nieuwe vraag</button>
                    <button aria-label={"Submit"} onClick={submit}>submit</button>
                </div>
            </div>
        </div>
    );
}