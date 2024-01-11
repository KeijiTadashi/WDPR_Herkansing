import {isDev} from "./Development";
import axios from "axios";

// Todo Change the else (second link) to the online API path once it's online
// export const apiPath = isDev() ? "http://localhost:5027/" : "https://apiwdprher.azurewebsites.net/";
export const apiPath = isDev() ? "https://apiwdprher.azurewebsites.net/" : "https://apiwdprher.azurewebsites.net/";

export async function apiPost(url, data)
{
    // TODO remove commented code once this works on the live server
    // const response = await fetch(apiPath + url, {
    //     method: 'Post',
    //     body: JSON.stringify(data),
    //     mode: "cors",
    //     headers: {
    //         "Content-Type": "application/json",
    //         // "Access-Control-Allow-Origin": true
    //     }
    // });
    // console.log(response);
    // const result = await response.text();
    // console.log(result);


    axios
        .post(apiPath + url, data) //{api url}/Test/CreateTest with the testInfo as the provided data
        .then((response) => {
            console.log(response);
            return response;
        });
    
}

export function apiGet(url, data)
{
    axios.get(apiPath + url, )
}