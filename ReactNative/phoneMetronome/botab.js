import {createMaterialBottomTabNavigator} from '@react-navigation/material-bottom-tabs';
import {Metronome} from './Metronome';
import {Record} from './Record';
import React from 'react';
const Tab = createMaterialBottomTabNavigator();
export const Botab = () => {

  return (
    <Tab.Navigator
      initialRouteName="Metronome"
      labelStyle={{fontSize: 12}}
      barStyle={{backgroundColor: '#028200'}}>
      <Tab.Screen name={'Metronome'} component={Metronome} />
      <Tab.Screen name={'Pad'} component={Record} />
    </Tab.Navigator>
  );
};
