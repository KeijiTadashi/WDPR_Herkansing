import React from 'react';
import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import { PropTypes } from 'prop-types';
import { useState } from 'react';

function OpenBeoordeling({index, onAnswerChange}){
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    const namePrefix = `beoordeling_${index}`;

    const [antwoord, setAntwoord] = useState('');
    const handleInputChange = (value) =>{
        setAntwoord(value);
        onAnswerChange(value);
    }

    return (
        <div className="Main" data-theme={theme} data-font-size={fontSize}>
            <div className="Beoordeling">
                
                <label htmlFor={`${namePrefix}_invoerveld`}>
                    <textarea
                        type="text"
                        value={antwoord}
                        onChange={(e)=>handleInputChange(e.target.value)}
                        id={`Invoerveld ${namePrefix}`}
                    />
                </label>
                
                
            </div>
        </div>
    );
}


OpenBeoordeling.propTypes = {
    index: PropTypes.number.isRequired,
    onAnswerChange: PropTypes.func.isRequired,
};

export default OpenBeoordeling;