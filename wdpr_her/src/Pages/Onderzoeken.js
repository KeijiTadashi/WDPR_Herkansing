import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import Header from '../standaardformats/Header';
import "../CSS/Onderzoeken.css";
import DynamicOnderzoekPaneel from '../standaardformats/DynamicOnderzoekPaneel';


function Onderzoeken() {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    /*
    *Laad de volgende array in met de vragen uit de database.
    *Er wordt automatisch een nummer aan gegeven, en de vragen worden automatisch ingeladen.
    */
    //TODO laad hier dus die arrays in
    const onderzoekArray = [
        {vraag: "Hoe gaat het met je?", type: "Radio"},
        {vraag: "Wie is Prins Bernhard, en zo niet, waarom?", type: "Open"}
    ]

    return (
        <>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header/>
                <div className={"Body"}>
                    <div className="Onderzoek">
                        <h2>Doel van het onderzoek</h2>
                        <p>Het doel van dit onderzoek is om te kijken wat jouw gebruikers ervaring is bij het gebruik
                            van de website.
                            etc.
                            and some more info.
                        </p>
                    </div>

                    <DynamicOnderzoekPaneel onderzoekArray={onderzoekArray}/>

                </div>
            </div>
        </>
    )
}


export default Onderzoeken;