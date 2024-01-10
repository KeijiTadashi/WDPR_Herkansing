import React from "react";

import "../CSS/Opdrachtpaneel.css";
import '../CSS/Theme.css'

import useLocalStorage from "use-local-storage";
import PropTypes from 'prop-types';

export const OpdrachtPaneel = (props) => {

    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');

    const NAAM = props.naam;
    const STATUS = props.status;
    const AANTALDEELNEMERS = props.aantaldeelnemers ? props.aantaldeelnemers : 0;

    return (
        <div className="opdracht-panel">
            <div className="heading">
                <p className="text-wrapper">Opdracht 1 - {NAAM}</p>
            </div>
            <div className="info-line">
                <div className="div">Status</div>
                <div className="text-wrapper-2">{STATUS}</div>
            </div>
            <div className="info-line">
                <div className="div">Aantal deelnemers</div>
                <div className="text-wrapper-2">{AANTALDEELNEMERS}</div>
            </div>
            <div className="frame">
                <button className="button">
                    <div className="button-txt">Meer informatie</div>
                </button>
            </div>
        </div>
    );
};

OpdrachtPaneel.propTypes = {
    naam : PropTypes.string.isRequired,
    status: PropTypes.string.isRequired,
    aantaldeelnemers: PropTypes.number,
};

export default OpdrachtPaneel;