import React from 'react';
import '../CSS/StichtingTheme.css';
import RadioBeoordeling from './RadioBeoordeling';
import PropTypes from 'prop-types';
import OpenBeoordeling from './OpenBeoordeling';

const OnderzoekItem = ({ indexnr, vraag, maxVraagNr, onAnswerChange, type }) => {
    const INDEX = indexnr ? indexnr : '-1';
    const VRAAG = vraag ? vraag : 'Geen vraag ingevuld';
    const TOTAALAANTALVRAGEN = maxVraagNr ? maxVraagNr : -1;
    const TYPE = type ? type : "Open"

    const handleAnswerChange = (value) => {
        onAnswerChange(value);
    };

    if (TYPE == "Radio") {
        return (
            <div className="Onderzoek">
                <h3>{`Vraag ${INDEX} van ${TOTAALAANTALVRAGEN}`}</h3>
                <p>{`${VRAAG}`}</p>
                <RadioBeoordeling index={INDEX} onAnswerChange={handleAnswerChange} />
            </div>
        )
    }else{
        return(
            <div className="Onderzoek">
                <h3>{`Vraag ${INDEX} van ${TOTAALAANTALVRAGEN}`}</h3>
                <p>{`${VRAAG}`}</p>
                <OpenBeoordeling index={INDEX} onAnswerChange={handleAnswerChange}/>
            </div>
        )
    }
}

OnderzoekItem.propTypes = {
    indexnr: PropTypes.number.isRequired,
    vraag: PropTypes.string.isRequired,
    maxVraagNr: PropTypes.number,
    onAnswerChange: PropTypes.func.isRequired,
    type: PropTypes.string,
}

export default OnderzoekItem;