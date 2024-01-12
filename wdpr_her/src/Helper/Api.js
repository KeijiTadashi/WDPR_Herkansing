import {isDev} from "./Development";

// Todo Change the else (second link) to the online API path once it's online
// export const apiPath = isDev() ? "http://localhost:5027/" : "https://apiwdprher.azurewebsites.net/";
export const apiPath = isDev() ? "https://apiwdprher.azurewebsites.net/" : "https://apiwdprher.azurewebsites.net/";
