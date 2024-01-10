import '../StichtingTheme.css';
import {apiPath, apiPost} from "../Helper/Api";
import axios from "axios";
import {useState} from "react";
import Header from "../standaardformats/Header";
import useLocalStorage from "use-local-storage";

export function ApiExample() {

    const [theme] = useLocalStorage('theme');
    const [fontSize] = useLocalStorage('font-size');


    const [testData, setTestData] = useState({
        data: "",
        show: false
    });

    //Axios post request (in TestController.cs)
    const AddTest = () => {
        // The data being send in the request (needs to be one object), variable names have to be the same as in the API
        // (Though capitalization doesn't matter, so NAME is the same as Name == nAmE == name etc; Same for API path)
        const testInfo =
            {
                "name": "test",
                "ditiseentestbool": true
            }
        // axios
        //     .post(apiPath + "Test/CreateTest", testInfo) //{api url}/Test/CreateTest with the testInfo as the provided data
        //     .then((response) => {
        //         console.log(response);
        //     });
        
        apiPost("Test/CreateTest", testInfo);
    }

    const ShowTest = () => {
        // Get the data if it isn't set (only use if the data isn't changing in the database, not sure if a page reload (f5) would save data or not)
        if (testData.data === "") {
            axios.get(apiPath + "Test/" + 2) //Magic number, gets the test with id 2
                .then((response) => {
                    console.log(response);
                    setTestData({
                        data: response.data, //Get the data that is sent back
                        show: true
                    });
                })
        } else {
            // If you already have the data switch between showing and not showing the data while keeping the rest of the testData object the same
            setTestData({
                ...testData, // ...StateName keeps the state the same except for other data that is changed (in this cases show)
                show: !testData.show
            });
        }
    }

    const getTest = () => {
        axios.get(apiPath + "Test/" + 1) //Magic number, gets the test with id 1
            .then((response) => {
                setTestData({
                    data: response.data, //Get the data that is sent back
                    show: true
                });
            })
    }

    const getAllTests = () => {
        axios.get(apiPath + "Test").then((response) => setTestData({data: response.data, show: true}))
    }

    const updateTestBool = () => {
        // just because I want to swap the data from true to false and vice versa, normally you just do one axios request
        // getTest sets testData.data to Test with id 1 and testData.data.isTest = true OR false
        getTest();
        axios
            .put(apiPath + "Test/UpdateIsTest/" + 1 + "?isTest=" + !testData.data.isTest) //since this is without [FromBody] in the api needs the arguments in the url
            .then(response => setTestData({
                data: response.data,
                show: true
            }));
    }

    // What to render with <TestList />
    const TestList = () => {
        /*If it's an array, map it to a list else display single test data (trying to map a single object causes errors)*/
        /* &#40; = '(' and 41 = ')' */
        return testData.data instanceof Array ? (
            <ul>
                {testData.data.map((d) => (
                    <li>
                        &#40;ID = {d.id}&#41;
                        Naam: {d.name}
                        IsTest: {d.isTest.toString()}
                    </li>
                ))}
            </ul>
        ) : (<>Single test data ID = {testData.data.id} =&62; Naam: {testData.data.name}
            <br/>IsTest: {testData.data.isTest.toString()}</>);
    }
    /* 
    In button ShowTest, change the text to show or hide test based on the current state
    If testData.show === true, show the data, else null
    */
    return (
        <>
            <div className="Main" data-theme={theme} data-font-size={fontSize}>
                <Header/>
                <button onClick={AddTest}>Add Test to DB</button>
                <button onClick={ShowTest}>{testData.show ? "Hide Test" : "Show Test"}</button>
                <button onClick={getAllTests}>Get all Tests</button>
                <button onClick={getTest}>Get id = 1 Test</button>
                <button onClick={updateTestBool}>swap id=1 bool</button>
                {testData.show ? <TestList/> : null}
            </div>
        </>
    )
}