import logo from '../Logo Icon/Op blauw/Transparant/icon_accessibility_on-blue_transp.png';
// import logo_donker_trans from '../Logo Icon/Op donker/Transparant/icon_accessibility_on-dark_transp.png';
import '../StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import {Link} from "react-router-dom";

function Font() {

    // Abstact this out
    // Check browser default theme preference
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    const [theme , setTheme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    //let curThemeLogo = theme === 'light' ? logo : logo_donker_trans;
    let curThemeLogo = logo;
    
    const [fontSize, setFontSize] = useLocalStorage('font-size', 'normal');

    const switchTheme = () => {
        const newTheme = theme === 'light' ? 'dark' : 'light';
        setTheme(newTheme);
        // curThemeLogo = theme === 'light' ? logo : logo_donker_trans;
    }
    
    const increaseFont = () => {
        let newFontSize = fontSize;
        switch (fontSize) {
            case 'normal' : newFontSize = 'big'; break;
            case 'big' : newFontSize = 'bigger'; break;
            case 'bigger' : newFontSize = 'biggest'; break;
            default : break;
        }
        setFontSize(newFontSize);
    }

    const decreaseFont = () => {
        let newFontSize = fontSize;
        switch (fontSize) {
            case 'big' : newFontSize = 'normal'; break;
            case 'bigger' : newFontSize = 'big'; break;
            case 'biggest' : newFontSize = 'bigger'; break;
            default : break;
        }
        setFontSize(newFontSize);
    }
    
    return (
        <div className="Main" data-theme={theme} data-font-size={fontSize}>
            {/*<div className="Header">*/}
            {/*    <img src={curThemeLogo} className={"Logo-header"} alt={"logo stichting accessibility"} />*/}
            {/*    <div className={"Header-column"}>*/}
            {/*        <div className={"Button-header-row"}>*/}
            {/*            <button className={"Button-header"} onClick={switchTheme}>Switch theme</button>*/}
            {/*            <button className={"Button-header"} onClick={increaseFont}>increase font</button>*/}
            {/*            <button className={"Button-header"} onClick={decreaseFont}>decrease font</button>*/}
            {/*        </div>*/}
            {/*        <div className={"Header-row"}>*/}
            {/*            <div className={"Menu-item"}>Home</div>*/}
            {/*            <div className={"Menu-item"}>Mijn profiel</div>*/}
            {/*            <div className={"Menu-item"}>Chat</div>*/}
            {/*        </div>*/}
            {/*    </div>*/}
            {/*</div>*/}

            <div className={"Header"}>
                <div className={"Header-top"}>
                    <img src={curThemeLogo} className={"Logo-header"} alt={"logo stichting accessibility. Klik om naar de homepage te gaan"} />
                    <h1>Title</h1>
                    <div className={"Info-header"}>
                        <button className={"Button-header-small"} onClick={increaseFont}>+</button>
                        <h3>Font size</h3>
                        <button className={"Button-header-small"} onClick={decreaseFont}>-</button>
                        <button className={"Button-header"} onClick={switchTheme}>theme</button>
                    </div>
                </div>
                <div className={"Navigation"}>
                    <div className={"Navigation-spacer"}/>
                    <Link to={"/"}><button className={"Button-navigation"}>Home</button></Link>
                    <button className={"Button-navigation"}>Mijn profiel</button>
                </div>
            </div>
            
            <div className={"Body"}>
                <button onClick={switchTheme}>
                    switch to {theme === 'light' ? 'dark' : 'light'}
                </button>
                <button onClick={increaseFont}>increase font</button>
                <button onClick={decreaseFont}>decrease font</button>
                <h1>H1 text here</h1>
                <h2>H2 lorem ipsum</h2>
                <h3>H3 example</h3>
                <p>P als een example stuk text hier</p>
                <p>

                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus mi eros, vehicula nec eleifend at, eleifend vitae sapien. Maecenas porttitor vestibulum neque at venenatis. Aliquam vitae nibh non justo aliquet consequat. Vivamus dapibus gravida dolor nec hendrerit. Vestibulum molestie velit ipsum, nec vestibulum lacus tincidunt quis. Donec nunc arcu, ultricies et nunc in, tincidunt aliquam nibh. Mauris sed augue non risus consectetur interdum vel et velit. Aenean viverra scelerisque metus in porttitor. Fusce imperdiet sit amet dui eget sagittis. Fusce euismod dapibus eros nec efficitur.

                    Integer mollis a dui non aliquam. Vivamus molestie fringilla pellentesque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Vestibulum ut erat et nunc posuere venenatis. Donec placerat at lectus vitae tempor. Etiam interdum tortor enim, eget malesuada tortor consequat ac. Phasellus vestibulum aliquam vehicula. Maecenas dictum est nisi, vitae feugiat nibh dignissim at. Aenean lacus massa, maximus vitae orci quis, vulputate pretium nisl.

                    Quisque posuere felis eget aliquet hendrerit. Sed faucibus, metus eu luctus mattis, justo lectus faucibus eros, in feugiat mi neque vel diam. Nunc eu dolor interdum, aliquet massa vitae, lacinia mauris. Vestibulum ac neque est. Pellentesque bibendum, eros ac volutpat tempus, dolor lorem facilisis ex, vitae pretium ligula quam id ante. Aliquam gravida nisi id ligula placerat vulputate. Proin dignissim, arcu eget pulvinar scelerisque, arcu sapien iaculis odio, non porta enim tellus at nibh. Nam porttitor auctor orci, eget aliquam turpis mollis ac. Morbi lobortis vulputate ex, ut pretium nibh congue nec. Aenean ac dictum nulla. Morbi ornare justo et tellus tempor sodales. Sed lacinia eros sed sodales sagittis. Proin ligula velit, iaculis eu enim ut, mattis vulputate enim. Duis finibus tortor sagittis, feugiat orci ullamcorper, consequat sem. Nulla commodo varius rhoncus. Praesent sollicitudin lectus eleifend quam vehicula, ut convallis leo placerat.

                    Praesent malesuada urna quis leo gravida ornare. Mauris semper at eros nec molestie. Ut eu volutpat lectus. Curabitur gravida porttitor nunc at posuere. In non orci tempus, condimentum nisl a, tincidunt leo. Nullam sed fringilla enim. Nulla facilisi. Vestibulum iaculis nulla at velit cursus sodales. Nam tristique eros varius metus vestibulum rutrum vitae in quam.

                    Aenean porttitor in lectus in volutpat. In ultricies quam quis consectetur varius. Quisque eget rhoncus libero. Aliquam vel lobortis velit, nec ultrices velit. Nunc non bibendum purus. Phasellus pharetra volutpat lectus. Vivamus lobortis hendrerit scelerisque. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nullam sagittis purus eget justo pretium euismod. Nulla vestibulum urna quis consectetur pharetra. Suspendisse venenatis sapien non nisl congue mollis. Integer imperdiet sapien eget consequat consequat. Nullam justo magna, suscipit ut rhoncus maximus, porttitor aliquam urna. Pellentesque vel efficitur urna, faucibus placerat ex. Etiam placerat erat dolor, sed molestie orci laoreet id. In convallis vulputate tellus eu suscipit. </p>
            </div>
        </div>
    )
}

export default Font;