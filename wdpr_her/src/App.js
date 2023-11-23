import logo from './Logo Icon/icon_accessibility.png';
import logo_blauw from './Logo Icon/Op blauw/icon_accessibility_on-blue.png';
import logo_blauw_trans from './Logo Icon/Op blauw/Transparant/icon_accessibility_on-blue_transp.png';
import logo_donker from './Logo Icon/Op donker/icon_accessibility_on-dark.jpg';
import logo_donker_trans from './Logo Icon/Op donker/Transparant/icon_accessibility_on-dark_transp.png';
import './App.css';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        Welkom bij Stichting Accessibility
        <div className="Logo-row">
          <p className="Logo-text"><img src={logo} className="App-logo" alt="logo" /> default logo</p>
          <p className="Logo-text"><img src={logo_blauw} className="App-logo"/> logo on blue</p>
          <p className="Logo-text"><img src={logo_blauw_trans} className="App-logo"/> logo on blue transparent</p>
          <p className="Logo-text"><img src={logo_donker} className="App-logo"/> logo on dark </p>
          <p className="Logo-text"><img src={logo_donker_trans} className="App-logo"/> logo on dark transparent</p>
        </div>
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
