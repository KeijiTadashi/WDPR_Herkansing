import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import Header from '../standaardformats/Header';
import "../CSS/Onderzoeken.css"

function Onderzoeken() {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    return (
        <>
            <Header/>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <div className={"Body"}>
                    <div className="Onderzoek">
                        <h3>Doel van het onderzoek</h3>
                        <p>Het doel van dit onderzoek is om te kijken wat jouw gebruikers ervaring is bij het gebruik van de website.
                            etc.
                            and some more info.
                        </p>
                    </div>

                    <div className="Onderzoek">
                        <h3>Vragen over onderdeel A</h3>
                        <p>vraag</p>
                        <Beoordeling/>
                    </div>

                    <div className="Onderzoek">
                        <h3>Vragen over onderdeel B</h3>
                        <p>vraag</p>
                        <Beoordeling/>
                    </div>
            </div>
        </div>
        </>
    )
}

function Beoordeling(){
    return(
        <div className="Beoordeling">
            <label for="Beoordeling Zeer goed">
                <input type="checkbox" value="1" name="Beoordeling Zeer goed" aria-label="Beoordeling Zeer goed"/>
                Zeer goed
            </label>
            <label for="Beoordeling Goed">
                <input type="checkbox" value="2" name="Beoordeling Goed" aria-label="Beoordeling Goed"/>
                Goed
            </label>
            <label for="Beoordeling Neutraal">
                <input type="checkbox" value="3" name="Beoordeling Neutraal" aria-label="Beoordeling Neutraal"/>
                Neutraal
            </label>
            <label for="Beoordeling Slecht">
                <input type="checkbox" value="4" name="Beoordeling Slecht" aria-label="Beoordeling Slecht"/>
                Slecht
            </label>
            <label for="Beoordeling Zeer slecht">
                <input type="checkbox" value="5" name="Beoordeling Zeer slecht" aria-label="Beoordeling Zeer slecht"/>
                Zeer slecht
            </label>
        </div>
    );
}

export default Onderzoeken;