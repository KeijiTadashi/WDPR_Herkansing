import React from 'react';
import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';
import PropTypes from 'prop-types';

function RadioBeoordeling({ index, onAnswerChange }) {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    const namePrefix = `beoordeling_${index}`;

    const handleInputChange = (value) =>{
        onAnswerChange(value);
    };

    return (
        <div className="Main" data-theme={theme} data-font-size={fontSize}>
            <div className="Beoordeling">
                
                <label htmlFor={`${namePrefix}_zeer_goed`}>
                    <input
                        type="radio"
                        value="1"
                        name={namePrefix}
                        id={`${namePrefix}_zeer_goed`}
                        onChange={()=>handleInputChange("Zeer_goed")}
                    />
                    Zeer goed
                </label>
                
                <label htmlFor={`${namePrefix}_goed`}>
                    <input 
                        type="radio"
                        value="2"
                        name={namePrefix}
                        id={`${namePrefix}_goed`}
                        onChange={()=>handleInputChange("Goed")}
                    />
                    Goed
                </label>

                <label htmlFor={`${namePrefix}_neutraal`}>
                    <input
                        type="radio"
                        value="3"
                        name={namePrefix}
                        id={`${namePrefix}_neutraal`}
                        onChange={()=>handleInputChange("Neutraal")}
                    />
                    Neutraal
                </label>

                <label htmlFor={`${namePrefix}_slecht`}>
                    <input
                        type="radio"
                        value="4"
                        name={namePrefix}
                        id={`${namePrefix}_slecht`}
                        onChange={()=>handleInputChange("Slecht")}
                    />
                    Slecht
                </label>
                
                <label htmlFor={`${namePrefix}_zeer_slecht`}>
                    <input
                        type="radio"
                        value="5"
                        name={namePrefix}
                        id={`${namePrefix}_zeer_slecht`}
                        onChange={()=>handleInputChange("Zeer_slecht")}
                    />
                    Zeer slecht
                </label>
            </div>
        </div>
    );
}

RadioBeoordeling.propTypes = {
    index: PropTypes.number.isRequired,
    onAnswerChange: PropTypes.func.isRequired,
};

export default RadioBeoordeling;
