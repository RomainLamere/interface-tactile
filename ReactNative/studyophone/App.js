/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow strict-local
 */

import React, {useEffect} from 'react';
import type {Node} from 'react';
import {
  PermissionsAndroid,
  SafeAreaView,
  ScrollView,
  StatusBar,
  StyleSheet,
  Text,
  useColorScheme,
  View,
} from 'react-native';

import {
  Colors,
  DebugInstructions,
  Header,
  LearnMoreLinks,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';
import {DarkTheme, NavigationContainer} from "@react-navigation/native";
import {BottomNavigation} from "./BottomTabs";
import {Topbar} from "./TopBar";
import {UserContextProvider} from "./UserContext";

const App: () => Node = () => {
  const isDarkMode = useColorScheme() === 'dark';
  const backgroundStyle = {
    backgroundColor: isDarkMode ? Colors.darker : Colors.lighter,
  };
  useEffect(() => {
    PermissionsAndroid.request(
        PermissionsAndroid.PERMISSIONS.RECORD_AUDIO,
        //PermissionsAndroid.PERMISSIONS.READ_EXTERNAL_STORAGE,
    );
  }, []);
  return (

      <NavigationContainer  theme={DarkTheme}>
        <UserContextProvider>
        <Topbar/>
        <BottomNavigation/>
        </UserContextProvider>
      </NavigationContainer>

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
