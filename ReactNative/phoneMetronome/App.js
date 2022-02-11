/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow strict-local
 */

import React, {useEffect, useState} from 'react';
import type {Node} from 'react';
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
  PermissionsAndroid,
} from 'react-native';
import socketIOClient from 'socket.io-client';
import AudioRecord from 'react-native-audio-record';

import {
  Colors,
  DebugInstructions,
  Header,
  LearnMoreLinks,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';

const App: () => Node = () => {
  const [ENDPOINT, setEndpoint] = useState('192.168.43.60:3000');
  const isDarkMode = useColorScheme() === 'dark';
  const options = {
    sampleRate: 16000, // default 44100
    channels: 1, // 1 or 2, default 1
    bitsPerSample: 16, // 8 or 16, default 16
    audioSource: 6, // android only (see below)
    wavFile: 'test.wav', // default 'audio.wav'
  };

  const backgroundStyle = {
    backgroundColor: isDarkMode ? Colors.darker : Colors.lighter,
  };
  const [response, setResponse] = useState('rien');
  const [bpm, setBpm] = useState(0);
  const vibrationDuration = 100;
  const [text, setText] = useState('');
  const [textAdd, setTextAdd] = useState('');
  const [path, setPath] = useState('');
  const [sound, setSound] = useState('');
  const [socket, setSocket] = useState(socketIOClient(''));

  useEffect(() => {
    setSocket(socketIOClient('http://' + ENDPOINT + ':3000/'));
    socket.on('changeBPM', data => {
      setResponse(JSON.stringify(data));
      console.debug(JSON.parse(data).bpm);
      let eta_ms = parseInt(JSON.parse(data).timestamp) - Date.now();
      setTimeout(() => {
        setBpm(parseInt(JSON.parse(data).bpm));
      }, eta_ms);
    });
  }, [ENDPOINT]);
  useEffect(() => {
    PermissionsAndroid.request(
      PermissionsAndroid.PERMISSIONS.RECORD_AUDIO,
      //PermissionsAndroid.PERMISSIONS.READ_EXTERNAL_STORAGE,
    );
  }, []);
  useEffect(() => {
    console.debug('in');
    if (bpm !== 0) {
      let waitDuration = (60000 - bpm * vibrationDuration) / bpm;
      console.debug(waitDuration);
      let pattern = [0, vibrationDuration, waitDuration];

      Vibration.vibrate(pattern, true);
    }
  }, [bpm]);
  AudioRecord.on('data', data => {
    //console.debug(data);
    setSound(sound + data);
  });
  return (
    <SafeAreaView style={backgroundStyle}>
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
      <TextInput
        placeholder={'enter the address'}
        keyboardType="numeric"
        onChangeText={newText => setTextAdd(newText)}
        defaultValue={textAdd}
        onEndEditing={() => setEndpoint(textAdd)}
      />
      <Text>Current address is : {ENDPOINT}</Text>
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
            console.log(sound);
            socket.emit('voiceFromPhone', sound);
            setSound('');
          });
        }}
      />
      <Text>{path}</Text>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  sectionContainer: {
    marginTop: 32,
    paddingHorizontal: 24,
  },
  sectionTitle: {
    fontSize: 24,
    fontWeight: '600',
  },
  sectionDescription: {
    marginTop: 8,
    fontSize: 18,
    fontWeight: '400',
  },
  highlight: {
    fontWeight: '700',
  },
});

export default App;
