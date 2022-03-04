<template>
  <drop class="drop-zone" @drop="trackDropped" :class="{empty: trackZone === ''}" @dblclick="soundControl()">
    <div v-bind:style="styleTrackSound" class="sound" :class="`${trackZone}`">
      <img v-if="instrument !== ''" :src="require(`@/assets/icons/${instrument}.png`)" draggable="false"/>
      <img v-else :src="require('@/assets/icons/musical-note.png')" draggable="false"/>
      <div class="line"></div>
      <button class="delete-track-btn" v-if="instrument !== ''" @click="deleteTrack()">x</button>
    </div>
  </drop>
</template>
<script>
import { Drop } from "vue-easy-dnd";
import Vue from "vue";

export default {
  components: {
    Drop,
  },
  props:{
    index: Number,
    bus: Vue,
  },
  data() {
    return {
      instrument: '',
      trackZone: '',
      recordAsArrayBuffer: null,
      audioContext: new AudioContext(),
      soundPaused: true,
      firstPlay: true,
      styleTrackSound: {
        width: "100px",
      },
      timeOutPlay : null,
    };
  },
  created() {
    // check si on doit jouer ou stopper la track
    this.bus.$on('playTrack', ($event) => {
      if (this.index !== -1 && $event !== -1) {
        if (this.$store.getters.arrayTracksPlaying[$event][this.index] === true) { // si en train d'etre joué
          this.soundControl($event);
        } else { // pas en train d'etre joué
          if (this.timeOutPlay === null && this.$store.getters.trackLineCanPlay[$event] !== false && this.$store.getters.arrayTracksDuration[$event] !== undefined) { // si pas prevu detre jouer
            this.timeOutPlay = setTimeout(() => {
              this.soundControl($event);
              this.timeOutPlay = null;
            }, this.$store.getters.arrayTracksDuration[$event][this.index] * 1000);
          } else { // si prevu detre joué
            clearTimeout(this.timeOutPlay);
            this.timeOutPlay = null;
          }
        }
      }
    });
    this.bus.$on('reloadDisplay', ($event) => {
      if(this.index !== -1) {
        if (this.index >= this.$store.getters.arrayTracks[$event].length) {
          this.index = -1;
        } else {
          let instrument = this.$store.getters.arrayTracks[$event][this.index].instrument;
          if (instrument === undefined) {
            this.instrument = '';
          } else {
            this.instrument = instrument;
          }
          this.trackZone = this.$store.getters.arrayTracks[$event][this.index].trackZone;
          this.recordAsArrayBuffer = this.$store.getters.arrayTracks[$event][this.index].recordAsArrayBuffer;
          let widthTrack = this.$store.getters.arrayTracks[$event][this.index].styleTrackSoundWidth;
          if (widthTrack === undefined) {
            this.styleTrackSound.width = "100px";
          } else {
            this.styleTrackSound.width = widthTrack;
            this.styleTrackSound.width = widthTrack;
          }
        }
      }
    });
  },
  methods: {
    deleteTrack(){
      this.$emit("trackDeleted", this.index);
    },
    trackDropped(e) {
      this.trackZone = e.data.zone;
      this.instrument = e.data.instrument;
      this.recordAsArrayBuffer = e.data.record;
      this.audioContext.decodeAudioData(this.copyBuff(), (decoded) =>{
        this.styleTrackSound.width = decoded.duration*20+"px";
        let track = {
          "trackZone" : this.trackZone,
          "instrument" : this.instrument,
          "recordAsArrayBuffer" : this.recordAsArrayBuffer,
          "styleTrackSoundWidth" : decoded.duration*20+"px",
          "duration" : decoded.duration
        }
        this.$emit('sourceadded',track);
        this.$emit("duration", decoded.duration);
      });
    },
    playSound(line){
      if(this.recordAsArrayBuffer){
        const audioBuffer = this.copyBuff();
        let source = this.audioContext.createBufferSource();
        source.onended = async () => {
          this.audioContext.close().then(() => {
            this.soundPaused = true;
            this.firstPlay = true;
          });
          this.audioContext = new AudioContext();
          this.$store.commit("changeStateArrayTracksPlaying", {"line": line, "index":this.index, "play":false})
        };
        this.audioContext.decodeAudioData(audioBuffer, (decoded) =>{
          source.buffer = decoded;
          source.connect(this.audioContext.destination);
          source.start(0);
        });
      }
    },
    soundControl(line){
      if(this.recordAsArrayBuffer) {
        if (this.firstPlay) {
          this.$store.commit("changeStateArrayTracksPlaying", {"line": line, "index":this.index, "play":true})
          this.playSound(line);
          this.soundPaused = false;
          this.firstPlay = false;
        } else{
          this.audioContext.close().then(() => {
            this.soundPaused = true;
            this.firstPlay = true;
          });
          this.audioContext = new AudioContext();
          this.$store.commit("changeStateArrayTracksPlaying", {"line": line, "index":this.index, "play":false})
        }
      }
    },
    copyBuff(){
      let dst = new ArrayBuffer(this.recordAsArrayBuffer.byteLength);
      new Uint8Array(dst).set(new Uint8Array(this.recordAsArrayBuffer));
      return dst;
    }
  }
};
</script>
<style lang="css" scoped>
.drop-zone {
  display: inline-flex;
  margin-right: 1px;
}

.empty{
  flex: 1 1 auto;
  min-width: 100px;
}

.drop-in{
  box-shadow: 2px 1px 15px #0095ff;
  box-shadow: 2px 1px 15px #0095ff;
}

.instru {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100px;
  height: 70px;
  margin-right: 10px;
  background-color: #444444;
  border-radius: 4px;
  box-shadow: 2px 2px 3px #222222;
}

.instru img {
  width: 60px;
  height: 60px;
}

.sound{
  position: relative;
}

.sound .line{
  width: 100%;
  height: 10px;
  margin: auto;
}

.A .line{
  background-color: #d66f6f;
}

.B .line{
  background-color: #6fd674;
}

.C .line{
  background-color: #6f87d6;
}

.D .line{
  background-color: #d4d66f;
}

.sound{
  display: grid;
}

.sound .line, .sound img{
  grid-area: 1 / 1;
}

.sound img{
  width: 60px;
  height: 60px;
  margin: auto;
  object-fit: cover;
  opacity: 0.1;
}

.delete-track-btn{
  border: none;
  background-color: transparent;
  cursor: pointer;
  position: absolute;
  right: 0;
  color: red;
  font-size: 25px;
  font-family: sans-serif;
  font-weight: bold;
}
</style>
