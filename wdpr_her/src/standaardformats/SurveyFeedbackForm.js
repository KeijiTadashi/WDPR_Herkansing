import React, {useState} from 'react';
import '../CSS/EnqueteFeedback.css';
import PropTypes from 'prop-types';

const SurveyFeedbackForm = ({onFeedback, onSubmit}) => {
    const [satisfaction, setSatisfaction] = useState('');
    const [easeOfUnderstanding, setEaseOfUnderstanding] = useState('');
    const [suggestions, setSuggestions] = useState('');

    const handleSubmit = () => {
        const feedbackData = [
            {naam: '1: tevredenheid', value: satisfaction},
            {naam: '2: gebruikersgemak', value: easeOfUnderstanding},
            {naam: '3: tips en tops', value: suggestions}
        ];
        onFeedback(feedbackData);
        if (onSubmit) {
            onSubmit(); // Invoke the onSubmit prop if provided
        }
    };

    return (
        <div className='Feedbackform'>
            <h3>Enquête Feedback</h3>
            <h4>De volgende vragen gaan over de enquête zelf:</h4>
            <label htmlFor='Algemene tevredenheid'>
                <p>Hoe tevreden bent u over de enquête?</p>
                <textarea
                    type="text"
                    value={satisfaction}
                    onChange={(e) => setSatisfaction(e.target.value)}
                    id='Algemene tevredenheid'
                    aria-label='Schrijf hier iets over hoe tevreden u bent over het onderzoek'
                />
            </label>

            <label htmlFor='Gebruikersgemak'>
                <p>Hoe gemakkelijk vond u het om deel te nemen in het onderzoek?</p>
                <textarea
                    type="text"
                    value={easeOfUnderstanding}
                    onChange={(e) => setEaseOfUnderstanding(e.target.value)}
                    id='Gebruikersgemak'
                    aria-label='Schrijf hier iets over hoe u het gebruikersgemak hebt ervaren in dit onderzoek'
                />
            </label>

            <label htmlFor='tips en tops'>
                <p>Heeft u tips om het onderzoek te verbeteren?</p>
                <textarea
                    type="text"
                    value={suggestions}
                    onChange={(e) => setSuggestions(e.target.value)}
                    id='tips en tops'
                    aria-label='Schrijf hier eventueel feedback over het onderzoek'
                />
            </label>

            <button onClick={handleSubmit}>Submit Feedback</button>
        </div>
    );
};

SurveyFeedbackForm.propTypes = {
    onFeedback: PropTypes.func.isRequired,
    onSubmit: PropTypes.func // Optional prop for additional submission logic
};

export default SurveyFeedbackForm;
