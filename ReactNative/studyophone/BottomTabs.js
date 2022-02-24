import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import React from "react";
import {Metronome} from "./Metronome";
import {Record} from "./Record";
import MaterialCommunityIcons from 'react-native-vector-icons/MaterialCommunityIcons';

const Tab = createBottomTabNavigator();

export const BottomNavigation =()=> {
    return (
        <Tab.Navigator>
            <Tab.Screen name="Metronome" component={Metronome} options={{
                tabBarIcon: ({color}) => (
                    <MaterialCommunityIcons
                        name="metronome"
                        size={26}
                        color={color}
                    />
                ),
            }}/>
            <Tab.Screen name="Pad" component={Record} options={{
                tabBarIcon: ({color}) => (
                    <MaterialCommunityIcons
                        name="dialpad"
                        size={26}
                        color={color}
                    />
                ),
            }}/>
        </Tab.Navigator>
    );
}
