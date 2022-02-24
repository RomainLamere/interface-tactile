import React, {createContext, useState} from 'react';
const defaultValue = {
    socket: null
};
export const UserContext = createContext(defaultValue);
export const UserContextProvider = ({children}) => {
    const [userInfo, setUserInfo] = useState(defaultValue);
    return (
        <UserContext.Provider value={[userInfo, setUserInfo]}>
            {children}
        </UserContext.Provider>
    );
};
