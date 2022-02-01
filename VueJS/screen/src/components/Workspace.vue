<template>
  <div class="workspace-container">
    <div>
      <input type="number" v-model="bpm">
      <button @click="sendBPM()">Send new BPM</button>
    </div>
    <div class="color-picker">
      <LightsTrack></LightsTrack>
    </div>
    <div class="tracks">
      <Track v-for="(n, index) in trackArray" :key="index" v-on:sourceadded="disableTrack()"></Track>
    </div>
  </div>
</template>

<script>
import LightsTrack from './LightsTrack.vue';
import Track from './Track.vue';

export default {
  name: "Workspace",
  components:{
    Track,
    LightsTrack
  },
  data(){
    return {
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
      console.log('Src added in track');
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
</style>
