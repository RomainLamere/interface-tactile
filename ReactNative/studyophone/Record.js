import React, {useContext, useState} from "react";
import {
    SafeAreaView,
    ScrollView,
    StatusBar,
    StyleSheet,
    Text,
    useColorScheme,
    View,
    Button,
} from 'react-native';
import AudioRecord from 'react-native-audio-record';
import {UserContext} from "./UserContext";
import {readFile} from 'react-native-fs';

export const Record = () =>{
    const [userContext, setUserContext] = useContext(UserContext);
    //const [path, setPath] = useState('');
    const [sound, setSound] = useState('');
    AudioRecord.on('data', data => {
        //console.debug(data);
        setSound(sound + data);
    });
    const options = {
        sampleRate: 16000, // default 44100
        channels: 1, // 1 or 2, default 1
        bitsPerSample: 16, // 8 or 16, default 16
        audioSource: 6, // android only (see below)
        wavFile: 'test.wav', // default 'audio.wav'
    };
    async function getUriToBase64(uri) {
        const base64String = await readFile(uri, 'base64');
        return base64String;
    }
    return(
        <View style={{height:"100%"}}>
            <Button
                title={'record'}
                onPress={() => {
                    AudioRecord.init(options);
                    AudioRecord.start();
                }}
            />
            <Button
                title={'stop'}
                onPress={() => {
                    AudioRecord.stop().then(r => {
                        console.log(r);
                        getUriToBase64(r).then(r2 => {
                            console.log(sound);
                            const chunk = Buffer.from(r2, 'base64');
                            //Uint8Array.from(atob(base64_string), c => c.charCodeAt(0))
                            userContext.socket.emit('voiceFromPhone', chunk);
                            setSound('');
                        });
                    });
                }}
            />
        </View>
    )
}
