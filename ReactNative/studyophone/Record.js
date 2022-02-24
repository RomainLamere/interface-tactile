import React, {useContext, useEffect, useState} from "react";
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
import StopWatch from "react-native-stopwatch-timer/lib/stopwatch";
import MaterialCommunityIcons from 'react-native-vector-icons/MaterialCommunityIcons';
import Pressable from "react-native/Libraries/Components/Pressable/Pressable";

export const Record = () =>{
    const [userContext, setUserContext] = useContext(UserContext);
    //const [path, setPath] = useState('');
    const [sound, setSound] = useState('');
    AudioRecord.on('data', data => {
        //console.debug(data);
        setSound(sound + data);
    });
    const [timerInPause, setTimerInPause] = useState(true)
    const [isRecord,setIsRecord] =  useState(false)
    const [currentTimerCount, setCurrentTimerCount] = useState(0)
    const [enableRecord, setEnableRecord]= useState(true)
    const [chunk,setChunk]= useState(null)
    const [reset,setReset]=useState(false)
    useEffect(()=>{
            if(!timerInPause){

                    setInterval(()=>{setCurrentTimerCount(currentTimerCount+10)},10)

            }
        },
        [timerInPause])
    const changeTimerPause=()=>{
        if(timerInPause){
            setTimerInPause(false)
        }
        else {
            setTimerInPause(true)
            setCurrentTimerCount(0)
            setEnableRecord(false)
        }
    }
    const transformTimer = (timer)=>{
        let sec = Math.floor(timer/1000)
        let mil=timer-1000*sec
        if(sec<10){
            sec='0'+sec
        }
        return(sec+':'+mil)
    }
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
    useEffect(()=>{setReset(false)},[reset])
    return(
        <View style={{height:"100%", marginTop:20}}>
            <Text style={{marginBottom:10}}>Current Record Time :</Text>
            <StopWatch msecs start={!timerInPause} reset={reset}/>
            <View style={{marginTop:20}}>
            <Button
                disabled={!enableRecord}
                title={isRecord?"stop":'record'}
                onPress={() => {

                    if(!isRecord) {
                        changeTimerPause()
                        AudioRecord.init(options);
                        AudioRecord.start();
                    }
                    else {
                        changeTimerPause()
                        AudioRecord.stop().then(r => {
                            console.log(r);
                            getUriToBase64(r).then(r2 => {
                                console.log(sound);
                                setChunk(  Buffer.from(r2, 'base64'))
                                //Uint8Array.from(atob(base64_string), c => c.charCodeAt(0))

                            });
                        });
                    }
                    setIsRecord(!isRecord)
                }}
            />
            </View>
            {!enableRecord&&
                <View style={{
                    display: "flex",
                    flexDirection: "row",
                    alignItems: "center",
                    justifyContent: "center",
                    marginTop: 100
                }}>
                    <Pressable style={{display: "flex", flexDirection: "column"}} onPress={()=>{
                        setEnableRecord(true)
                        userContext.socket.emit('voiceFromPhone', chunk);
                        setSound('');
                        setReset(true)
                    }}>
                        <MaterialCommunityIcons
                            name="send"
                            size={55}
                            color={'white'}
                            style={{marginRight: 20, alignSelf: 'center'}}
                        />
                        <Text>Envoyer</Text>
                    </Pressable>
                    <Pressable style={{display: "flex", flexDirection: "column", marginLeft: 50}} onPress={()=>{
                        setEnableRecord(true)
                        setReset(true)
                    }}>
                        <MaterialCommunityIcons
                            name="delete"
                            size={55}
                            color={'white'}
                            style={{marginRight: 20, alignSelf: 'center'}}
                        />
                        <Text>Supprimer</Text>
                    </Pressable>
                </View>
            }
        </View>
    )
}
