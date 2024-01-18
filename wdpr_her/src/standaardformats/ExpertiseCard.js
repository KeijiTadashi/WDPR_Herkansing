import React from "react";
import useLocalStorage from "use-local-storage";
import "../CSS/StichtingTheme.css";
import "../CSS/homepage.css";
import  PropTypes from 'prop-types';

export const ExpertiseCard = ({kop, tekst}) => {
    const defaultDark = window.matchMedia("(prefers-color-scheme: dark)").matches;

    const [theme] = useLocalStorage("theme", defaultDark ? "dark" : "light");
    const [fontSize] = useLocalStorage("font-size", "normal");

    return (
        <div class="expertise-card" data-theme={theme} data-fontSize={fontSize}>
            <h3>{kop}</h3>
            <p>
                {tekst}
            </p>
        </div>
    );
};

ExpertiseCard.propTypes = {
    kop: PropTypes.string.isRequired,
    tekst: PropTypes.string.isRequired,
};

export default ExpertiseCard;
