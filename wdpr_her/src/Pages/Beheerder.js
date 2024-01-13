import React from "react";
import useLocalStorage from 'use-local-storage';
import Header from "../standaardformats/Header";
import "../StichtingTheme.css";
import "../Beheerder.css"
import {Link} from "react-router-dom";
import "../Beheerder.css"


export const Beheerder = () => {

    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    return (
        <>
            <Header />
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
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
                                <p>Heb jij een beperking en wil je graag helpen bij onafhankelijke onderzoeken, of onderzoeken voor bedrijven.
                                    Voor het verbeteren van de ....
                                    On Cracker Island, it was born To the collective of the dawn They were planting seeds at nigh.</p>
                                <Link to={"/"}>
                                <button>Registreer</button>
                                </Link>
                            </div>
                            <div className="border">
                                <h2>Wil je helpen? </h2>
                                <p>Heb jij een beperking en wil je graag helpen bij onafankelijke onderzoeken, of onderzoeken voor bedrijven.
                                    Voor het verbeteren van de ....
                                    Oh freddled gruntbuggly,Thy micturations are to me As plurdled gabbleblotchits on a lurgid bee.Groop, I implore thee, my foonting turlingdromes,And hooptiously drangle me with crinkly bindlewurdles,Or I will rend thee in the gobberwarts With my blurglecruncheon, see if I don't!</p>
                            </div>
                        </div>
                    </div>


                </div>
            </div >
        </>
    );
};

export default Beheerder;