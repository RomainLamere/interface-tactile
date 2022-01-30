<template>
  <div class="workspace-container">
    <div>
      Message: {{ msg }}
      <input type="number" v-model="bpm">
      <button @click="sendBPM()">Send new BPM</button>
    </div>
    <div class="color-picker">
      <input type="color" name="color" id="color" v-model="color">
      {{color}}
      <button @click="sendColor()">Send new color</button>
    </div>
    <div class="tracks">
      <Track v-for="(n, index) in trackArray" :key="index" v-on:sourceadded="disableTrack()"></Track>
    </div>
  </div>
</template>

<script>
import Track from './Track.vue';

export default {
  name: "Workspace",
  components:{
    Track
  },
  data(){
    return {
      msg: '',
      bpm: 0,
      color: '#000000',
      trackArray: [0]
    }
  },
  sockets:{},
  methods:{
    sendBPM(){
      console.log('Send BPM');
      this.$socket.emit('newBPM',this.bpm);
    },
    sendColor(){
      this.$socket.emit('changeLights',this.color)
    },
    disableTrack(){
      this.trackArray.push(this.trackArray[this.trackArray.length-1]+1);
      console.log('Src added in track');
    }
  },
  created() {
    this.$options.sockets.eventToScreen = (data) => {
      console.log(`Test subscribe: ${data}`);
      this.msg = data;
    };
  },
};
</script>

<style lang="css" scoped>
  .workspace-container{
    width: 100%;
    overflow: hidden;
  }

  .color-picker{
    color: white;
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
