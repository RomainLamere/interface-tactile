<template>
  <div class="lineSound" ref="lineSound">
    <div class="playButton">
      <img v-if="canPlay" src="@/assets/icons/bouton-jouer.png" @click="playTracks()" alt=""/>
      <img v-else src="@/assets/icons/stop-button.png" @click="playTracks()" alt=""/>
    </div>
    <Track v-for="(n, indexTrack) in this.$store.getters.arrayTracks[index]" :key="indexTrack" v-bind:index="indexTrack" v-on:sourceadded="soundRecordAdded($event)" v-on:duration="addDuration($event)" v-on:trackDeleted="deleteTrack($event)" :bus="bus"></Track>
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
      trackCumulateDurationArray: [0],
      canPlay : true
    }
  },
  created() {
    this.$store.subscribe(() => {
      this.canPlay = this.$store.getters.trackLineCanPlay[this.index]
    })
    this.busCol.$on('playTrack', () => {
      if(this.index !== -1) {
        this.playTracks();
      }
    });
    this.busCol.$on('stopAll', () => {
      if(this.$store.getters.trackLineCanPlay[this.index] === false){
        if(this.index !== -1) {
          this.playTracks();
        }
      }
    });
    this.busCol.$on('reloadLineTrack', () => {
      if(this.index !== -1) {
        if (this.index >= this.$store.getters.arrayTracks.length) {
          this.index = -1;
        }
      }
      if(this.index !== -1) {
        this.setArrayCumulateDuration()
        setTimeout(() => {
          this.bus.$emit("reloadDisplay", this.index);
        }, 100)
      }
    });
  },
  sockets:{},
  methods:{
    deleteTrack($event){
      let numTrackDeleted = $event;
      if(numTrackDeleted === 0){
        this.$store.getters.arrayTracks[this.index].shift();
        this.$store.getters.arrayTracksPlaying[this.index].shift();
        this.setArrayCumulateDuration()
      }else {
        this.$store.getters.arrayTracks[this.index].splice(numTrackDeleted,1)
        this.$store.getters.arrayTracksPlaying[this.index].splice(numTrackDeleted,1)
        this.setArrayCumulateDuration()
      }
      if(this.index !== 0) {
        if (this.$store.getters.arrayTracks[this.index].length === 1 && this.index !== this.$store.getters.arrayTracks.length-1 ) {
          this.$emit("deleteLine", this.index)
        }
      }else if (this.index ===0){
        if(this.$store.getters.arrayTracks[this.index].length === 1 && this.$store.getters.arrayTracks[this.index+1].length === 1){
          this.$emit("deleteLine", this.index+1)
        }else if(this.$store.getters.arrayTracks[this.index].length === 1 && this.$store.getters.arrayTracks[this.index+1].length > 1){
          this.$emit("deleteLine", this.index)
        }
      }
      this.$store.commit('resetMaxTrackWidth')
      this.bus.$emit("reloadDisplay", this.index);
    },
    addDuration(){
      this.setArrayCumulateDuration();
      this.$store.commit('pushLineArrayTracksPlaying', this.index)
    },
    setArrayCumulateDuration(){
      let listCumulateDuration = [0];
      for(let i=1; i<this.$store.getters.arrayTracks[this.index].length-1; i++){
        listCumulateDuration[i] = (listCumulateDuration[i-1]+this.$store.getters.arrayTracks[this.index][i-1].duration);
      }
      this.$store.commit("setArrayLineTracksDuration",{"line": this.index, "lineArrayTracksDuration":[]})
      this.$store.commit("setArrayLineTracksDuration",{"line": this.index, "lineArrayTracksDuration":listCumulateDuration})
      setTimeout(() => {
        this.$store.commit('setMaxTrackWidth',parseFloat(window.getComputedStyle(this.$refs.lineSound).getPropertyValue('width').split('px')[0])+16);
      }, 200);
    },
    soundRecordAdded($event){
      if(this.$store.getters.arrayTracks[this.index].length===1){
        // Tell the workspace to add an empty line
        this.$emit("addLine");
      }
      this.$store.commit("pushArrayLineTracks",{"line" : this.index, "track": $event})
      console.log('Src added in track line', this.$store.getters.arrayTracks);
    },
    playTracks(){
      this.bus.$emit('playTrack', this.index);
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

.playButton{
  flex: 0 1 auto;
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 100%;
}

.playButton img{
  cursor: pointer;
  height: 50px;
  margin: 1em;
}

</style>
