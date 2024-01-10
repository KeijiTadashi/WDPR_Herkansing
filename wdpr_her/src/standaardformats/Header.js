import logo from '../Logo Icon/Op blauw/Transparant/icon_accessibility_on-blue_transp.png';
import '../StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import {Link} from "react-router-dom";

const Header = () => {
    // Abstact this out
    // Check browser default theme preference
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    const [theme, setTheme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    //let curThemeLogo = theme === 'light' ? logo : logo_donker_trans;
    // let curThemeLogo = logo;

    const [fontSize, setFontSize] = useLocalStorage('font-size', 'normal');

    const switchTheme = () => {
        const newTheme = theme === 'light' ? 'dark' : 'light';
        setTheme(newTheme);
        // curThemeLogo = theme === 'light' ? logo : logo_donker_trans;
    }

    const increaseFont = () => {
        let newFontSize = fontSize;
        switch (fontSize) {
            case 'normal' :
                newFontSize = 'big';
                break;
            case 'big' :
                newFontSize = 'bigger';
                break;
            case 'bigger' :
                newFontSize = 'biggest';
                break;
            default :
                break;
        }
        setFontSize(newFontSize);
    }

    const decreaseFont = () => {
        let newFontSize = fontSize;
        switch (fontSize) {
            case 'big' :
                newFontSize = 'normal';
                break;
            case 'bigger' :
                newFontSize = 'big';
                break;
            case 'biggest' :
                newFontSize = 'bigger';
                break;
            default :
                break;
        }
        setFontSize(newFontSize);
    }

    return (
        <header>
            {/*<div className={"Header"}>*/}
            <div className={"Header-top"}>
                <img src={logo} className={"Logo-header"}
                     alt={"logo stichting accessibility. Klik om naar de homepage te gaan"}/>
                <h1>Title</h1>
                <div className={"Info-header"}>
                    <ul aria-label="Toegankelijkheid menu">
                        <li><button className={"Button-header-small"} onClick={increaseFont} aria-label="Vergroot tekst">+</button></li>
                        <li><h3>Font size</h3></li>
                        <li><button className={"Button-header-small"} onClick={decreaseFont} aria-label="Verklein tekst">-</button></li>
                        <li><button className={"Button-header"} onClick={switchTheme} aria-label="Verander kleurthema">theme</button></li>
                    </ul>
                </div>
            </div>
            <div className={"Navigation"}>
                <div className={"Navigation-spacer"}/>
                <ul aria-label="menubalk">
                    <li><Link to={"/"}>
                        <button className={"Button-navigation"} aria-label="Home">Home</button>
                    </Link></li>
                    <li>
                        <button className={"Button-navigation"} aria-label="Mijn profiel">Mijn profiel</button>
                    </li>
                    <li><Link to={"/Scam"}>
                        <button className={"Button-navigation"} aria-label="Logo test pagina">Gratis iPhone 20</button>
                    </Link></li>
                    <li><Link to={"/Test"}>
                        <button className={"Button-navigation"} aria-label="API test pagina">Api Test Page</button>
                    </Link></li>
                    <li><Link to={"/Login"}>
                        <button className={"Button-navigation"} aria-label="Login pagina">Login</button>
                    </Link></li>
                </ul>
            </div>
            {/*</div>*/}
        </header>
    );
}

export default Header;
