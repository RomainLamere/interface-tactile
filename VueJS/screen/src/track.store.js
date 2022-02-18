import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export const store = new Vuex.Store({
    state:{
        maxTrackWidth: 0
    },
    getters:{
        maxTrackWidth(state){
            return state.maxTrackWidth;
        }
    },
    mutations:{
        setMaxTrackWidth(state, width){
            if(state.maxTrackWidth < width){
                state.maxTrackWidth = width;
            }
        }
    },
})