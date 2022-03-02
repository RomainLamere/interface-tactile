import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export const store = new Vuex.Store({
    state:{
        maxTrackWidth: 0,
        arrayTracks: [[{
            "trackZone" : '',
            "instrument" : '',
            "recordAsArrayBuffer" : '',
            "styleTrackSoundWidth" : '',
            "duration" : ''
        }]],
        arrayTracksDuration: [[0]],
        arrayTracksPlaying: [[false]],
        trackLineCanPlay: [true],
        allTrackCanPlay: true,
    },
    getters:{
        maxTrackWidth(state){
            return state.maxTrackWidth;
        },
        arrayTracks(state){
            return state.arrayTracks;
        },
        arrayTracksDuration(state){
            return state.arrayTracksDuration;
        },
        arrayTracksPlaying(state){
            return state.arrayTracksPlaying;
        },
        trackLineCanPlay(state){
            return state.trackLineCanPlay;
        },
        allTrackCanPlay(state){
            return state.allTrackCanPlay;
        },
    },
    mutations:{
        setMaxTrackWidth(state, width){
            if(state.maxTrackWidth < width){
                state.maxTrackWidth = width;
            }
        },
        resetMaxTrackWidth(state){
            state.maxTrackWidth = 0
        },
        setArrayTracks(state, arrayTracks){
                state.arrayTracks = arrayTracks;
            },
        pushArrayTracks(state, lineTracks){
            state.arrayTracks.push(lineTracks);
        },
        pushArrayLineTracks(state, {line,track}){
            state.arrayTracks[line].pop();
            state.arrayTracks[line].push(track);
            state.arrayTracks[line].push([{
                "trackZone" : '',
                "instrument" : '',
                "recordAsArrayBuffer" : '',
                "styleTrackSoundWidth" : '',
                "duration" : ''
            }]);
        },
        setArrayTracksDuration(state, arrayTracksDuration){
            state.arrayTracksDuration = arrayTracksDuration;
        },
        setArrayLineTracksDuration(state, {line,lineArrayTracksDuration}){
            state.arrayTracksDuration[line] = lineArrayTracksDuration;
        },
        pushArrayTracksPlaying(state){
            state.arrayTracksPlaying.push([false]);
        },
        pushLineArrayTracksPlaying(state, line){
            state.arrayTracksPlaying[line].push(false);
        },
        pushTrackLineCanPlay(state){
            state.trackLineCanPlay.push(true);
        },
        changeStateArrayTracksPlaying(state, {line, index, play}){
            state.arrayTracksPlaying[line][index] = play;
            if(play === false) {
                this.getters.trackLineCanPlay[line] = false;
                state.allTrackCanPlay = false;
                let canPlayedLine = true;
                for (let i = 0; i < this.getters.arrayTracksPlaying[line].length; i++) {
                    if (this.getters.arrayTracksPlaying[line][i] === true) {
                        canPlayedLine = false;
                    }
                }
                if(canPlayedLine === true){
                    this.getters.trackLineCanPlay[line] = true;
                }
                let canPlayedAll = true;
                for (let j = 0; j < this.getters.arrayTracksPlaying.length; j++) {
                    if (this.getters.trackLineCanPlay[j] === false) {
                        canPlayedAll = false;
                    }
                }
                if(canPlayedAll === true){
                    state.allTrackCanPlay = true;
                }
            }else{
                this.getters.trackLineCanPlay[line] = false;
                state.allTrackCanPlay = false;
            }
        },
    }
})
