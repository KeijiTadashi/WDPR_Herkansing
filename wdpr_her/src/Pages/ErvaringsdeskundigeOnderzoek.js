import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import Header from '../standaardformats/Header';
import "../CSS/Onderzoeken.css";
import {Link, useLocation} from 'react-router-dom';

function ErvaringsdeskundigeOnderzoek() {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');
    
    let location = useLocation();
    
    let pathList = location.pathname.split('/');
    console.log(pathList[pathList.length - 1]);
    // const id = pathList[pathList.length - 1];
    
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
                        <Link to={"/Onderzoeken"}>
                            <button className="neem-deel-button">Neem deel</button>
                        </Link>
                    </div>
                </div>
            </div>
        </>
    )
}

export default ErvaringsdeskundigeOnderzoek;