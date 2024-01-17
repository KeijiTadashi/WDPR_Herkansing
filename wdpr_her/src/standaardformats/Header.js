import logo from '../Logo Icon/Op blauw/Transparant/icon_accessibility_on-blue_transp.png';
import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import {Link, useLocation} from "react-router-dom";
import {GetAuthRole} from "../Helper/AuthToken";
import {useEffect, useState} from "react";

const Header = ({Title}) => {
    // Abstact this out
    // Check browser default theme preference
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    const [theme, setTheme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    //let curThemeLogo = theme === 'light' ? logo : logo_donker_trans;
    // let curThemeLogo = logo;

    const [fontSize, setFontSize] = useLocalStorage('font-size', 'normal');
    
    const [role, setRole] = useState(GetAuthRole);

    let location = useLocation();
    console.log(location.pathname);
    
    useEffect(() => {
        setRole(GetAuthRole);
    }, []);
    
    const switchTheme = () => {
        const newTheme = theme === 'light' ? 'dark' : 'light';
        setTheme(newTheme);
        // curThemeLogo = theme === 'light' ? logo : logo_donker_trans;
    }

    const increaseFont = () => {
        let newFontSize = fontSize;
        switch (fontSize) {
            case 'normal':
                newFontSize = 'big';
                break;
            case 'big':
                newFontSize = 'bigger';
                break;
            case 'bigger':
                newFontSize = 'biggest';
                break;
            default:
                break;
        }
        setFontSize(newFontSize);
    }

    const decreaseFont = () => {
        let newFontSize = fontSize;
        switch (fontSize) {
            case 'big':
                newFontSize = 'normal';
                break;
            case 'bigger':
                newFontSize = 'big';
                break;
            case 'biggest':
                newFontSize = 'bigger';
                break;
            default:
                break;
        }
        setFontSize(newFontSize);
    }
    
    const headerMavClassName = (path) => {
        return location.pathname === path ? "Button-navigation Button-navigation-current" : "Button-navigation"
    }

    return (
        <header>
            <div className={"Header"}>
                <div className={"Header-top"}>
                    <img src={logo} className={"Logo-header"}
                        alt={"logo stichting accessibility. Klik om naar de homepage te gaan"} />
                    <h1 className={"Title"}>{Title ?? "NO TITLE"}</h1>
                    <div className={"Info-header"}>
                        <ul aria-label="Toegankelijkheid menu">
                            <li><button className={"Button-header"} onClick={switchTheme} aria-label="Verander kleurthema">theme</button></li>
                            <li><button className={"Button-header-small"} onClick={decreaseFont} aria-label="Verklein tekst">-</button></li>
                            <li><h3 className={"header-text"}>Font size</h3></li>
                            <li><button className={"Button-header-small"} onClick={increaseFont} aria-label="Vergroot tekst">+</button></li>

                        </ul>
                    </div>
                </div>
                <div className={"Navigation"}>
                    <div className={"Navigation-spacer"} />
                    <ul aria-label="menubalk">
                        <li>
                            <Link to={"/"}><button className={headerMavClassName("/")}  aria-label="Home">Home</button></Link>
                        </li>
                        {(role !== false) ?
                            <li>
                                <button className={headerMavClassName("/Profiel")} aria-label="Mijn profiel">Mijn profiel</button>
                            </li> : 
                            <>
                                <li>
                                    <Link to={"/Login"}><button className={headerMavClassName("/Login")} aria-label="Login pagina">Login</button></Link>
                                </li>
                                <li>
                                    <Link to={"/Registreer"}><button className={headerMavClassName("/Registreer")} aria-label="Registeer als nieuwe ervaringsdeskundige of nieuw bedrijf">Registreer</button></Link>
                                </li> 
                            </>
                        }
                        {(role === "Beheerder") ?
                            <li>
                                <Link to={"/Beheerder"}><button className={headerMavClassName("/Beheerder")} aria-label="Beheerder portaal">beheerder</button></Link>
                            </li> : 
                            (role === "Ervaringsdeskundige") ?
                                <li>
                                    <Link to={"/Ervaringdeskundige"}><button className={headerMavClassName("/Ervaringsdeskundige")} aria-label="Ervaringdeskundige portaal">Ervaringdeskundige</button></Link>
                                </li> :
                            (role === "Bedrijf") ?
                                <li>
                                    <Link to={"/Bedrijven"}><button className={headerMavClassName("/Bedrijven")} aria-label="Bedrijven portaal">Bedrijven</button></Link>
                                </li> : ""
                        }
                        
                        
                        
                        {/*<li>*/}
                        {/*    <Link to={"/Onderzoeken"}><button className={"Button-navigation"} aria-label="Onderzoeken">Onderzoeken</button></Link>*/}
                        {/*</li>*/}
                        {/*<li>*/}
                        {/*    <Link to={"/Test"}><button className={"Button-navigation"} aria-label="API test pagina">Api Test Page</button></Link>*/}
                        {/*</li>*/}
                        
                        {/*<li>*/}
                        {/*    <Link to={"/Scam"}><button className={"Button-navigation"} aria-label="Testing Playground Scam pagina">Scam</button></Link>*/}
                        {/*</li>*/}
                        
                        
                    </ul>
                </div>
            </div>
        </header>
    );
}

export default Header;
