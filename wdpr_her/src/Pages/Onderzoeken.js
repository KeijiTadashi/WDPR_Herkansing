import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import Header from '../standaardformats/Header';
import "../Onderzoeken.css"
import Beoordeling from '../standaardformats/Beoordeling'

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


export default Onderzoeken;