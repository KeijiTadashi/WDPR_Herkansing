import SurveyFeedbackForm from "../standaardformats/SurveyFeedbackForm";
import useLocalStorage from "use-local-storage";
import Header from '../standaardformats/Header';


export default function Feedback() {
    const defaultDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    const [theme] = useLocalStorage('theme', defaultDark ? 'dark' : 'light');
    const [fontSize] = useLocalStorage('font-size', 'normal');


    return (
        <div className="Main" data-theme={theme} data-font-size={fontSize}>
            <Header Title={Feedback} />
            <SurveyFeedbackForm />
        </div>
    )
}