import React, { useState } from 'react';
import OnderzoekItem from '../standaardformats/OnderzoekItem';
import '../CSS/Onderzoeken.css'

const DynamicOnderzoekPaneel = ({ onderzoekArray }) => {
    const [answers, setAnswers] = useState([])

    const handleAnswerChange = (index, value) => {
        const newAnswers = [...answers];
        newAnswers[index] = { vraag: onderzoekArray[index].vraag, answer: value };
        setAnswers(newAnswers)
    };

    const exportData = () => {
        //TODO exporteer answers array naar de database via API
        console.log('Exported data:', answers)
    };

    return (
        <div className='DynamicOnderzoekPaneel'>
            {onderzoekArray.map((onderzoek, index) => (
                <OnderzoekItem
                    class='OnderzoekItem'
                    key={onderzoek.id || index} //Als onderzoek een ID heeft gebruikt het die. anders de nieuwe index.
                    {...onderzoek}
                    indexnr={index + 1}
                    maxVraagNr={onderzoekArray.length}
                    onAnswerChange={(value)=>handleAnswerChange(index, value)}
                />
            ))}

            <button onClick={exportData} aria-label='Lever antwoorden in'>Lever in</button>
        </div>

    );
};

export default DynamicOnderzoekPaneel;
