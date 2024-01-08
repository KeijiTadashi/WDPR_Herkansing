import useLocalStorage from "use-local-storage";
import Header from "./Header";

export const Layout = (props) => {
    const theme = useLocalStorage('theme');
    //let curThemeLogo = theme === 'light' ? logo : logo_donker_trans;
    // let curThemeLogo = logo;

    const fontSize = useLocalStorage('font-size');

    return (
        <div className="Main" data-theme={theme} data-font-size={fontSize}>
            <Header/>
            {props}
        </div>
    )
}