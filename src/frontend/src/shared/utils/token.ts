export const getToken = (): string => {
    return localStorage.getItem("accessToken") ?? "";
}

export const setToken = (token: string): void => {
    localStorage.setItem("accessToken", token);
}