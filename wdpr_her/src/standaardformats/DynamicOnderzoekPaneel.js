import React, { useState } from 'react';
import OnderzoekItem from '../standaardformats/OnderzoekItem';
import '../CSS/Onderzoeken.css';
import SurveyFeedbackForm from './SurveyFeedbackForm';
import axios from "axios";
import useLocalStorage from 'use-local-storage';

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
    
    const exportData = async () => {
        console.log('Exported data:', answers);
        console.log('Export Feedback data:', feedback);

        // TODO: Export data to the database via API
        try{
            const opdrachtResponsData = {

                //TODO: fix dat hier de echte UserID en OnderzoekID in komen te staan
                UserID: 1, //vervang dit
                OnderzoekID: 1, //vervang ook dit
                VraagMetAntwoordenJSON: JSON.stringify(answers)
            };

            const accesToken = localStorage.getItem("token");

            const response = await axios.post(apiPath + "opdrachtrespons", opdrachtResponsData, {
                headers: {
                    Authorization:`Bearer ${accesToken}`,
                    'Content-Type': 'application/json',
                },
            });

            console.log('OpdrachtRespons created successfully:', response.data);
        }catch(error){
            console.error('Fout in \nDynamicOnderzoekPaneel -> exportData\n bij het maken van een OpdrachtRespons')
        }

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