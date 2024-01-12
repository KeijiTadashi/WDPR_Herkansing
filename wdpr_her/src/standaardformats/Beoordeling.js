import React from 'react';
import '../CSS/StichtingTheme.css';
import useLocalStorage from 'use-local-storage';

function Beoordeling({ index }) {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    const namePrefix = `beoordeling_${index}`;

    return (
        <div className="Main" data-theme={theme} data-font-size={fontSize}>
            <div className="Beoordeling">
                
                <label htmlFor={`${namePrefix}_zeer_goed`}>
                    <input
                        type="radio"
                        value="1"
                        name={namePrefix}
                        id={`${namePrefix}_zeer_goed`}
                    />
                    Zeer goed
                </label>
                
                <label htmlFor={`${namePrefix}_goed`}>
                    <input 
                        type="radio"
                        value="2"
                        name={namePrefix}
                        id={`${namePrefix}_goed`}
                    />
                    Goed
                </label>

                <label htmlFor={`${namePrefix}_neutraal`}>
                    <input
                        type="radio"
                        value="3"
                        name={namePrefix}
                        id={`${namePrefix}_neutraal`}
                    />
                    Neutraal
                </label>

                <label htmlFor={`${namePrefix}_slecht`}>
                    <input
                        type="radio"
                        value="4"
                        name={namePrefix}
                        id={`${namePrefix}_slecht`}
                    />
                    Slecht
                </label>
                
                <label htmlFor={`${namePrefix}_zeer_slecht`}>
                    <input
                        type="radio"
                        value="5"
                        name={namePrefix}
                        id={`${namePrefix}_zeer_slecht`}
                    />
                    Zeer slecht
                </label>
            </div>
        </div>
    );
}

export default Beoordeling;
