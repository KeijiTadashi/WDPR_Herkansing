import "../CSS/StichtingTheme.css";
import useLocalStorage from "use-local-storage";
import Header from "../standaardformats/Header";

function App() {
  // Abstact this out
  // Check browser default theme preference
  const defaultDark = window.matchMedia("(prefers-color-scheme: dark)").matches;

  const [theme] = useLocalStorage("theme", defaultDark ? "dark" : "light");
  const [fontSize] = useLocalStorage("font-size", "normal");

  return (
    <>
      <div className="Main" data-theme={theme} data-font-size={fontSize}>
        <Header />
        {/*<div className="Main" data-theme={theme} data-font-size={fontSize}>*/}
        <div className={"Body"}></div>
      </div>
    </>
  );
}

export default App;
