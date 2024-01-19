import React from "react";
import useLocalStorage from 'use-local-storage';
import Header from "../standaardformats/Header";
import "../CSS/StichtingTheme.css";
import {Link} from "react-router-dom";
import "../CSS/Beheerder.css"


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
                            <p>Wij zijn stichting accessability en wij....

                                Hello darkness, my old friend
                                I've come to talk with you again
                                Because a vision softly creeping
                                Left its seeds while I was sleeping
                                And the vision that was planted in my brain
                                Still remains
                                Within the sound of silence
                                In restless dreams I walked alone
                                Narrow streets of cobblestone
                                'Neath the halo of a street lamp
                                I turned my collar to the cold and damp
                                When my eyes were stabbed by the flash of a neon light
                                That split the night
                                And touched the sound of silence</p>
                        </div>

                        <div className="textbox">

                            <div className="border">

                                <h2>Wil je helpen? </h2>
                                <p>Heb jij een beperking en wil je graag helpen bij onafhankelijke onderzoeken, of
                                    onderzoeken voor bedrijven.
                                    Voor het verbeteren van de ....
                                    On Cracker Island, it was born To the collective of the dawn They were planting
                                    seeds at night To grow a made-up paradise Where the truth was auto-tuned</p>
                                <Link to={"/"}>
                                    <button aria-label="registreer">Registreer</button>
                                </Link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Beheerder;