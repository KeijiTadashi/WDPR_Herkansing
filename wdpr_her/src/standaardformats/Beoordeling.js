// Beoordeling.js
import React from 'react';
import '../CSS/StichtingTheme.css'
import useLocalStorage from 'use-local-storage';

function Beoordeling() {

    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    return (
        <div className="Main" data-theme={theme} data-font-size={fontSize}>
            <div className="Beoordeling">
                
                <label for='checkbox_zeer_goed'>
                    <input
                        type="checkbox"
                        value="1"
                        name='checkbox_zeer_goed'
                    />
                    Zeer goed
                </label>
                

                <label for='checkbox_goed'>
                    <input 
                        type="checkbox"
                        value="2"
                        name='checkbox_goed' 
                    />
                    Goed
                </label>

                <label for='checkbox_neutraal'>
                    <input
                        type="checkbox"
                        value="3"
                        name='checkbox_neutraal'
                    />
                    Neutraal
                </label>

                <label for='checkbox_slecht'>
                    <input
                        type="checkbox"
                        value="4"
                        name='checkbox_slecht'
                    />
                    Slecht
                </label>
                <label for='checkbox_zeer_slecht'>
                    <input
                        type="checkbox"
                        value="5"
                        name='checkbox_zeer_slecht'
                    />
                    Zeer slecht
                </label>
            </div>
        </div>
    );
}

export default Beoordeling;
