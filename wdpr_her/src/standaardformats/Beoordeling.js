// Beoordeling.js
import React from 'react';
import '../CSS/StichtingTheme'
import useLocalStorage from 'use-local-storage';

function Beoordeling() {

    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');

    return (
        <div className="Beoordeling">
            <label for='checkbox_zeer_goed'>
                <input type="checkbox" value="1" name='checkbox_zeer_goed'/>
                Zeer goed
            </label>
            <label>
                <input type="checkbox" value="2" />
                Goed
            </label>
            <label>
                <input type="checkbox" value="3" />
                Neutraal
            </label>
            <label>
                <input type="checkbox" value="4" />
                Slecht
            </label>
            <label>
                <input type="checkbox" value="5" />
                Zeer slecht
            </label>
        </div>
    );
}

export default Beoordeling;
