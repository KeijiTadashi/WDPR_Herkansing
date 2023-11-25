import logo from './Logo Icon/icon_accessibility.png';
import logo_blauw from './Logo Icon/Op blauw/icon_accessibility_on-blue.png';
import logo_blauw_trans from './Logo Icon/Op blauw/Transparant/icon_accessibility_on-blue_transp.png';
import logo_donker from './Logo Icon/Op donker/icon_accessibility_on-dark.jpg';
import logo_donker_trans from './Logo Icon/Op donker/Transparant/icon_accessibility_on-dark_transp.png';
// import './App.css';
import './Theme.css';
import useLocalStorage from 'use-local-storage';

function App() {
  // Check browser default theme preference
  const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
  const [theme , setTheme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
  let curThemeLogo = theme === 'light' ? logo : theme === 'blue' ? logo_blauw_trans : logo_donker_trans;

  const switchTheme = () => {
    const newTheme = theme === 'light' ? 'blue' : theme === 'blue' ? 'dark' : 'light';
    setTheme(newTheme);
    curThemeLogo = theme === 'light' ? logo : theme === 'blue' ? logo_blauw_trans : logo_donker_trans;
  }
  
  return (
    <div className="App" data-theme={theme}>
      <header className="App-header">
        Welkom bij Stichting Accessibility
        <div className="Logo-row">
          <p className="Logo-text"><img src={logo} className="App-logo" alt="logo" /> default logo</p>
          <p className="Logo-text"><img src={logo_blauw} className="App-logo"/> logo on blue</p>
          <p className="Logo-text"><img src={logo_blauw_trans} className="App-logo"/> logo on blue transparent</p>
          <p className="Logo-text"><img src={logo_donker} className="App-logo"/> logo on dark </p>
          <p className="Logo-text"><img src={logo_donker_trans} className="App-logo"/> logo on dark transparent</p>
        </div>
        <div className={"Logo-row"} >
          <img src={curThemeLogo} className={"App-logo"} alt={"logo stichting accessibility"} /> <button onClick={switchTheme}>
          switch to {theme === 'light' ? 'blue' : theme === 'blue' ? 'dark' : 'light'} theme
        </button></div>
      </header>
    </div>
  );
}

export default App;
