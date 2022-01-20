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
  Button, TextInput,
} from 'react-native';
import socketIOClient from 'socket.io-client';

import {
  Colors,
  DebugInstructions,
  Header,
  LearnMoreLinks,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';

const App: () => Node = () => {
  const ENDPOINT = '192.168.43.60:3000';
  const isDarkMode = useColorScheme() === 'dark';

  const backgroundStyle = {
    backgroundColor: isDarkMode ? Colors.darker : Colors.lighter,
  };
  const [response, setResponse] = useState('rien');
  const [bpm, setBpm] = useState(0);
  const vibrationDuration = 100;
  const [text, setText] = useState('');

  useEffect(() => {
    const socket = socketIOClient(ENDPOINT);
    socket.on('FromAPI', data => {
      setResponse(data);
    });
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

  return (
    <SafeAreaView style={backgroundStyle}>
      <Text style={{alignSelf: 'center', marginTop: 20}}>{response}</Text>
      <TextInput placeholder={"enter your bpm"} keyboardType='numeric' onChangeText={(newText)=>setText(newText)} defaultValue={text}/>
      <Button onPress={() => setBpm(text === '' ? 0 : parseInt(text))} title={'enter'} />
      <Button title={"stop"} onPress={Vibration.cancel}/>
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
