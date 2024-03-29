import logo from '../Logo Icon/icon_accessibility.png';
import logo_blauw from '../Logo Icon/Op blauw/icon_accessibility_on-blue.png';
import logo_blauw_trans from '../Logo Icon/Op blauw/Transparant/icon_accessibility_on-blue_transp.png';
import logo_donker from '../Logo Icon/Op donker/icon_accessibility_on-dark.jpg';
import logo_donker_trans from '../Logo Icon/Op donker/Transparant/icon_accessibility_on-dark_transp.png';
// import './App.css';
import '../CSS/Theme.css';
import useLocalStorage from 'use-local-storage';
// import {useAuth0} from "@auth0/auth0-react";
import {Link} from "react-router-dom";

import DynamicOpdrachtPaneel from '../standaardformats/DynamicOpdrachtPaneel'


function Scam() {
    // Check browser default theme preference
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    const [theme, setTheme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    let curThemeLogo = theme === 'light' ? logo : theme === 'blue' ? logo_blauw_trans : logo_donker_trans;

    const switchTheme = () => {
        const newTheme = theme === 'light' ? 'blue' : theme === 'blue' ? 'dark' : 'light';
        setTheme(newTheme);
        curThemeLogo = theme === 'light' ? logo : theme === 'blue' ? logo_blauw_trans : logo_donker_trans;
    }

    const opdrachtArray = [
        {naam: "If it hadn't been for", aantaldeelnemers: 69},
        {naam: 'Cotton-Eyed Joe', status: "Completed"},
        {status: "Completed", aantaldeelnemers: 420},
        {naam: "I'd been married long time ago", aantaldeelnemers: 314, status: "Beschikbaar"}
        // Add more opdracht objects as needed
    ];

    // const LoginButton = () => {
    //   const { loginWithRedirect } = useAuth0();
    //
    //   return <button onClick={() => loginWithRedirect()}>Log In</button>;
    // };

    return (
        <div className="App" data-theme={theme}>
            <header className="App-header">
                Welkom bij Stichting Accessibility

                <Link to={"/"}><h1>GRATIS iPHONE 20</h1>
                    <button>CLICK</button>
                </Link>
                <h1><font color="#ff0000">DIT IS TOTAAL GEEN SCAM</font></h1>
                <div className="Logo-row">
                    <p className="Logo-text"><img src={logo} className="App-logo" alt="logo"/> default logo</p>
                    <p className="Logo-text"><img src={logo_blauw} className="App-logo" alt="logo"/> logo on blue</p>
                    <p className="Logo-text"><img src={logo_blauw_trans} className="App-logo" alt="logo"/> logo on blue
                        transparent</p>
                    <p className="Logo-text"><img src={logo_donker} className="App-logo" alt="logo"/> logo on dark </p>
                    <p className="Logo-text"><img src={logo_donker_trans} className="App-logo" alt="logo"/> logo on dark
                        transparent</p>
                </div>
                <div className={"Logo-row"}>
                    <img src={curThemeLogo} className={"App-logo"} alt={"logo stichting accessibility"}/>
                    <button onClick={switchTheme} aria-label="Verander kleurschema">
                        switch to {theme === 'light' ? 'blue' : theme === 'blue' ? 'dark' : 'light'} theme
                    </button>
                </div>

                <p className={"text-sec"}>Secondary text</p>
                <p className={"text-accent"}>Text accent</p>
                <DynamicOpdrachtPaneel opdrachtArray={opdrachtArray}/>
            </header>
        </div>
    );
}

export default Scam;
