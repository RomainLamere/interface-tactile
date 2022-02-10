<template>
  <div class="lineSound">
    <div class="playButton">
      <img v-if="!play" id="playButton" src="@/assets/icons/bouton-jouer.png" @click="playTracks()" alt=""/>
      <img v-if="play" id="pauseButton" src="@/assets/icons/bouton-pause.png" @click="stopTracks()" alt=""/>
    </div>
    <Track v-for="(n, index) in trackArray" :key="index" v-bind:index="index" v-on:sourceadded="disableTrack()" v-on:duration="addDuration($event)" v-on:play="isPlayed($event)" v-on:finish="isFinished($event)" :bus="bus"></Track>
  </div>
</template>

<script>
import Track from "./Track";
import Vue from "vue";
export default {
  name: "TrackLine",
  props:{
    index: Number,
    busCol: Vue
  },
  components: {Track},
  data(){
    return {
      bus: new Vue(),
      bpm: 0,
      trackArray: [0],
      trackArrayPlaying: [],
      trackDurationArray: [],
      trackCumulateDurationArray: [0],
      play : false
    }
  },
  created() {
    this.busCol.$on('checkIfCanPlayTrack', () => {
        this.$emit("canPlay",this.canPlay())
    });
    this.busCol.$on('playTrack', () => {
        this.playTracks();
        this.$emit("lineIsStart", this.index);
    });
    this.busCol.$on('stopTrack', () => {
      this.stopTracks();
    });
  },
  sockets:{},
  methods:{
    addDuration($event){
      this.trackDurationArray.push($event);
      this.trackArrayPlaying.push(false);
      for(let i=1; i<this.trackDurationArray.length; i++){
        this.trackCumulateDurationArray[i] = this.trackCumulateDurationArray[i-1]+this.trackDurationArray[i-1];
        this.trackCumulateDurationArray[i+1] = this.trackCumulateDurationArray[i]+this.trackDurationArray[i];
      }
    },
    disableTrack(){
      if(this.trackArray.length===1){
        this.$emit("addLine");
      }
      this.trackArray.push(this.trackArray[this.trackArray.length-1]+1);
      console.log('Src added in track line');
    },
    playTracks(){
        this.bus.$emit('playTrack', this.trackCumulateDurationArray);
        if(this.trackArray.length > 1) {
          this.play = true;
        }
    },
    stopTracks(){
      if(!this.canPlay()) { // si on ne peut pas play c'est parceque la ligne est en train d'etre jouer, donc on la stop
        this.bus.$emit('playTrack', this.trackCumulateDurationArray);
        this.play = false;
      }
    },
    isFinished($event){
      if($event === this.trackArrayPlaying.length-1){
        this.play = false;
        this.$emit('lineIsStop',this.index);
      }
      this.trackArrayPlaying[$event] = false;
    },
    isPlayed($event){
      this.trackArrayPlaying[$event] = true;
    },
    canPlay(){
      for(let i = 0 ; i < this.trackArrayPlaying.length ; i++){
        if(this.trackArrayPlaying[i] === true)
          return false;
      }
      return true;
    }
  },
}
</script>

<style scoped>
.lineSound{
  display: flex;
}

#playButton{
  height: 50px;
  margin: 1em;
}

#playButton:hover{
  cursor: pointer;
}

.playButton{
  flex: 0 1 auto;
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 100%;
}

#pauseButton:hover{
  cursor: pointer;
}

#pauseButton{
  height: 50px;
  margin: 1em;
}
</style>
