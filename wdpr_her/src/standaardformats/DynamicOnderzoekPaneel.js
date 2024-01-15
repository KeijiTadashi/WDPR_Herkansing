import React, { useState } from 'react';
import OnderzoekItem from '../standaardformats/OnderzoekItem';
import '../CSS/Onderzoeken.css';
import SurveyFeedbackForm from './SurveyFeedbackForm';

const DynamicOnderzoekPaneel = ({ onderzoekArray }) => {
    const [answers, setAnswers] = useState([]);
    const [feedback, setFeedback] = useState([]);

    const handleAnswerChange = (index, value) => {
        const newAnswers = [...answers];
        newAnswers[index] = { vraag: onderzoekArray[index].vraag, answer: value };
        setAnswers(newAnswers);
    };

    const handleFeedback = (feedbackData) => {
        setFeedback(feedbackData);
    };
    
    const exportData = () => {
        console.log('Exported data:', answers);
        console.log('Export Feedback data:', feedback);
        // TODO: Export data to the database via API
    };

    return (
        <div className='DynamicOnderzoekPaneel'>
            {onderzoekArray.map((onderzoek, index) => (
                <OnderzoekItem
                    key={onderzoek.id || index}
                    {...onderzoek}
                    indexnr={index + 1}
                    maxVraagNr={onderzoekArray.length}
                    onAnswerChange={(value) => handleAnswerChange(index, value)}
                />
            ))}

            <SurveyFeedbackForm onFeedback={(feedbackData) => handleFeedback(feedbackData)} />

            <button onClick={exportData} aria-label='Lever antwoorden in'>
                Lever in
            </button>
        </div>
    );
};

export default DynamicOnderzoekPaneel;