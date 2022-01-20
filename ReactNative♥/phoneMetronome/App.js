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
    Vibration
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
  const ENDPOINT = '';
  const isDarkMode = useColorScheme() === 'dark';

  const backgroundStyle = {
    backgroundColor: isDarkMode ? Colors.darker : Colors.lighter,
  };
  const [response, setResponse] = useState('');
  const [bpm, setBpm] = useState(0);
  const vibrationDuration = 200;

  useEffect(() => {
    const socket = socketIOClient(ENDPOINT);
    socket.on('FromAPI', data => {
      setResponse(data);
    });
  }, []);

  useEffect(() => {
    let waitDuration = (60000 - bpm * vibrationDuration) / bpm;
    let pattern = [vibrationDuration, waitDuration];
    Vibration.vibrate(pattern);
  }, [bpm]);

  return (
    <SafeAreaView style={backgroundStyle}>
      <Text>{response}</Text>
      <button onClick={() => setBpm(60)}>BPM=60</button>
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
