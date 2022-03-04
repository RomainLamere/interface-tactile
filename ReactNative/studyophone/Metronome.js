import React, {useContext, useEffect, useState} from "react";
import {
    SafeAreaView,
    ScrollView,
    StatusBar,
    StyleSheet,
    Text,
    useColorScheme,
    View,
    Vibration,
    Button,
    TextInput,
    Switch,
} from 'react-native';
import {UserContext} from "./UserContext";

export const Metronome = () =>{
    const [userContext, setUserContext] = useContext(UserContext);
    const [response, setResponse] = useState('rien');
    const [bpm, setBpm] = useState(0);
    const vibrationDuration = 100;
    const [text, setText] = useState('');
    const [isEnabled, setIsEnabled] = useState(false);
    const toggleSwitch = () => setIsEnabled(previousState => !previousState);
    useEffect(()=>{
        if(userContext.socket!==null){
        userContext.socket.on('changeBPM', data => {
            setResponse(JSON.stringify(data));
            //console.debug(JSON.parse(data).bpm);
            let eta_ms = parseInt(JSON.parse(data).timestamp) - Date.now();
            setTimeout(() => {
                setBpm(parseInt(JSON.parse(data).bpm));
            }, eta_ms);
        });}
    },[userContext.socket])

    useEffect(() => {
        //console.debug('in');
        if (bpm>0 && bpm<250) {
            let waitDuration = (60000 - bpm * vibrationDuration) / bpm;
            //console.debug(waitDuration);
            let pattern = [0, vibrationDuration, waitDuration];

            Vibration.vibrate(pattern, true);
        }
    }, [bpm]);
    return(
        <View>
            <Text style={{alignSelf: 'center', marginTop: 20}}>
                From server : {response}
            </Text>
            <TextInput
                placeholder={'enter your bpm'}
                keyboardType="numeric"
                onChangeText={newText => setText(newText)}
                defaultValue={text}
            />
            <Button
                onPress={() => setBpm(text === '' ? 0 : parseInt(text))}
                title={'enter'}
            />
            <Button title={'stop'} onPress={Vibration.cancel} />
            <View style={{display:"flex", flexDirection:"row"}}>
                <Text>Activate BPM from serv</Text>
            <Switch
                trackColor={{ false: "#767577", true: "#81b0ff" }}
                thumbColor={isEnabled ? "#f5dd4b" : "#f4f3f4"}
                ios_backgroundColor="#3e3e3e"
                onValueChange={toggleSwitch}
                value={isEnabled}
            />
            </View>
        </View>
    )
}
