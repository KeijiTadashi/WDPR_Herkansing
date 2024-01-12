import React from 'react';
import '../CSS/StichtingTheme.css';
import Beoordeling from './Beoordeling';
import PropTypes from 'prop-types';

const OnderzoekItem = ({indexnr, vraag, maxVraagNr, onAnswerChange}) => {
    const INDEX = indexnr?indexnr:'-1';
    const VRAAG = vraag?vraag:'Geen vraag ingevuld';
    const TOTAALAANTALVRAGEN = maxVraagNr?maxVraagNr:-1;

    const handleAnswerChange = (value) =>{
        onAnswerChange(value);
    };

    return (
        <div className="Onderzoek">
            <h3>{`Vraag ${INDEX} van ${TOTAALAANTALVRAGEN}`}</h3>
            <p>{`${VRAAG}`}</p>
            <Beoordeling index={INDEX} onAnswerChange={handleAnswerChange}/>
        </div>
    )
}

OnderzoekItem.propTypes = {
    indexnr: PropTypes.number.isRequired,
    vraag: PropTypes.string.isRequired,
    maxVraagNr: PropTypes.number,
    onAnswerChange: PropTypes.func.isRequired,
}

export default OnderzoekItem;