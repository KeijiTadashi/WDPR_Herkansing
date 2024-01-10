import React from "react";
import useLocalStorage from 'use-local-storage';
import Header from "../standaardformats/Header";
import "../CSS/StichtingTheme.css";

export const Homepage = () => {

  const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

  const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
  const [fontSize] = useLocalStorage('font-size', 'normal');

  return (
    <>
      <Header />
      <div className="Main" data-theme={theme} data-font-size={fontSize}>
        <div className={"Body"}>

          <h1>Wie zijn wij?</h1>
          <p className="p">
            Wij zijn stichting accessability en wij....
            <br />
            <br />
            Hello darkness, my old friend
            <br />
            I&#39;ve come to talk with you again
            <br />
            Because a vision softly creeping
            <br />
            Left its seeds while I was sleeping
            <br />
            And the vision that was planted in my brain
            <br />
            Still remains
            <br />
            Within the sound of silence
            <br />
            In restless dreams I walked alone
            <br />
            Narrow streets of cobblestone
            <br />
            &#39;Neath the halo of a street lamp
            <br />I turned my collar to the cold and damp
            <br />
            When my eyes were stabbed by the flash of a neon light
            <br />
            That split the night
            <br />
            And touched the sound of silence
          </p>
        </div>
      </div>
    </>
  );
};

export default Homepage;