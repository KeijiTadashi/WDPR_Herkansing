import React from 'react';
import '../CSS/StichtingTheme';
import useLocalStorage from 'use-local-storage';
import { PropTypes } from 'prop-types';

function OpenBeoordeling({index, onAnswerChange}){
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
                
                <label htmlFor={`${namePrefix}_invoerveld`}>
                    <input
                        type="radio"
                        value="1"
                        name={namePrefix}
                        id={`${namePrefix}_zeer_goed`}
                        onChange={()=>handleInputChange("Zeer_goed")}
                    />
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


RadioBeoordeling.propTypes = {
    index: PropTypes.number.isRequired,
    onAnswerChange: PropTypes.func.isRequired,
};

export default OpenBeoordeling;