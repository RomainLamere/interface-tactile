import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import React from "react";
import {Metronome} from "./Metronome";
import {Record} from "./Record";

const Tab = createBottomTabNavigator();

export const BottomNavigation =()=> {
    return (
        <Tab.Navigator>
            <Tab.Screen name="Metronome" component={Metronome} />
            <Tab.Screen name="Pad" component={Record} />
        </Tab.Navigator>
    );
}
