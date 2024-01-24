import React from "react";
import useLocalStorage from 'use-local-storage';
import Header from "../standaardformats/Header";
import "../CSS/StichtingTheme.css";
import {Link} from "react-router-dom";
import "../CSS/Beheerder.css"
import Bedrijven from "./Bedrijven";
import Ervaringsdeskundige from "./Ervaringsdeskundige";


export const Beheerder = () => {

    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    return (
        <>

            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header Title={"Beheerdersportaal"}/>
                <div className={"Body"}>

                    <div className="flex-container">
                        <div className="textbox border">
                            <h2>Wie zijn wij? </h2>
                            <p>Bij Stichting Accessibility koesteren we een diepgewortelde overtuiging 
                                dat toegankelijkheid een essentieel recht is voor iedereen, ongeacht achtergrond, 
                                capaciteit of beperking. Onze missie is glashelder: streven naar een inclusieve 
                                samenleving waarin alle mensen, met of zonder beperking, gelijkwaardig kunnen participeren.
                            </p>
                            <p>
                                Met een toegewijd team van experts zetten we ons dagelijks in om digitale, fysieke en sociale 
                                barrières af te breken. Onze focus ligt op het creëren van een wereld waarin iedereen toegang 
                                heeft tot dezelfde kansen en mogelijkheden. Het is onze overtuiging dat technologische vooruitgang 
                                en innovatie de sleutel zijn tot het verwezenlijken van deze visie.
                            </p>
                        </div>

                        <div className="textbox">

                            <div className="border">

                                <h2>Wil je helpen? </h2>
                                <p>Heb jij een beperking en wil je graag helpen bij onafhankelijke onderzoeken, of
                                    onderzoeken voor bedrijven?
                                    Met uw feedback helpt u mee de toegankelijkheid van de websites van grote bedrijven 
                                    te verbeteren.</p>
                                <Link to={"/"}>
                                    <button aria-label="registreer">Registreer</button>
                                </Link>
                                <Bedrijven/>
                                <Ervaringsdeskundige/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Beheerder;