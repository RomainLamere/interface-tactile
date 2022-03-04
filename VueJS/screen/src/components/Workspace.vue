<template>
  <div class="workspace-container">
    <div class="studyo">
      <div class="playButton">
        <img v-if="$store.getters.allTrackCanPlay" src="@/assets/icons/bouton-jouer.png" @click="playTrack()" alt="" draggable="false"/>
        <img v-else src="@/assets/icons/stop-button.png" @click="playTrack()" alt="" draggable="false"/>
      </div>
      <div class="compose">
        <Tracker :busCol="busCol"></Tracker>
        <LightsTrack :busCol="busCol" :lightId="1"></LightsTrack>
        <LightsTrack :busCol="busCol" :lightId="2"></LightsTrack>
        <LightsTrack :busCol="busCol" :lightId="3"></LightsTrack>
        <div class="tracks">
          <div class="tracksList">
            <TrackLine
                v-for="(n, index) in this.$store.getters.arrayTracks"
                :key="index" v-bind:index="index"
                v-on:addLine="addEmptyLine()"
                :busCol="busCol" v-on:deleteLine="deleteLine($event)"
            ></TrackLine>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import LightsTrack from './LightsTrack.vue';
import Vue from "vue";
import TrackLine from "./TrackLine";
import Tracker from "./Tracker.vue"

export default {
  name: "Workspace",
  components:{
    TrackLine,
    LightsTrack,
    Tracker
  },
  data(){
    return {
      busCol: new Vue(),
      bpm: 0,
    }
  },
  created() {
  },
  methods: {
    addEmptyLine() {
      this.$store.commit('pushArrayTracks', [0])
      this.$store.commit('pushArrayTracksPlaying')
      this.$store.commit('pushTrackLineCanPlay')
      console.log('Src added in track col');
    },
    playTrack() {
      if(!this.$store.getters.trackLineCanPlay.includes(false)){
        this.busCol.$emit('playTrack', {});
      }else{
        this.busCol.$emit('stopAll', {});
        this.busCol.$emit('soundListeningEnded', {});
      }
    },
    deleteLine($event){
      this.$store.getters.arrayTracks.splice($event, 1)
      this.$store.getters.arrayTracksPlaying.splice($event, 1)
      this.busCol.$emit('reloadLineTrack');
    }
  }
};
</script>

<style lang="css" scoped>
.workspace-container{
  width: 100%;
  overflow: hidden;
}

.studyo{
  height: 100%;
  width: 100%;
  display: flex;
}

.studyo ::-webkit-scrollbar{
  height: 10px;
  width: 10px;
  background-color: #535353;
}

.studyo ::-webkit-scrollbar-thumb{
  background-color: #ffffff1f;
  border-radius: 10px;
  height: 8px;
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
  height: 75px;
  margin: 1em;
}

.compose{
  position: relative;
  flex: 1 1 100%;
  display: flex;
  flex-direction: column;
  overflow-x: scroll;
  background-color: #535353;
}

.tracks{
  height: 100%;
  width: max-content;
  padding: 1em;
  overflow: visible;
  overflow-y: scroll;
}

.tracksList{
  flex: 1 1 auto;
}

.sound{
  display: flex;
  height: 100%;
}

</style>
