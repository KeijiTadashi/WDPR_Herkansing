import {isDev} from "./Development";

// Todo Change the else (second link) to the online API path once it's online
export const apiPath = isDev() ? "http://localhost:5027/" : "http://localhost:5027/";