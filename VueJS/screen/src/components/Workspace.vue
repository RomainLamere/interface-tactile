<template>
  <div class="workspace-container">
    Message: {{ msg }}
    <input type="number" v-model="bpm">
    <button @click="sendBPM()">Send new BPM</button>
    <div class="tracks">
      <Track></Track>
    </div>
    <av-waveform id="la" audio-src="sonoiseaux.mp3"></av-waveform>
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
      bpm: 0
    }
  },
  sockets:{},
  methods:{
    sendBPM(){
      console.log('Send BPM');
      this.$socket.emit('newBPM',this.bpm);
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

  .tracks{
    min-height: 70%;
    background-color: #535353;
    padding: 1.5em 1em;
    overflow-x: scroll;
  }

  .tracks::-webkit-scrollbar{
    height: 10px;
  }

  .tracks::-webkit-scrollbar-thumb{
    background-color: #ffffff13;
    border-radius: 10px;
    height: 8px;
  }
</style>
