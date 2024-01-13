// Opdrachtpaneel.js

import React from "react";
import PropTypes from 'prop-types';
import "../CSS/Opdrachtpaneel.css";

const OpdrachtPaneel = ({ naam, status, aantaldeelnemers, indexnr }) => {

    const NAAM = naam?naam:"Geen naam opgegeven";
    const STATUS = status?status:"Status niet beschikbaar";
    const AANTALDEELNEMERS = aantaldeelnemers?aantaldeelnemers:-1;
    const INDEXNR = indexnr?indexnr:-1;

    return (
        <div className="opdracht-panel">
            <div className="heading">
                <p className="text-wrapper">{`Opdracht ${INDEXNR} - ${NAAM}`}</p>
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
    naam: PropTypes.string.isRequired,
    status: PropTypes.string.isRequired,
    aantaldeelnemers: PropTypes.number,
    indexnr: PropTypes.number,
};

export default OpdrachtPaneel;
