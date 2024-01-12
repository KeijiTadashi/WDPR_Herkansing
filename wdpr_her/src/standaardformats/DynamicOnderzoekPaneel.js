import React, {useState} from 'react';
import OnderzoekItem from '../standaardformats/OnderzoekItem';

const DynamicOnderzoekPaneel = ({ onderzoekArray }) => {
    const [vraagCounter] = useState(1);
    const amountOfElements = onderzoekArray.length;
    return (
        <>
            {onderzoekArray.map((onderzoek, index) => (
                <OnderzoekItem
                    key={onderzoek.id || index}  // Use a unique identifier if available
                    {...onderzoek}
                    indexnr={index + 1}  // Assuming you want to start indexing from 1
                    maxVraagNr={amountOfElements}
                />
            ))}
        </>
    );
};

export default DynamicOnderzoekPaneel;
