<template>
  <div class="lineSound">
    <div class="playButton">
      <img id="playButton" src="@/assets/icons/bouton-jouer.png" @click="playTracks()" alt=""/>
    </div>
    <Track v-for="(n, index) in trackArray" :key="index" v-bind:index="index" v-on:sourceadded="soundRecordAdded()" v-on:duration="addDuration($event)" :bus="bus"></Track>
  </div>
</template>

<script>
import Track from "./Track";
import Vue from "vue";
export default {
  name: "TrackLine",
  props:{
    busCol: Vue
  },
  components: {Track},
  data(){
    return {
      bus: new Vue(),
      bpm: 0,
      trackArray: [0],
      trackDurationArray: [],
      trackCumulateDurationArray: [0],
    }
  },
  created() {
    this.busCol.$on('playTrack', () => {
      this.playTracks();
    });
  },
  sockets:{},
  methods:{
    addDuration($event){
      this.trackDurationArray.push($event)
      for(let i=1; i<this.trackDurationArray.length; i++){
        this.trackCumulateDurationArray[i] = this.trackCumulateDurationArray[i-1]+this.trackDurationArray[i-1];
        this.trackCumulateDurationArray[i+1] = this.trackCumulateDurationArray[i]+this.trackDurationArray[i];
      }
    },
    soundRecordAdded(){
      if(this.trackArray.length===1){
        // Tell the workspace to add an empty line
        this.$emit("addLine");
      }
      this.trackArray.push(this.trackArray[this.trackArray.length-1]+1);
      console.log('Src added in track line');
    },
    playTracks(){
        this.bus.$emit('playTrack', this.trackCumulateDurationArray);
    },
  },
}
</script>

<style scoped>
.lineSound{
  display: flex;
  height: 70px;
  width: fit-content;
  background-color: #444444;
  border-radius: 4px;
  box-shadow: 2px 2px 3px #222222;
  margin-bottom: 0.5em;
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
</style>
