import axios from "axios";
import {jwtDecode} from "jwt-decode";

export const SetAuthToken = (token) => {
    if (token) {
        localStorage.setItem("token", token);
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
        delete axios.defaults.headers.common['Authorization'];
        localStorage.removeItem("token");
    }
}

export const GetAuthTokenUser = () => {
    const token = localStorage.getItem("token");

    if (token === null) return false;

    const decodedToken = jwtDecode(token);
    const name = decodedToken.name;
    const exp = decodedToken.exp * 1000; //token.exp is in seconds from epoch, Date.now() is in milliseconds

    if (exp < Date.now()) {
        console.log("Expired - exp: " + exp + " now: " + Date.now());
        SetAuthToken();
        return false;
    }

    return name;
};