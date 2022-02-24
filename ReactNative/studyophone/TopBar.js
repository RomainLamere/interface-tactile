import React, {useContext, useEffect, useState} from "react";
import {
    SafeAreaView,
    ScrollView,
    StatusBar,
    StyleSheet,
    Text,
    useColorScheme,
    View,
    TextInput,
} from 'react-native';
import socketIOClient from 'socket.io-client';
import {UserContext} from "./UserContext";
export const Topbar = () =>{
    const [textAdd, setTextAdd] = useState('');
    const [ENDPOINT, setEndpoint] = useState('192.168.43.60:3000');
    const [userContext, setUserContext] = useContext(UserContext);
    useEffect(() => {
        setUserContext(data => ({...data, socket: socketIOClient('http://' + ENDPOINT + ':3000/')}));
        //setSocket(socketIOClient('http://' + ENDPOINT + ':3000/'));

    }, [ENDPOINT]);
    return(
        <View style={{height:80}}>
            <TextInput
                placeholder={'enter the address'}
                keyboardType="numeric"
                onChangeText={newText => setTextAdd(newText)}
                defaultValue={textAdd}
                onEndEditing={() => setEndpoint(textAdd)}
            />
            <Text>Current address is : {ENDPOINT}</Text>
        </View>
    )
}
