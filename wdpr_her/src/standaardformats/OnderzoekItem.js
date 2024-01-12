import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import Beoordeling from './Beoordeling';
import PropTypes from 'prop-types';

const OnderzoekItem = ({indexnr, vraag, maxVraagNr}) => {
    const INDEX = indexnr?indexnr:'-1';
    const VRAAG = vraag?vraag:'Geen vraag ingevuld';
    const TOTAALAANTALVRAGEN = maxVraagNr?maxVraagNr:-1;

    return (
        <div className="Onderzoek">
            <h3>{`Vraag ${INDEX} van ${TOTAALAANTALVRAGEN}`}</h3>
            <p>{`${VRAAG}`}</p>
            <Beoordeling index={INDEX}/>
        </div>
    )
}

OnderzoekItem.propTypes = {
    indexnr: PropTypes.number.isRequired,
    vraag: PropTypes.string.isRequired,
    maxVraagNr: PropTypes.number,
}

export default OnderzoekItem;