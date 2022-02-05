<template>
  <div class="workspace-container">
    <div>
      <input type="number" v-model="bpm">
      <button @click="sendBPM()">Send new BPM</button>
    </div>
    <div class="color-picker">
      <LightsTrack></LightsTrack>
    </div>
    <div class="sound">
      <div class="playButton">
        <img id="playButton" src="@/assets/icons/bouton-jouer.png" @click="playTrack()" alt=""/>
      </div>
      <div class="tracks">
        <div class="tracksList">
          <TrackLine v-for="(n, index) in trackArray" :key="index" v-on:addLine="disableTrack()" :busCol="busCol"></TrackLine>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import LightsTrack from './LightsTrack.vue';
import Vue from "vue";
import TrackLine from "./TrackLine";

export default {
  name: "Workspace",
  components:{
    TrackLine,
    LightsTrack
  },
  data(){
    return {
      busCol: new Vue(),
      bpm: 0,
      trackArray: [0]
    }
  },
  sockets:{},
  methods:{
    sendBPM(){
      console.log('Send BPM');
      this.$socket.emit('newBPM',this.bpm);
    },
    disableTrack(){
      this.trackArray.push(this.trackArray[this.trackArray.length-1]+1);
      console.log('Src added in track col');
    },
    playTrack(){
      this.busCol.$emit('playTrack', {
      });
    }
  },
};
</script>

<style lang="css" scoped>
  .workspace-container{
    width: 100%;
    overflow: hidden;
  }

  .tracks{
    min-height: 70%;
    max-height: 95%;
    background-color: #535353;
    padding: 1.5em 1em;
    overflow: scroll;
    display: flex;
    width: 100%;
  }

  .tracks::-webkit-scrollbar{
    height: 10px;
    width: 10px;
  }

  .tracks::-webkit-scrollbar-thumb{
    background-color: #ffffff13;
    border-radius: 10px;
    height: 8px;
  }

  #playButton{
    height: 75px;
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

  .tracksList{
    flex: 1 1 auto;
  }

  .sound{
    display: flex;
    height: 100%;
  }
</style>
